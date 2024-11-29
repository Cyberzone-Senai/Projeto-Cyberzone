using System.Collections;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    private Vector2 playerDirection;
    private Rigidbody2D playerRB;
    private float playerspeed = 1f;
    private Animator PlayerAnimator;
    private bool walking;
    private bool FaceRight = true;
    private int PunchCount = 0;
    private bool comboControl;
    private float TimeCross = 1.5f;

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        PlayerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        playerMove();
        Updateanimator();


        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (PunchCount < 3)
            {
                PunchCount++;
                PlayerPunch();

                if (!comboControl)
                {
                    StartCoroutine(CrossController());
                }

            }
            else if (PunchCount >= 3)
            {
                PlayerCross();
            }

            StopCoroutine(CrossController());

        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            PlayerSpecial();
        } 
        if (Input.GetKeyDown(KeyCode.Q))
        {
            PlayerDash();
        }

        //Correr

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            PlayerRun();

        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            PlayerRun();
            playerspeed = 1f;
        }

    }

    private void FixedUpdate()
    {
        if (playerDirection.x != 0 || playerDirection.y != 0)
        {
            walking = true;
        }
        else
        {
            walking = false;
        }

        playerRB.MovePosition(playerRB.position + playerspeed * Time.fixedDeltaTime * playerDirection);
    }

    void Updateanimator()
    {
        PlayerAnimator.SetBool("Walking", walking);
    }

    void playerMove()
    {
        playerDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (playerDirection.x > 0 && !FaceRight)
        {
            Flip();
        }
        else if (playerDirection.x < 0 && FaceRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        FaceRight = !FaceRight;

        transform.Rotate(0, 180, 0);

    }

    void PlayerPunch()
    {
        PlayerAnimator.SetTrigger("Punch");
    }

    void PlayerCross()
    {
        PlayerAnimator.SetTrigger("Cross");
    }

    void PlayerSpecial()
    {
        PlayerAnimator.SetTrigger("Special");
    }

    void PlayerDash()
    {
        PlayerAnimator.SetTrigger("Dash");
    }

    void PlayerRun()
    {
        playerspeed += 0.25f;
        PlayerAnimator.SetTrigger("Run");
    }

    IEnumerator CrossController()
    {
        comboControl = true;

        yield return new WaitForSeconds(TimeCross);
        PunchCount = 0;

        comboControl = false;
    }

}
