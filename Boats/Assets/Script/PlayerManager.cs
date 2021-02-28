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
    public PlayerSkinManager skinManager;

    [Header("Configuration")]
    [Space]
    public bool inputsLocked;
    public float speedToDock;
    public float maxMotorForce;
    public float maxMotorTorque;
    public float bounce_Factor;
    public float bounce_CollisionMaxSource;
    public float windFactor;

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
    public bool mainInput;

    public void UpdatePlayerIndex()
    {
        text_playerName.text = playerIndex.ToString();
    }

    public void UpdateInventory()
    {
        text_fishAmount.text = fishAmount.ToString();
    }

    public void InitPlayer()
    {
        skinManager.SetPlayerColor();
    }

    void Start()
    {
        InitPlayer();

        if (playerIndex == PlayerIndex.AI)
        {
            StartCoroutine(AI_Brain_Delayed());
        }

        if (target_AI == null)
        {
            target_AI = this.transform;
        }

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
        Vector3 windForce = new Vector3(WindManager.Instance.windX * WindManager.Instance.windForce * windFactor, 0, WindManager.Instance.windY * WindManager.Instance.windForce * windFactor);
        this.GetComponent<ConstantForce>().force = windForce;

        if(inputsLocked == false)
        {
            ProcessInputs();
            PlayerHaptics();
        }
        
        UpdateInventory();
        UpdateCanvasPosition();
        UpdateTooltips();

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
            AI_Brain();
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

    public void SetPlayerToGameplayMode()
    {
        this.GetComponent<Rigidbody>().isKinematic = false;
        this.GetComponent<Rigidbody>().useGravity = true;
        this.transform.parent = null;

        GameManager.Instance.SetPlayerSpawn(this);
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

    public void AI_Brain()
    {
        mainInput = GetAIBrain_Main();
    }
    
    IEnumerator AI_Brain_Delayed()
    {
        yield return null;

        while(true)
        {
            yield return new WaitForSeconds(0.1f);

            if (fishAmount == 0)
            {
                Fishzone[] fishzones = GameObject.FindObjectsOfType<Fishzone>();

                Transform nearestFishzone = null;
                float nearestDistance = 5000;

                for(int i = 0; i < fishzones.Length; i++)
                {
                    float d = Mathf.Abs(Vector3.Magnitude(fishzones[i].transform.position - this.transform.position));

                    if(d < nearestDistance)
                    {
                        nearestDistance = d;
                        nearestFishzone = fishzones[i].transform;
                    }
                }

                target_AI = nearestFishzone;
            }

            if(fishAmount > 0)
            {
                Dock[] docks = GameObject.FindObjectsOfType<Dock>();

                Transform nearestFishzone = null;
                float nearestDistance = 5000;

                for (int i = 0; i < docks.Length; i++)
                {
                    float d = Mathf.Abs(Vector3.Magnitude(docks[i].transform.position - this.transform.position));

                    if (d < nearestDistance)
                    {
                        nearestDistance = d;
                        nearestFishzone = docks[i].transform;
                    }
                }

                target_AI = nearestFishzone;
            }
        }
    }

    public bool GetAIBrain_Main()
    {
        bool returned = false;

        if (isInFishZone)
        {
            returned = true;
        }

        else if (actualDock != null)
        {
            returned = true;
        }

        else
        {
            returned = false;
        }

        return returned;
    }

    public float GetAIBrain_X()
    {
        if(target_AI == null)
        {
            target_AI = this.transform;
        }

        var targetDir = transform.position - target_AI.transform.transform.position;
        var forward = -transform.forward;
        var localTarget = transform.InverseTransformPoint(target_AI.transform.position);

        float angle = Mathf.Atan2(localTarget.x, localTarget.z) * Mathf.Rad2Deg;

        
        return Mathf.Clamp(angle,-1,1);
    }

    public float GetAIBrain_Y()
    {
        if (target_AI == null)
        {
            target_AI = this.transform;
        }

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
        if(playerIndex != PlayerIndex.AI)
        {
            float lowMotor = 0;
            float highMotor = 0;

            if (InputTooltip_mainInput.gameObject.activeSelf)
            {
                highMotor = mainInteractionTimer;
            }

            if (InputManager.Instance.GetJoystickAxis_Y(playerIndex) > 0)
            {
                lowMotor = InputManager.Instance.GetJoystickAxis_Y(playerIndex) * GameManager.Instance.GetComponent<XInputTestCS>().motorFactor;
            }

            GameManager.Instance.GetComponent<XInputTestCS>().Haptic(playerIndex, lowMotor, highMotor);
        }
    }

    public void OnDestroy()
    {
        if(playerCanvas)
        {
            Destroy(playerCanvas.gameObject);
        }
        
    }
}

