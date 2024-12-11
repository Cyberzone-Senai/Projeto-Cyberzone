using System.Collections;
using UnityEngine;
public class Player_Controller : MonoBehaviour
{
    private Vector2 playerDirection;
    private Rigidbody2D playerRB;
    private Animator PlayerAnimator;

    //Movimenta��o
    private float playerspeed = 1f;
    private float currentSpeed;
    private bool walking;
    private bool FaceRight = true;

    //Combate
    private int punchCount = 0;
    private bool comboControl;
    private float TimeCross = 1.5f;

    //vivo ou morto
    private bool isdead;

    //ataque e dano
    private bool isTakeDamage;

    //UI
    public int maxHealth = 10;
    public int currentHealth;
    public Sprite playerImage;

    public int espada;

    private Collider2D itemPerto;

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        PlayerAnimator = GetComponent<Animator>();

        currentSpeed = playerspeed;
        currentHealth = maxHealth;
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
                PlayerAnimator.SetTrigger("Punch");

                if (!comboControl)
                {
                    StartCoroutine(CrossController());
                }

            }
            else if (punchCount >= 3)
            {
                PlayerAnimator.SetTrigger("Cross");
            }

            StopCoroutine(CrossController());

        }

        //Special
        if (Input.GetKeyDown(KeyCode.E))
        {
            PlayerAnimator.SetTrigger("Special");
        }

        //Dash
        if (Input.GetKeyDown(KeyCode.Q))
        {
            PlayerAnimator.SetTrigger("Dash");
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

        // Espada peg�vel com um trigger
        if (Input.GetKeyDown(KeyCode.F))
        {
            //procura o item em gameobject perto e o destr�i
            if (itemPerto)
            {
                Destroy(itemPerto.gameObject);
                
            }


        }

    }

    private void FixedUpdate()
    {

        //Movimenta��o
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

    public void TakeDamage(int damage)
    {
        if (!isdead)
        {
            isTakeDamage = true;
            currentHealth -= damage;

            PlayerAnimator.SetTrigger("HitDamage");
            FindFirstObjectByType<UIManager>().UpdatePlayerHealth(currentHealth);

            if (currentHealth <= 0)
            {
                isdead = true;
                ZeroSpeed();
                PlayerAnimator.SetTrigger("Dead");            
            }
        }
    }

    void Flip()
    {
        FaceRight = !FaceRight;

        transform.Rotate(0, 180, 0);

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

    void DisablePlayer()
    {
        gameObject.SetActive(false);
    }

    // mostra que o itemPerto � aquele que tem a colis�o junto ao player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        itemPerto = collision;
        
    }

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    itemPerto = false;
    //}
}
