﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerIndex {Player1, Player2, Player3, Player4, AI};

public class PlayerManager : MonoBehaviour
{
    [Header("Informations")]
    [Space]
    public PlayerIndex playerIndex;
    public bool isInWater = false;

    [Header("References")]
    [Space]
    public GameObject renderer;
    public Transform playerOffset;

    [Header("Configuration")]
    [Space]
    public float maxMotorForce;
    public float maxMotorTorque;

    private float nearestWaterY;

    [Header("AI")]
    [Space]
    public AI_LookAt AI_LookAt;
    public Transform target_AI;
    public float actualBrainX;

    void Start()
    {
        
    }

    public void Update()
    {
        ProcessInputs();
    }

    public void ProcessInputs()
    {
        float y = 0;
        float x = 0;

        if(playerIndex != PlayerIndex.AI)
        {
            y = InputManager.Instance.GetJoystickAxis_Y(playerIndex);
            x = InputManager.Instance.GetJoystickAxis_X(playerIndex);
        }

        else
        {
            y = GetAIBrain_Y();
            x = GetAIBrain_X();
            actualBrainX = x;
        }

        if (y > 0)
        {
            this.GetComponent<Rigidbody>().AddForce(renderer.transform.forward * maxMotorForce * y);
            Debug.Log("IsForward");
        }

        if (x > 0.05f || x < -0.05f)
        {
            this.GetComponent<Rigidbody>().AddRelativeTorque(renderer.transform.up * maxMotorTorque * x);
            Debug.Log("IsTurning");
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
        return Mathf.Clamp(Mathf.Abs(Vector3.Distance(this.transform.position, target_AI.transform.position)), -1, 1);
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
}
