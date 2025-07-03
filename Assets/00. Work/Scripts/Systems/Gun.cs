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
        Vector3 mousedirection = mouse - transform.position;//마우스포인터 방향 계산
        float angle = Mathf.Atan2(mousedirection.y, mousedirection.x) * Mathf.Rad2Deg; //회전 각도 계산(라디안 각도 반환)
        if (-90 <= angle && angle <= 90)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, angle); //z축으로 회전
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
