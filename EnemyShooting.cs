using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    public Transform myTrans;
    public Animator myAnim;
    public Transform weaponTrans;
    private Transform playerTrans;

    public float distanceBuffer = 2f;
    private float distance;
    public float speed;

    [Header("Attacking")];
    private bool isAttacking = false;
    public float timer;
    private float currentInterval;
    public float attackMaxInterval;
    public float attackMinInterval;
    public int minAttackShots;
    public int maxAttackShots;
    private int currentAttackShots;
    public float fireRateInterval;

    public AudioSource shootSFX;
    public float accuracy;
    public GameObject firePoint;
    public float bulletForce
    public GameObject bulletPrefab;
    public GameObject gunAnim;

    [Header("Health")];
    public int maxHealth;
    private float currentHealth;
    public float armor;
    public AudioSource hitAudioSrc;

    private void Start()
    {
        playerTrans = GameObject.Find("Player").transform;
        myAnim.SetBool("IsMoving", true);
        currentHealth = maxHealth;
        currentInterval = Random.Range(attackMinInterval, attackMaxInterval);
    }

    private void Update()
    {
        //Aiming
        if(isAttacking == false)
        {
            Vector3 aimDir = (playerTrans.position - weaponTrans.position).normalized;
            float angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;
            angle += 180f;
            weaponTrans.eulerAngles = new Vector3(0, 0, angle);

            Vector2 scale = weaponTrans.localScale;
            if (playerTrans.position.x > weaponTrans.position.x)
            {
                scale.y = -1;
            }
            else
            {
                scale.y = 1;
            }
            weaponTrans.localScale = scale;
        }

        //Movement



        distance = Vector2.Distance(transform.position, playerTrans.position);

        myTrans.position = Vector2.MoveTowards(myTrans.position, playerTrans.position, speed * Time.deltaTime);


        if(distance < distanceBuffer && isAttacking == false)
        {            
            if(timer >= currentInterval){
                //Handle Shooting
                isAttacking = true;
                ShootBurst();

                //Reseting timer
                currentInterval = Random.Range(attackMinInterval, attackMaxInterval);
                timer = 0.0f;
            }            
        }
        timer += Time.deltaTime;

        if(myTrans.position.x < playerTrans.position.x)
        {
            myAnim.SetBool("IsWalkingRight", true);
        }
        else
        {
            myAnim.SetBool("IsWalkingRight", false);
        }
    }

    public void HealthDown(int ammount){
        float damage = ammount - (armor * 0.1f);
        //TODO Display the damage
        currentHealth -= damage;
        hitAudioSrc.Play();
        if(currentHealth <= 0){
            Death();
        }
    }

    public void Death(){
        StartCoroutine(DeathAnim());
    }

    //TODO enter animation duration!!!
    IEnumerator DeathAnim(){
        myAnim.SetTrigger("Death");
        yield return new WaitForSeconds(1f);
        Destroy(myTrans.gameObject);
    }


    public void ShootBurst(){
        currentAttackShots = Random.Range(minAttackShots, maxAttackShots);
        StartCoroutine(ShootBurstWaiter());
    }

    IEnumerator ShootBurstWaiter(){
        for(int i = 0; i < currentAttackShots; i++){
            Shoot();
            yield return new WaitForSeconds(fireRateInterval);
        }
        isAttacking = false;
    }

    public void Shoot()
    {
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
        //Animation
        StartCoroutine(ShootAnim());
    }

    IEnumerator ShootAnim()
    {
        gunAnim.SetBool("Shoot", true);
        yield return new WaitForSeconds(0.5f);
        gunAnim.SetBool("Shoot", false);
        yield return new WaitForSeconds(attackCD);
    }
}
