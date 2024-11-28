using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    private Vector2 playerDirection;
    private Rigidbody2D playerRB;
    private float playerspeed = 1f;

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        playerMove();
    }

    private void FixedUpdate()
    {
        //if (playerDirection.x != 0 || playerDirection.y != 0)
        //{
        //    isWalking = true;
        //}
        //else
        //{
        //    isWalking = false;
        //}

        playerRB.MovePosition(playerRB.position + playerspeed * Time.fixedDeltaTime * playerDirection);
    }

    void playerMove()
    {
        playerDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
}
