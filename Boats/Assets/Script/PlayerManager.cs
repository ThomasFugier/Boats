using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerIndex {Player1, Player2, Player3, Player4, AI, Anyone};
public enum State {Docked, InSea};

public class PlayerManager : MonoBehaviour
{
    [Header("Informations")]
    [Space]
    public PlayerIndex playerIndex;
    public bool isInWater = false;
    public bool isInFishZone = false;
    public bool isFishing = false;
    public State state;
    public Dock actualDock;

    [Header("Inventory")]
    [Space]
    public int money;
    public int fishAmount;

    [Header("References")]
    [Space]
    public GameObject renderer;
    public Transform playerOffset;

    [Header("Configuration")]
    [Space]
    public float speedToDock;
    public float maxMotorForce;
    public float maxMotorTorque;
    public float bounce_Factor;
    public float bounce_CollisionMaxSource;

    private float nearestWaterY;

    [Header("AI")]
    [Space]
    public AI_LookAt AI_LookAt;
    public Transform target_AI;
    public float actualBrainX;
    public float actualBrainY;

    [Header("Internal UI References")]
    [Space]
    public UnityEngine.UI.Text text_fishAmount;
    public Canvas playerCanvas;
    public UnityEngine.UI.Text text_playerName;
    public InputTooltip InputTooltip_mainInput;

    [HideInInspector]
    public Fishzone actualFishzone;

    private float mainInteractionTimer;

    //Inputs
    private bool mainInputLocked = false;
    private bool mainInput;

    public void UpdatePlayerIndex()
    {
        text_playerName.text = playerIndex.ToString();
    }

    public void UpdateInventory()
    {
        text_fishAmount.text = fishAmount.ToString();
    }

    void Start()
    {
        UpdatePlayerIndex();

        playerCanvas.transform.parent = null;
    }

    public void UpdateTooltips()
    {
        InputTooltip_mainInput.filler.fillAmount = mainInteractionTimer;

        if (mainInput == false)
        {
            mainInteractionTimer = Mathf.Lerp(mainInteractionTimer, 0, Time.deltaTime * 3);
        }
    }

    public void Update()
    {
        ProcessInputs();
        UpdateInventory();
        UpdateCanvasPosition();
        UpdateTooltips();
        PlayerHaptics();
    }

    public void UpdateCanvasPosition()
    {
        RectTransform myRect = playerCanvas.transform.GetChild(0).transform.GetComponent<RectTransform>();
        Vector2 myPositionOnScreen = Camera.main.WorldToScreenPoint(this.transform.position);
        myPositionOnScreen.y += 50;
        myRect.anchoredPosition = myPositionOnScreen - playerCanvas.GetComponent<RectTransform>().anchoredPosition;
    }

    public void ProcessInputs()
    {
        float y = 0;
        float x = 0;
        mainInput = false;

        if(playerIndex != PlayerIndex.AI)
        {
            y = InputManager.Instance.GetJoystickAxis_Y(playerIndex);
            x = InputManager.Instance.GetJoystickAxis_X(playerIndex);
            mainInput = InputManager.Instance.GetJoystickButton_Main(playerIndex);
        }

        else
        {
            y = GetAIBrain_Y();
            x = GetAIBrain_X();
            actualBrainX = x;
            actualBrainY = y;
        }

        if (y > 0)
        {
            this.GetComponent<Rigidbody>().AddForce(renderer.transform.forward * maxMotorForce * y);
   
        }

        if (x > 0.05f || x < -0.05f)
        {
            this.GetComponent<Rigidbody>().AddRelativeTorque(renderer.transform.up * maxMotorTorque * x);

        }

        if(mainInput)
        {
            PressingOnMainButton();
        }

        else
        {
            if(actualFishzone)
            {
                StopFishing();
            }
            
        }

        mainInteractionTimer = Mathf.Clamp(mainInteractionTimer, 0, 1);
    }

    public void PressingOnMainButton()
    {
        if(isInFishZone)
        {
            Fish();
        }

        if(state == State.Docked)
        {
            RequestUseDock();
        }
    }

    public void Fish()
    {
        if(actualFishzone)
        {
            if (actualFishzone.canBeFished)
            {
                isFishing = true;
                mainInteractionTimer += Time.deltaTime * actualFishzone.requiredTimeToCatch;
                InputTooltip_mainInput.filler.fillAmount = mainInteractionTimer / 1;

                if (mainInteractionTimer > 1)
                {
                    GatherFishing(actualFishzone);
                }
            }

            else
            {
                StopFishing();
            }
        } 
    }

