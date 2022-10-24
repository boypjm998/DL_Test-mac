using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [Header("Key Settings")]
    public string keyRight = "d";
    public string keyLeft = "a";
    public string keyDown = "s";
    public string keyRoll = "l";
    public string keyJump = "k";
    public string keyAttack = "j";
    

    public MyInputModule buttonRight = new MyInputModule();
    public MyInputModule buttonLeft = new MyInputModule();
    public MyInputModule buttonDown = new MyInputModule();
    public MyInputModule buttonJump = new();
    public MyInputModule buttonRoll = new MyInputModule();
    public MyInputModule buttonAttack = new MyInputModule();

    [Header("Input Selection")]
    public bool inputRollEnabled;
    public bool inputMoveEnabled;
    public bool inputJumpEnabled;
    public bool inputAttackEnabled;

    public enum PlayerStates
    {
        Move = 1,
        Jump = 2,
        Roll = 3,
        Attack = 4,
        ALL = 5
    }




    [Header("Output Signal")]

    public float DRight;
    public float isMove;
    private float velocityRight;
    public float targetDRight;
    public bool jump;
    public bool wjump;
    private int jumptime;
    public bool roll;
    public bool stdAtk;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        buttonLeft.Tick(Input.GetKey(keyLeft));
        buttonRight.Tick(Input.GetKey(keyRight));
        buttonJump.Tick(Input.GetKey(keyJump));
        buttonRoll.Tick(Input.GetKey(keyRoll));
        buttonDown.Tick(Input.GetKey(keyDown));
        buttonAttack.Tick(Input.GetKey(keyAttack));

        CheckMovement();
        CheckJump();
        CheckRoll();
        CheckStdAtk();
        //print(DRight);
    }


    private void CheckMovement()
    {
        if (!inputMoveEnabled)
        {
            targetDRight = 0;
            DRight = 0;
            isMove = 0;
            return;
        }

        targetDRight = (buttonRight.IsPressing ? 1.0f : 0) - (buttonLeft.IsPressing ? 1.0f : 0);
        DRight = Mathf.SmoothDamp(DRight, targetDRight, ref velocityRight, 0.15f);

        if (DRight < 0.35f && DRight > -0.35f)
        {
            isMove = 0;

        }
        else
        {
            isMove = DRight;
        }


    }


    private void CheckJump()
    {

        wjump = buttonJump.OnPressed && jumptime == 1;
        if (wjump)
        {
            jumptime--;
        }



        jump = buttonJump.IsPressing && jumptime == 2;
        if (jump)
        {
            jumptime--;
        }

    }

    private void CheckStdAtk()
    {
        if (!inputAttackEnabled)
        {
            stdAtk = false;
            return;
        }
        stdAtk = buttonAttack.IsPressing;


    }










    public void ResetJumpTime()
    {
        jumptime = 2;
    }

    private void CheckRoll()

    {
        if (!inputRollEnabled)
        {
            roll = false;
            return;
        }

        roll = buttonRoll.IsPressing;
        
    }


    public void SetInputEnabled(string s)
    {
        if (s.Equals("roll"))
        {
            inputRollEnabled = true;
        }
        else if (s.Equals("attack"))
        {
            inputAttackEnabled = true;
        }
        else if (s.Equals("move"))
        {
            inputMoveEnabled = true;
        }
        else if (s.Equals("roll"))
        {
            inputAttackEnabled = true;
        }
        //StartCoroutine(InputEnableAgain(s, 0.3f));

    }

    public void SetInputDisabled(string s)
    {
        if (s.Equals("roll"))
        {
            inputRollEnabled = false;
        }
        else if (s.Equals("attack"))
        {
            inputAttackEnabled = false;
        }
        else if (s.Equals("move"))
        {
            inputMoveEnabled = false;
        }
        else if (s.Equals("roll"))
        {
            inputAttackEnabled = false;
        }
        StartCoroutine(InputEnableAgain(s, 0.3f));

    }


    public IEnumerator InputEnableAgain(string s, float time)
    {
        yield return new WaitForSeconds(time);
        SetInputEnabled(s);
    }

}