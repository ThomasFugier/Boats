using UnityEngine;

//  --- How to setup ---
//
//   -Add box Collider on Water Body and set it as Trigger.
//   -Also Add "WaterBody" component On it (optionally you can change surface level property if needed)
//   -Give water object Tag as "Water" (or you can change "waterVolumeTag" property accordingly)
//   -Add "Buoyancy" component on a Rigidbody Object that you want to float on WaterBody.
//
//
//
//  --- What this Dose ---
//
//   -Rigidbody with "Buoyency" Component floats on "Water" Tagged body.
//   -Check if Rigidbody is inside X - Z bound of Water body and gives force on Y Upwards.
//   -Buoyant force (Upwards force) increases as rigibody dive deep underwater. (can be variable by depthPowerLimit property)
//
//
//
//  --- Limitations ---
//
//   -This is clearly NOT real world Physics, just simple Up force.
//   -No additional forces or drag or waves.
//   -As this component is limited to Y axis buoyancy, make sure your Y is up, however feel free to modify.
//   -Other collider types will work as long as water surface level stay flat, as this is designed on checking collider bounding box.
//   -Rigidbody never fall Asleep if inside water body.
//   -Don't stack water Bodies on top or duplicate on same location.



[RequireComponent(typeof(Rigidbody))]
public class Buoyancy : MonoBehaviour
{
    //  ▀▄▀▄▀▄ Editor Variables ▄▀▄▀▄▀

    [SerializeField, Tooltip("increase value to make object more buoyant, default 8.")]
    float buoyantForce = 8f;
    [SerializeField, Tooltip("value 0 mean no additional Buoyant Force underwater, 1 mean Double buoyant Force underwater (underwater pressure)"),
    Range(0f, 1f)]
    float depthPower = 1f;
    [SerializeField, Tooltip("Center of Mass on Y axis (kind of), default 0.")]
    public float offsetY = 0f;
    [SerializeField, Tooltip("Tag of the Water Body")]
    string waterVolumeTag = "Water";
    public bool isInWater;
    public float waterLineOffset;


    //  ▀▄▀▄▀▄ Private Variables ▄▀▄▀▄▀

    private Rigidbody rb;
    public Collider collider;
    private float yBound;
    private bool isWaterBodySet;
    private int waterCount;



    //  ▀▄▀▄▀▄ Core Functions ▄▀▄▀▄▀


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(isInWater)
        {
            Buoy(5);
        }
    }



    //  ▀▄▀▄▀▄ Shared Functions ▄▀▄▀▄▀


    //Set and Get for Under water Buoyancy (depth pressure)(0 to 1 range).
    public void SetDepthPower(in float value)
    {
        if (value >= 0f && value <= 1f) depthPower = value;
    }

    public float GetDepthPower() => depthPower;

    //if this object fully submerged into water, returns true.
    public bool IsUnderWater() => isWaterBodySet && yBound > collider.bounds.max.y;

    //if this object floating on surface of water, returns true.
    public bool IsFloating() => isWaterBodySet && !(yBound > collider.bounds.max.y);



    //  ▀▄▀▄▀▄ Trigger Functions ▄▀▄▀▄▀


   public void Buoy(float y)
    {
        float objectYValue = collider.bounds.center.y + offsetY;

        yBound = Mathf.Abs(y) - waterLineOffset;

        if (objectYValue < yBound)
        {
            float buoyantForceMass = buoyantForce * rb.mass;
            float underWaterBuoyantForce = Mathf.Clamp01((yBound - objectYValue) * depthPower); //can be inline below
            float buoyency = buoyantForceMass + (buoyantForceMass * underWaterBuoyantForce); //can be inline below
            rb.AddForce(0f, buoyency * Time.deltaTime, 0f);
        }
    }

}