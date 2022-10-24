using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour
{

    public PlayerInput pi;
    Rigidbody2D rigid;
    public Animator anim;



    public int facedir;

    public float movespeed;
    public float rollspeed;
    public float jumpforce;


    [Header("Action Condition")]
    public bool rollEnabled;
    public bool moveEnabled;
    public bool jumpEnabled;
    public bool attackEnabled;



    // Start is called before the first frame update
    private void Awake()
    {
        pi = GetComponent<PlayerInput>();
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        facedir = 1;
        rigid.transform.eulerAngles = new Vector3(0, 0, 0);

        movespeed = 6.0f;

        jumpEnabled = true;
        moveEnabled = true;
        rollEnabled = true;
        attackEnabled = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (pi.jump)
        {
            Jump();
        }
        if (pi.wjump)
        {
            DoubleJump();
        }
        if (pi.roll)
        {
            Roll();
        }
        if (pi.stdAtk)
        {
            Attack();
        }
    }

    private void FixedUpdate()
    {
        Move();

    }




    private void Move()
    {
        if (!moveEnabled)
            return;

        anim.SetFloat("forward", Mathf.Abs(pi.DRight));
        //rigid.position += new Vector2(movespeed * (pi.isMove), 0) * Time.fixedDeltaTime;
        rigid.velocity = new Vector3(movespeed*pi.DRight, rigid.velocity.y);
        CheckFaceDir();
        

    }

    private void Jump()
    {
        if(jumpEnabled)
            anim.SetBool("jump",true);

    }
    private void DoubleJump()
    {
        if(jumpEnabled)
            anim.SetBool("wjump", true);

    }
    private void Roll()
    {
        if(rollEnabled)
            anim.SetBool("roll", true);
    }
    private void Attack()
    {
        if (attackEnabled)
            anim.SetBool("attack", true);
    }



    private void CheckFaceDir()
    {
        if (pi.DRight > 0.05f)
        {
            rigid.transform.eulerAngles = new Vector3(0, 0, 0);
            facedir = 1;
        }
        else if (pi.DRight < -0.05f)
        {
            rigid.transform.eulerAngles = new Vector3(0, 180, 0);
            facedir = -1;
        }
    }


    public void RollEvent()
    {
        //if (pi.buttonLeft.IsPressing && !pi.buttonRight.IsPressing)
        //{
        //    SetFaceDir(-1);
        //}
        //else if (!pi.buttonLeft.IsPressing && pi.buttonRight.IsPressing)
        //{
        //    SetFaceDir(1);
        //}
        StartCoroutine(HorizontalMove(rollspeed*facedir, 0.4f));

    }


    public void DashEvent()
    {
        StartCoroutine(HorizontalMove2(3 * rollspeed, 10f, 0.1f));
    }

    public void InputClearBoolSignal(string s)
    {
        anim.SetBool(s, false);
    }

    public void InputClearFloatSignal(string s)
    {
        anim.SetFloat(s, 0);
    }









    private void IsGround()
    {
        anim.SetBool("isGround", true);
        pi.ResetJumpTime();
        
    }

    private void IsNotGround()
    {
        anim.SetBool("isGround", false);
    }

    private void OnJumpEnter()
    {
        rigid.velocity = new Vector2(rigid.velocity.x, jumpforce);
        anim.SetBool("jump", false);
    }

    private void OnDoubleJumpEnter()
    {
        rigid.velocity = new Vector2(rigid.velocity.x, jumpforce);
        anim.SetBool("wjump", false);
    }

    private void OnRollEnter()
    {
        SetAttackDisabled();
        SetJumpDisabled();
        SetMoveDisabled();
    }
    private void OnRollExit()
    {
        SetAttackEnabled();
        SetJumpEnabled();
        SetMoveEnabled();
        anim.SetBool("roll", false);
    }

    private void OnDashEnter()
    {
        SetAttackDisabled();
        SetJumpDisabled();
        SetMoveDisabled();
        SetRollDisabled();
        //anim.SetBool("attack", true);
    }

    private void OnDashExit()
    {
        SetAttackEnabled();
        SetJumpEnabled();
        SetMoveEnabled();
        SetRollEnabled();
        anim.SetBool("attack", false);
    }

    private void OnStandardAttackEnter()
    {
        pi.SetInputDisabled("move");
        SetJumpDisabled();
        SetMoveDisabled();
        
    }

    private void OnStandardAttackExit()
    {
        SetJumpEnabled();
        SetMoveEnabled();
    }


    public void SetRollEnabled()
    {
        rollEnabled = true;
    }
    public void SetRollDisabled()
    {
        rollEnabled = false;
    }
    public void SetJumpEnabled()
    {
        jumpEnabled = true;
    }
    public void SetJumpDisabled()
    {
        jumpEnabled = false;
    }

    public void SetMoveEnabled()
    {
        moveEnabled = true;
    }
    public void SetMoveDisabled()
    {
        moveEnabled = false;
    }
    public void SetAttackEnabled()
    {
        attackEnabled = true;
    }
    public void SetAttackDisabled()
    {
        attackEnabled = false;
    }

    public void SetFaceDir(int dir)
    {
        if (dir == 1)
        {
            rigid.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (dir == -1) {
            rigid.transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    public void SetVelocity(float vx, float vy)
    {
        rigid.velocity = new Vector2(vx, vy);

    }

    private IEnumerator HorizontalMove(float speed, float time)
    {
        while(time>0)
        {
        SetVelocity(speed, rigid.velocity.y);
        time -= Time.fixedDeltaTime;
        yield return new WaitForFixedUpdate();
        }


        SetVelocity(rigid.velocity.x, rigid.velocity.y);
    }

    private IEnumerator HorizontalMove2(float speed, float acceleration, float time)
    {
        while (time > 0)
        {
            transform.position = new Vector2(transform.position.x + transform.right.x * speed * Time.fixedDeltaTime,
                transform.position.y);
            time -= Time.fixedDeltaTime;

            speed -= acceleration * Time.fixedDeltaTime;

            yield return new WaitForFixedUpdate();
        }

    }

}
