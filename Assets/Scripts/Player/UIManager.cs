using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider playerHealthBar;
    public Image playerImage;

    public GameObject enemyUI;
    public Slider enemyHealthBar;

    //Objet o para armazenar estados do player
    private Player_Controller Player;
    //Timers e controles do enemyUI
    public float enemyUITimer = 4f;
    private float enemyTimer;

    private Player_Controller player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindAnyObjectByType<Player_Controller>();

        playerHealthBar.maxValue = player.maxHealth;

        playerHealthBar.value = playerHealthBar.maxValue;

        //playerImage.sprite = player.playerImage;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void UpdatePlayerHealth(int amount)
    {
        playerHealthBar.value = amount;

    }

    public void UpdateenemyUI(int maxHealth, int currentHealth, Sprite image)
    {

        //Atualiza os dados do inimigo de acordo com o inimigo atacado
        enemyHealthBar.maxValue = maxHealth;
        enemyHealthBar.value = currentHealth;

        //Zera o timer para começar a contar os 4 segundos
        enemyTimer = 0;
        //Habilida a enemyUI, deixando-a visível
        enemyUI.SetActive(true);

    }
}


