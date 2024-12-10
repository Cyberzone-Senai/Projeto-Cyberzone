using UnityEngine;

public class Teleport : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public Transform Destination;
    public Transform player;
    public void OnTriggerEnter(Collider other)
    {
        player.transform.position = Destination.transform.position;
    }
}
