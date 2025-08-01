using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int poolSize = 10;
    private List<GameObject> bulletPool = new List<GameObject>();
    private bool canfire = true;
    private Vector3 mouse;

    private void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            bulletPool.Add(bullet);
        }
    }

    private void Update()
    {
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
        GameObject bullet = GetInactiveBulletFromPool();
        if (bullet == null)
        {
            return;
        }

        bullet.transform.position = transform.position;
        bullet.transform.rotation = transform.rotation;

        Bullet bulletScript = bullet.GetComponent<Bullet>();

        ReadyButton readyBtn = FindFirstObjectByType<ReadyButton>();
        if (readyBtn != null && readyBtn.cardSequence.Count > 0)
        {
            bulletScript.SetCardSequence(readyBtn.cardSequence);
        }

        bullet.SetActive(true);
        bulletScript.Initialize();

        canfire = false;
        StartCoroutine(CoolTime());
    }

    private GameObject GetInactiveBulletFromPool()
    {
        foreach (GameObject bullet in bulletPool)
        {
            if (!bullet.activeInHierarchy)
                return bullet;
        }
        return null;
    }

    private IEnumerator CoolTime()
    {
        yield return new WaitForSeconds(1f);
        canfire = true;
    }
}