using UnityEngine;

public class Camera : MonoBehaviour
{

    private Animator cameraAnimation;

    void Start()
    {
        cameraAnimation = GetComponent<Animator>();
    }

    void Update()
    {
        cameraAnimacao();
    }

    void cameraAnimacao()
    {
        cameraAnimation.SetTrigger("Ativar");
    }
}
