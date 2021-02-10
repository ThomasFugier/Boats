using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerIndex {Player1, Player2, Player3, Player4, AI};

public class PlayerManager : MonoBehaviour
{
    [Header("Informations")]
    [Space]
    public PlayerIndex playerIndex;
    public bool isInWater = false;
    public bool isInFishZone = false;
    public bool isFishing = false;

    [Header("Inventory")]
    [Space]
    public int fishAmount;

    [Header("References")]
    [Space]
    public GameObject renderer;
    public Transform playerOffset;

    [Header("Configuration")]
    [Space]
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

    private float fishingTimer;

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

    public void Update()
    {
        ProcessInputs();
        UpdateInventory();

        RectTransform myRect = playerCanvas.transform.GetChild(0).transform.GetComponent<RectTransform>();
        Vector2 myPositionOnScreen = Camera.main.WorldToScreenPoint(this.transform.position);
        myPositionOnScreen.y += 50;
        myRect.anchoredPosition = myPositionOnScreen - playerCanvas.GetComponent<RectTransform>().anchoredPosition;
    }

    public void ProcessInputs()
    {
        float y = 0;
        float x = 0;
        bool mainButton = false;

        if(playerIndex != PlayerIndex.AI)
        {
            y = InputManager.Instance.GetJoystickAxis_Y(playerIndex);
            x = InputManager.Instance.GetJoystickAxis_X(playerIndex);
            mainButton = InputManager.Instance.GetJoystickButton_Main(playerIndex);
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

        if(mainButton)
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
    }

    public void PressingOnMainButton()
    {
        if(isInFishZone)
        {
            Fish();
        }
    }

    public void Fish()
    {
        if(actualFishzone)
        {
            if (actualFishzone.canBeFished)
            {
                isFishing = true;
                fishingTimer += Time.deltaTime;
                InputTooltip_mainInput.filler.fillAmount = fishingTimer / actualFishzone.requiredTimeToCatch;

                if (fishingTimer > actualFishzone.requiredTimeToCatch)
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
            fishingTimer = 0;
            InputTooltip_mainInput.filler.fillAmount = fishingTimer / actualFishzone.requiredTimeToCatch;
        }
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

    public void EnterInFishZone(Fishzone fishzone)
    {
        actualFishzone = fishzone;
        isInFishZone = true;
        InputTooltip_mainInput.gameObject.SetActive(true);
    }

    public void ExitFromFishZone()
    {
        actualFishzone = null;
        isInFishZone = false;
        StopFishing();
        InputTooltip_mainInput.gameObject.SetActive(false);
    }
}

