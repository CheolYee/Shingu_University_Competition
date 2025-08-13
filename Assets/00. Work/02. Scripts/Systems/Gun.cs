using _00._Work._02._Scripts.Systems;
using _00.Work.Scripts.Managers;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private ReadyButton readyButton;
    private GameObject bulletr;
    private GameObject[] bulletPool;
    private Magnazine magnazine;
    private SpriteRenderer sprite;

    [SerializeField] private Transform bulletPosition;
    private Vector3 mouse;
    private Vector3 startPos;

    private float followRadius = 1.5f;

    private bool canfire = true;
    private bool aim = false;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        magnazine = GameObject.FindWithTag("Magnazine").GetComponent<Magnazine>();
    }

    private void Start()
    {
        SoundManager.Instance.PlayBgm("StageBGM");
        sprite.enabled = false;
        startPos = transform.position;

        bulletPool = new GameObject[1];
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bulletr = bullet;
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


        if (!bulletr.activeSelf)
        {
            bulletr.transform.position = bulletPosition.transform.position;
            TimeWall.Instance?.DoReset();
            SwitchWall.Instance?.gameObject.SetActive(true);
            magnazine.gameObject.SetActive(true);
        }
    }

    private void LookAtMouse()
    {
        sprite.enabled = true;

        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0f;
        Vector3 dir = mouse - startPos;
        float distance = dir.magnitude;
        if (distance > followRadius)
        {
            dir = dir.normalized * followRadius;
        }
        transform.position = startPos + dir;
        Vector3 mouseDir = mouse - transform.position;
        float angle = Mathf.Atan2(mouseDir.y, mouseDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        if (Keyboard.current.spaceKey.wasPressedThisFrame && canfire)
        {
            Shot();
        }
    }

    private void Shot()
    {
        SoundManager.Instance.PlaySfx("Shot");
        TimeWall.Instance?.DoTime();
        GameObject bullet = bulletPool[0];
        if (bullet.activeSelf || !canfire)
            return;
        bullet.transform.position = bulletPosition.position;
        bullet.transform.rotation = transform.rotation;
        bullet.SetActive(true);
        canfire = false;
        PlayerAnimation.Instance.FireAnimation();
        StartCoroutine(CoolTime());
    }

    private IEnumerator CoolTime()
    {
        SoundManager.Instance.PlaySfx("Reload");
        yield return new WaitForSeconds(2f);
        canfire = true;
    }

    private void Aiming()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            PlayerAnimation.Instance.IdleToAimAnimation();
            StartCoroutine(AimTime());
        }
    }

    private IEnumerator AimTime()
    {
        yield return new WaitForSeconds(2f);
        aim = true;
    }
}