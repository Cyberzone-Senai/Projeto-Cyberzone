using UnityEngine;

public class Attack : MonoBehaviour
{
    public int damage;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //colis�o player
        Player_Controller player = collision.GetComponent<Player_Controller>();

        //colis�o inimigo
        Bot_Controller bot = collision.GetComponent<Bot_Controller>();
        


        if (bot != null)
        {
            bot.TakeDamage(damage);
        }

        if (player != null)
        {
            player.TakeDamage(damage);
        }
    }
}
