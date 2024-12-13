using System;
using Unity.Mathematics;
using UnityEngine;

public class Bot_Controller : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;

    //posição player
    private Transform target;

    //morrrer
    public bool isdead;

    //virar o inimigo para o player
    public bool facingRight;
    public bool previousDirectionRight;

    //movimentação
    // protected serve para torna as variaveis publicas, mas somente para classes
    protected bool walk;
    private float walkTimer;
    private float enemySpeed = 0.4f;
    private float currentSpeed;
    private float horizontalForce;
    private float verticalForce;

    //ataque
    private float attackRate = 1f;
    private float nextAttack;

    //mecanica de dano
    public int damage;
    public int maxHealth = 5;
    public int currentHealth;
    public float staggerTime = 0.5f;
    private int damageTimer;
    public bool isTakeDamage;
    
    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        target = FindAnyObjectByType<Player_Controller>().transform;

        currentSpeed = enemySpeed;
        currentHealth = maxHealth;
    }

    public void Update()
    {
        //virar o inimigo
        if (target.position.x < transform.position.x)
        {
            facingRight = true;
        }
        else
        {
            facingRight = false;
        }

        if (facingRight && !previousDirectionRight)
        {
            transform.Rotate(0,180,0);
            previousDirectionRight = true;
        }else if (!facingRight && previousDirectionRight)
        {
            transform.Rotate(0,180,0);
            previousDirectionRight = false;
        }

        //iniciar timer caminhar
        walkTimer += Time.deltaTime;

        //gerenciar animação
        if (horizontalForce == 0&& verticalForce == 0)
        {
            walk = false;
        }
        else
        {
            walk = true;
        }

        UpdateAnimator();

    }

    private void FixedUpdate()
    {
        if (!isdead)
        {
            //movimentação 
            //variavel para armazenar a distancia entre o inimigo e o player

            Vector3 targetDistance = target.position - transform.position;

            horizontalForce = targetDistance.x / Mathf.Abs(targetDistance.x);
            //verticalForce = targetDistance.y / Mathf.Abs(targetDistance.y);

            //entre 1 e 2 segundos será feita uma definição de direção vertical
            if (walkTimer >= UnityEngine.Random.Range(1f, 2f))
            {
                verticalForce = UnityEngine.Random.Range(-1, 2);

                walkTimer = 0;
            }

            //caso esteja perto do player, parar a movimentação

            if (Mathf.Abs(targetDistance.x) < 0.2f)
            {
                horizontalForce = 0;
            }

            //aplica a velocidade no inimigo fazendo movimentar

            rb.linearVelocity = new Vector2(horizontalForce * currentSpeed, verticalForce * currentSpeed);

            //Attack
            //se estiver perto do player e o timer do jogo for maior que o valor de nextAttack 

            if (MathF.Abs(targetDistance.x) < 0.2f && Mathf.Abs(targetDistance.y) < 0.05f && Time.time > nextAttack)
            {
                //executa o ataque do inimigo 
                animator.SetTrigger("Attack");

                ZeroSpeed();

                //pega o tempo atual e soma o attackrate, para definir a partir de quando o inimigo poderá atacar novamente
                nextAttack = Time.time + attackRate;

            }
        }
    }

    public void UpdateAnimator()
    {
        animator.SetBool("Walk", walk);
    }

    public void TakeDamage(int damage)
    {
        if (!isdead)
        {
            isTakeDamage = true;
            currentHealth -= damage;

            animator.SetTrigger("Hit");

            if (currentHealth <= 0)
            {
                isdead = true;
                ZeroSpeed();
                animator.SetTrigger("Dead");            
            }
        }
    }

    protected void ZeroSpeed()
    {
        currentSpeed = 0;
    }

    protected void ResetSpeed()
    {
        currentSpeed = enemySpeed;
    }

    public void DisableEnemy()
    {
        gameObject.SetActive(false);
    }
}
