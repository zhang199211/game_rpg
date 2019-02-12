using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [Header("====  Key setting  ====")]
    //public string keyUp="w";
    //public string keyDown="s";
    //public string keyLeft="a";
    //public string keyRight="d";
    private KeyCode keyUp=KeyCode.W;
    private KeyCode keyDown = KeyCode.S;
    private KeyCode keyLeft = KeyCode.A;
    private KeyCode keyRight = KeyCode.D;
    private KeyCode keyA = KeyCode.LeftShift;
    private KeyCode keyB = KeyCode.J ;
    private KeyCode keyC = KeyCode.A;
    private KeyCode keyD = KeyCode.D;

    [Header("====  Output signals  ====")]
    public float Dup;
    public float Dright;
    [Header("向量长度")]
    public float Dmag;
    //模型向前向量
    public Vector3 DVec;

    // 1.pressing signals
    public bool run;
    // 2.trigger once signals;
    public bool jump;
    private bool lastJump;
    // 3.double trigger


    [Header("====  Others  ====")]
    public bool inputEnabled = true;
    private float targetDup;
    private float targetDright;
    private float velocityDup;
    private float velocityDright;
     
    private void Update()
    {
        targetDup = (Input.GetKey(keyUp) ? 1.0f : 0f)-(Input.GetKey(keyDown)?1.0f:0f);
        targetDright = (Input.GetKey(keyRight) ? 1.0f : 0f) - (Input.GetKey(keyLeft) ? 1.0f : 0f);
        if (!inputEnabled)
        {
            targetDup = 0;
            targetDright = 0;
        }
        Dup = Mathf.SmoothDamp(Dup,targetDup,ref velocityDup,0.1f);
        Dright = Mathf.SmoothDamp(Dright, targetDright, ref velocityDright, 0.1f);

        Vector2 tempDAxis = SquareToCirle(new Vector2(Dup, Dright));
        float Dup2 = tempDAxis.y;
        float Dright2 = tempDAxis.x;


        Dmag = Mathf.Sqrt((Dup2 * Dup2) + (Dright2 * Dright2));
        DVec = Dright2 * transform.right + Dup2 * transform.forward;

        run = Input.GetKey(keyA);

        bool newJump=Input.GetKey(keyB);
        if (newJump !=lastJump&& newJump == true)
        {
            jump = true;
        }
        else
        {
            jump = false;
        }
        lastJump = newJump;

    }

    private Vector2 SquareToCirle(Vector2 input)
    {
        Vector2 output = Vector2.zero;
        output.x = input.x * Mathf.Sqrt(1 - (input.y * input.y) / 2.0f);
        output.y = input.y * Mathf.Sqrt(1 - (input.x * input.x) / 2.0f);
        return output;
    }

}
