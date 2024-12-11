using Unity.VisualScripting;
using UnityEngine;

public class attack_explosivo : Bot_Controller
{
    // essa variavel sera usada para armazenar a posicao do player



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        base.Start();

        base.TakeDamage(damage);
    }

    // Update is called once per frame
    void Update()
    {

        base.Update();

        base.UpdateAnimator();

    }
    
    


}
