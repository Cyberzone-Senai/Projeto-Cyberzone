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
        //colisão player
        Player_Controller player = collision.GetComponent<Player_Controller>();

        //colisão inimigo
        Bot_Controller bot = collision.GetComponent<Bot_Controller>();

        LixeiraController lixeira = collision.GetComponent<LixeiraController>();
        


        if (bot != null)
        {
            bot.TakeDamage(damage);
        }

        if (player != null)
        {
            player.TakeDamage(damage);
        }

        if(lixeira != null)
        {
            lixeira.TakeDamage(damage);
        }
    }
}
