using UnityEngine;

public class LixeiraController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;

    public bool isDead;

    public int maxHealth;
    public int currentHealth;

    public bool isTakingDamage;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        currentHealth = maxHealth;
        
    }

    void Update()
    {
        
    }

    public void TakeDamage (int  damage)
    {
        if (!isDead)
        {
            isTakingDamage = true;
            currentHealth -= damage;

            animator.SetTrigger("Lixeira_Quebrada");
        }
    }

    public void DisableEnemy()
    {
        this.gameObject.SetActive(false);
    }
}
