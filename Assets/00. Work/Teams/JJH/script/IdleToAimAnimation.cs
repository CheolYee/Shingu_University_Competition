using UnityEngine;

public class IdleToAimAnimation : MonoBehaviour
{
    public void IdleToAim()
    {
        PlayerAnimation.Instance.Upperani.SetBool("Aiming", true);
        PlayerAnimation.Instance.Lowerani.SetBool("Aiming", true);
    }

    public void FireQuit()
    {
        PlayerAnimation.Instance.Upperani.SetBool("Fire", false);
        PlayerAnimation.Instance.Lowerani.SetBool("Fire", false);
    }
}
