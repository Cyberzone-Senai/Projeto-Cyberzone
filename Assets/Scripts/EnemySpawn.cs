using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private float minSpawnTime;
    [SerializeField] private float maxSpawnTime;
     private float timerSpawn;

    void awake()
    {
        timer();
        
    }

    void Update()
    {
        timerSpawn -= Time.deltaTime;

        if (timerSpawn <= 0)
        {
            Instantiate(enemy, transform.position, Quaternion.identity);
            timer();
        }
    }

   private void timer()
    {
        timerSpawn = Random.Range(minSpawnTime, maxSpawnTime);
        


    }
}
