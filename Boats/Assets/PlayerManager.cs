using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public bool isInWater = false;
    public float nearestWaterY;

    public GameObject renderer;
    public Transform playerOffset;

    public float maxMotorForce;
    public float maxMotorTorque;

    void Start()
    {
        
    }

    public void Update()
    {
        if(Input.GetAxis("Joystick_Y") > 0)
        {
            this.GetComponent<Rigidbody>().AddForce(renderer.transform.forward * maxMotorForce * Input.GetAxis("Joystick_Y"));
            Debug.Log("IsForward");
        }

        if (Input.GetAxis("Joystick_X") > 0.05f || Input.GetAxis("Joystick_X") < -0.05f)
        {
           this.GetComponent<Rigidbody>().AddRelativeTorque(renderer.transform.up * maxMotorTorque * Input.GetAxis("Joystick_X"));
            Debug.Log("IsTurning");
        }
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
