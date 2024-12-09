using System.Collections;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    private Vector2 playerDirection;
    private Rigidbody2D playerRB;
    private Animator PlayerAnimator;

    //Movimentação
    private float playerspeed = 1f;
    private float currentSpeed;
    private bool walking;
    private bool FaceRight = true;

    //Combate
    private int punchCount = 0;
    private bool comboControl;
    private float TimeCross = 1.5f;

    //UI
    public int maxHealth = 10;
    public int currentHealth;
    public Sprite playerImage;
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        PlayerAnimator = GetComponent<Animator>();

        currentSpeed = playerspeed;
    }

    void Update()
    {
        playerMove();
        Updateanimator();

        //Attack
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (punchCount < 3)
            {
                punchCount++;
                PlayerPunch();

                if (!comboControl)
                {
                    StartCoroutine(CrossController());
                }

            }
            else if (punchCount >= 3)
            {
                PlayerCross();
            }

            StopCoroutine(CrossController());

        }

        //Special
        if (Input.GetKeyDown(KeyCode.E))
        {
            PlayerSpecial();
        }

        //Dash
        if (Input.GetKeyDown(KeyCode.Q))
        {
            PlayerDash();
            currentSpeed += 3f;

        }
        else if (Input.GetKeyUp(KeyCode.Q))
        {
            currentSpeed = playerspeed;


        }
        //Correr

        if (Input.GetKeyDown(KeyCode.LeftShift) && walking)
        {
            PlayerRun();

        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            PlayerRun();
            currentSpeed = playerspeed;

            PlayerAnimator.SetBool("Walking", walking);
        }

    }

    private void FixedUpdate()
    {

        //Movimentação
        if (playerDirection.x != 0 || playerDirection.y != 0)
        {
            walking = true;
        }
        else
        {
            walking = false;

        }

        playerRB.MovePosition(playerRB.position + currentSpeed * Time.fixedDeltaTime * playerDirection);
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
        currentSpeed += 0.40f;
        PlayerAnimator.SetTrigger("Run");
    }

    void ZeroSpeed()
    {
        currentSpeed = 0;
    }

    void ResetSpeed()
    {
        currentSpeed = playerspeed;
    }

    IEnumerator CrossController()
    {
        comboControl = true;
        yield return new WaitForSeconds(TimeCross);
        punchCount = 0;
        comboControl = false;
    }
}
