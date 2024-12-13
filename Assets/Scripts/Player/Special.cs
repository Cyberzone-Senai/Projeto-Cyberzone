using UnityEngine;

public class Special : MonoBehaviour
{
    public float specialSpeed = 4f;
    private Player_Controller player;
    void Update()
    {

        if (player.FaceRight == false)
        {
            transform.position += transform.right * Time.deltaTime * specialSpeed;
        }
        else if (player.FaceRight)
        {
            transform.position += -transform.right * Time.deltaTime * specialSpeed;

        }

        //transform.position += -transform.right * Time.deltaTime * specialSpeed;


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
