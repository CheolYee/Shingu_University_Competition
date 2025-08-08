using _00._Work._02._Scripts.Systems;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private ReadyButton readyButton;
    private GameObject[] bulletPool;
    private Vector3 mouse;

    private bool canfire = true;
    private bool aim = false;

    private void Start()
    {
        bulletPool = new GameObject[1];
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bulletPool[0] = bullet;
        bullet.SetActive(false);

        if (readyButton != null)
        {
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            readyButton.RegisterBullet(bulletScript);
            Debug.Log("[Gun] ReadyButton에 Bullet 등록 완료");
        }
        else
        {
            Debug.LogWarning("[Gun] ReadyButton 참조가 유니티에서 연결되지 않았습니다.");
        }
    }

    private void Update()
    {
        if (Time.timeScale == 0f)
            return;

        if (aim)
            LookAtMouse();
        else if (!aim)
            Aiming();
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
        PlayerAnimation.Instance.FireAnimation();
        StartCoroutine(CoolTime(1));
    }

    private IEnumerator CoolTime(float time)
    {
        yield return new WaitForSeconds(time);
        canfire = true;
    }

    private void Aiming()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            PlayerAnimation.Instance.IdleToAimAnimation();
            aim = true;
        }
    }
}