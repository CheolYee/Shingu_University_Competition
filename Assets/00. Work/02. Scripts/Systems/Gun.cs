using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    private GameObject[] bulletPool;
    private bool canfire = true;
    private Vector3 mouse;

    private void Start()
    {
        bulletPool = new GameObject[1];
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bulletPool[0] = bullet;
        bullet.SetActive(false);
    }

    private void Update()
    {
        if (Time.timeScale == 0f)
            return;

        LookAtMouse();
    }

    private void LookAtMouse()
    {
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mousedirection = mouse - transform.position;
        float angle = Mathf.Atan2(mousedirection.y, mousedirection.x) * Mathf.Rad2Deg;

        if (-90 <= angle && angle <= 90)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
            if (Keyboard.current.spaceKey.wasPressedThisFrame && canfire)
            {
                Shot();
            }
        }
    }

    private void Shot()
    {
        GameObject bullet = bulletPool[0];
        if (bullet.activeSelf || !canfire)
            return;

        bullet.transform.position = transform.position;
        bullet.SetActive(true);
        canfire = false;
        StartCoroutine(CoolTime());
    }

    private IEnumerator CoolTime()
    {
        yield return new WaitForSeconds(1f);
        canfire = true;
    }
}