using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    private Vector3 mouse;

    private void Update()
    {
        LookAtMouse();
    }

    private void LookAtMouse()
    {
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mousedirection = mouse - transform.position;//���콺������ ���� ���
        float angle = Mathf.Atan2(mousedirection.y, mousedirection.x) * Mathf.Rad2Deg; //ȸ�� ���� ���(���� ���� ��ȯ)
        if (-90 <= angle && angle <= 90)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, angle); //z������ ȸ��
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                Shot();
            }
        }
    }

    private void Shot()
    {
        GameObject bullt = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
    }
}
