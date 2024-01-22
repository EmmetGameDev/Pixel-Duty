using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [Header("Customizable")]
    public int damage;
    public string gun;
    public int maxBullets = 6;
    public float reloadTime;
    public float accuracy;

    [Header("Rest")]
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce = 20f;
    private int curBullets;
    private bool isWaiting;
    public float fireRateDuration = 0.5f;
    public Animator reloadingAnim;
    public GameObject reloadCircle;
    private bool isReloading = false;
    public SpriteRenderer pistolRend;
    public AudioSource reloadSFX;
    public AudioSource shootSFX;
    public Sprite[] reloadSprites;

    public GameObject mainPlayer;

    public Sprite spIdle;
    public Sprite spShoot;

    private void Start()
    {
        curBullets = maxBullets;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (curBullets > 0)
            {
                FireRateWait();
            }
            else
            {
                pistolRend.sprite = spShoot;
                Reload();
            }
        }
    }

    public void Reload()
    {
        if (isReloading == false)
        {
            StartCoroutine(ReloadAnim());
            isReloading = true;
        }
    }

    IEnumerator ReloadAnim()
    {
        reloadSFX.Play();
        pistolRend.sprite = spShoot;
        reloadCircle.SetActive(true);
        ReloadAnimation(reloadTime);
        yield return new WaitForSeconds(reloadTime);
        reloadCircle.SetActive(false);
        pistolRend.sprite = spIdle;
        curBullets = maxBullets;
        isReloading = false;
    }

    public void ReloadAnimation(float duration)
    {
        reloadCircle.gameObject.SetActive(true);
        StartCoroutine(WaitReload(duration / reloadSprites.Length));
    }

    IEnumerator WaitReload(float time)
    {
        for (int i = 0; i < reloadSprites.Length; i++)
        {
            reloadCircle.GetComponent<SpriteRenderer>().sprite = reloadSprites[i];
            yield return new WaitForSeconds(time);
        }
        reloadCircle.gameObject.SetActive(false);
    }

    public void FireRateWait()
    {
        if (isWaiting == false)
        {
            Shoot();
            StartCoroutine(WaitEnumerator());
            isWaiting = true;
        }
    }

    IEnumerator WaitEnumerator()
    {
        pistolRend.sprite = spShoot;
        yield return new WaitForSeconds(fireRateDuration);
        pistolRend.sprite = spIdle;
        isWaiting = false;
    }

    public void Shoot()
    {
        mainPlayer.GetComponent<Health>().isPlating = false;
        shootSFX.Play();
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        float randomized = Random.Range(accuracy, accuracy * -1);
        Quaternion oldrot = firePoint.transform.rotation;
        Quaternion newrot = firePoint.transform.rotation;
        newrot.w += randomized;
        newrot.z += randomized;
        firePoint.transform.rotation = newrot;
        bullet.transform.rotation = newrot;
        rb.AddForce(firePoint.right * -1 * bulletForce, ForceMode2D.Impulse);
        firePoint.transform.rotation = oldrot;
        curBullets -= 1;
    }
}