    public void GatherFishing(Fishzone fishzone)
    {
        fishAmount += fishzone.fishAmount;
        fishzone.DestroyFishZone();
        StopFishing();
    }

    public void StopFishing()
    {
        if(actualFishzone)
        {
            isFishing = false;
            StartCoroutine(LockMainInputForSeconds(0.5f));
        }
    }

    public void StopDocking()
    {
        if (actualDock != null)
        {
            StartCoroutine(LockMainInputForSeconds(0.5f));
        }
    }

    IEnumerator LockMainInputForSeconds(float delay)
    {
        mainInputLocked = true;
        yield return new WaitForSeconds(1);
        mainInputLocked = false;
    }

    public float GetAIBrain_X()
    {
        var targetDir = transform.position - target_AI.transform.transform.position;
        var forward = -transform.forward;
        var localTarget = transform.InverseTransformPoint(target_AI.transform.position);

        float angle = Mathf.Atan2(localTarget.x, localTarget.z) * Mathf.Rad2Deg;

        
        return Mathf.Clamp(angle,-1,1);
    }

    public float GetAIBrain_Y()
    {
        Vector3 thisPos = this.transform.position;
        Vector3 targetPos = target_AI.transform.position;
        thisPos.y = 0;
        targetPos.y = 0;

        return Mathf.Clamp(Vector3.Distance(thisPos, targetPos) / 3, -1, 1);
    }

    public void EnterInWater(float y)
    {
        isInWater = true;
        nearestWaterY = Mathf.Abs(y);
        this.GetComponent<Buoyancy>().isInWater = true;
        this.GetComponent<Buoyancy>().offsetY = nearestWaterY;
        this.GetComponent<Rigidbody>().mass = 1;
    }

    public void ExitFromWater()
    {
        isInWater = false;
        this.GetComponent<Buoyancy>().isInWater = false;
        this.GetComponent<Rigidbody>().mass = 1;
    }

    public void BouncePlayer(PlayerManager player, Vector3 direction, float impactMagnitude)
    {
        //player.GetComponent<Rigidbody>().AddForce(direction * impactMagnitude * bounce_Factor, ForceMode.Impulse);
    }

    public void EnableMainTooltip()
    {
        InputTooltip_mainInput.gameObject.SetActive(true);
    }

    public void DisableMainTooltip()
    {
        InputTooltip_mainInput.gameObject.SetActive(false);
    }

    public void EnterInFishZone(Fishzone fishzone)
    {
        actualFishzone = fishzone;
        isInFishZone = true;
        EnableMainTooltip();
    }

    public void ExitFromFishZone()
    {
        actualFishzone = null;
        isInFishZone = false;
        StopFishing();
        DisableMainTooltip();
    }

    public void SellFish()
    {
        Earn(fishAmount * GameManager.Instance.gameplayManager.moneyPerFish);
        fishAmount = 0;
        DisableMainTooltip();
    }

    public void Earn(int howMuch)
    {
        money += howMuch;
    }

    public void RequestUseDock()
    {
        if(actualDock.dockType == Dock.DockType.Fish && fishAmount > 0)
        {
               mainInteractionTimer += Time.deltaTime * speedToDock;

               InputTooltip_mainInput.filler.fillAmount = mainInteractionTimer / 1;

                if (mainInteractionTimer > 1)
                {
                    SellFish();
                }


                else
                {
                    StopDocking();
                }
        }
    }

    public void SetDockState(State newState, Dock dock)
    {
        state = newState;
        actualDock = dock;

        if(state == State.Docked)
        {
            if (dock.dockType == Dock.DockType.Fish && fishAmount > 0)
            {
                EnableMainTooltip();
            }
        }
       
        else
        {
            DisableMainTooltip();
        }
    }

    public void PlayerHaptics()
    {
        float lowMotor = 0;
        float highMotor = 0;

        if(InputTooltip_mainInput.gameObject.activeSelf)
        {
            highMotor = mainInteractionTimer;
        }
       
        if(InputManager.Instance.GetJoystickAxis_Y(playerIndex) > 0)
        {
            lowMotor = InputManager.Instance.GetJoystickAxis_Y(playerIndex) * GameManager.Instance.GetComponent<XInputTestCS>().motorFactor;
        }

        GameManager.Instance.GetComponent<XInputTestCS>().Haptic(playerIndex, lowMotor, highMotor);
    }
}

