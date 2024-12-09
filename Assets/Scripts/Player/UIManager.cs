using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider playerHealthBar;
    public Image playerImage;

    private Player_Controller player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GetComponent<Player_Controller>();

        playerHealthBar.maxValue = player.maxHealth;

        playerHealthBar.value = playerHealthBar.maxValue;

        playerImage.sprite = player.playerImage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdatePlayerHealth(int amount)
    {
        playerHealthBar.value = amount;

    }
}
