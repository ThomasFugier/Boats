using UnityEngine;
using XInputDotNetPure; // Required in C#

public class XInputTestCS : MonoBehaviour
{
    bool playerIndexSet = false;
    XInputDotNetPure.PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;

    [Header("Factors")]
    [Range(0.0f, 1.0f)]
    public float lowFactor;
    [Range(0.0f, 1.0f)]
    public float highFactor;


    [Header("Values")]
    [Range(0,1)]
    public float motorFactor;

    [Header("Injection")]
    [Range(0.0f, 1.0f)]
    public float intensity;

    [Range(0.0f, 1.0f)]
    public float mix;

    public int greatestFrequency;

    public float Remap(float from, float fromMin, float fromMax, float toMin, float toMax)
    {
        var fromAbs = from - fromMin;
        var fromMaxAbs = fromMax - fromMin;

        var normal = fromAbs / fromMaxAbs;

        var toMaxAbs = toMax - toMin;
        var toAbs = toMaxAbs * normal;

        var to = toAbs + toMin;

        return to;
    }

    void Start()
    {
        
    }

    public void Haptic(PlayerIndex gamePlayerIndex, float leftMotor, float rightMotor)
    {
        XInputDotNetPure.PlayerIndex PI = XInputDotNetPure.PlayerIndex.One;

        if (gamePlayerIndex == PlayerIndex.Player1)
        {
            PI = XInputDotNetPure.PlayerIndex.One;
        }

        else if (gamePlayerIndex == PlayerIndex.Player2)
        {
            PI = XInputDotNetPure.PlayerIndex.Two;
        }

        else if (gamePlayerIndex == PlayerIndex.Player3)
        {
            PI = XInputDotNetPure.PlayerIndex.Three;
        }

        else if (gamePlayerIndex == PlayerIndex.Player4)
        {
            PI = XInputDotNetPure.PlayerIndex.Four;
        }


        GamePad.SetVibration(PI, leftMotor, rightMotor);
    }

    void FixedUpdate()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        // Find a PlayerIndex, for a single player game
        // Will find the first controller that is connected ans use it
        if (!playerIndexSet || !prevState.IsConnected)
        {
            for (int i = 0; i < 4; ++i)
            {
                XInputDotNetPure.PlayerIndex testPlayerIndex = (XInputDotNetPure.PlayerIndex)i;
                GamePadState testState = GamePad.GetState(testPlayerIndex);
                if (testState.IsConnected)
                {
                  
                    playerIndex = testPlayerIndex;
                    playerIndexSet = true;
                }
            }
        }

        prevState = state;
        state = GamePad.GetState(playerIndex);

   
        
    }
}
