using System.Collections;
using UnityEditor;
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
    public bool FaceRight = true;

    //Combate
    private int punchCount = 0;
    private bool comboControl;
    private float timeCross = 1.5f;

    //Ataque especial

    public Special specialPrefab;
    public Transform launchOffSet;

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
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    Instantiate(specialPrefab, launchOffSet.position ,transform.rotation);

        //    PlayerAnimator.SetTrigger("Special");
        //}

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

        // Espada pegável com um trigger
        if (Input.GetKeyDown(KeyCode.F))
        {
            //procura o item em gameobject perto e o destrói
            if (itemPerto)
            {
                Destroy(itemPerto.gameObject);
                
            }
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

    // teleport
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Teleport tp = collision.gameObject.GetComponent<Teleport>();
        Item item = collision.gameObject.GetComponent<Item>();

        if (item != null)
        {
            currentHealth += 5;

            item.gameObject.SetActive(false);
            FindFirstObjectByType<UIManager>().UpdatePlayerHealth(currentHealth);

        }

        if (tp != null)
        {
            float distance = 2.3f;
            transform.position = new Vector2(transform.position.x + distance, transform.position.y);
           
        }

        itemPerto = collision;
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
        yield return new WaitForSeconds(timeCross);
        punchCount = 0;
        comboControl = false;
    }

    void DisablePlayer()
    {
        gameObject.SetActive(false);
    }
}
