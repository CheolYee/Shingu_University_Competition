using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public static PlayerAnimation Instance = null;
    public Animator Upperani;
    public Animator Lowerani;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        Upperani = transform.GetChild(0).GetComponent<Animator>();
        Lowerani = transform.GetChild(1).GetComponent<Animator>();
    }


    public void IdleToAimAnimation()
    {
        Upperani.SetBool("IdleToAim", true);
        Lowerani.SetBool("IdleToAim", true);
    }

    public void FireAnimation()
    {
        Upperani.SetBool("Fire", true);
        Lowerani.SetBool("Fire", true);
    }
}
