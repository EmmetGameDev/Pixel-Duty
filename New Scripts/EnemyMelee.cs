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

    [Header("Attacking")]
    public Animator knifeAnim;
    private bool isAttacking = false;
    public float attackCD;

    public Transform attackpoint;
    public float attackRange;
    public LayerMask playerLayer;

    [Header("Health")]
    public int maxHealth;
    public float deathDuration;
    public AudioSource deathSrc;
    private SpriteRenderer knifeRend;
    private float currentHealth;
    public float armor;

    private void Start()
    {
        playerTrans = GameObject.Find("Player").transform;
        knifeRend = gameObject.GetComponentInChildren<SpriteRenderer>();
        currentHealth = maxHealth;
    }

    private void Update()
    {
        //Aiming

        if (isAttacking == false)
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
        if (distance > distanceBuffer && isAttacking == false)
        {
            myTrans.position = Vector2.MoveTowards(myTrans.position, playerTrans.position, speed * Time.deltaTime);
            myAnim.SetBool("IsMoving", true);
        }
        else
        {
            
            if (isAttacking == false)
            {
                SlashAttack();
            }
        }

        if (myTrans.position.x < playerTrans.position.x)
        {
            myAnim.SetBool("IsWalkingRight", true);
        }
        else
        {
            myAnim.SetBool("IsWalkingRight", false);
        }
    }

    public void HealthDown(int ammount)
    {
        float damage = ammount - (armor * 0.1f);
        //TODO Display the damage
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        StartCoroutine(DeathAnim());
    }

    IEnumerator DeathAnim()
    {
        myAnim.SetTrigger("Death");
        isAttacking = true;
        knifeRend.enabled = false;
        deathSrc.Play();
        yield return new WaitForSeconds(deathDuration);
        Destroy(myTrans.gameObject);
    }

    public void SlashAttack()
    {
        if (isAttacking == false)
        {
            myAnim.SetBool("IsMoving", false);

            Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackpoint.position, attackRange, playerLayer);
            if (hitPlayer != null)
            {
                isAttacking = true;
                hitPlayer[0].gameObject.GetComponent<Health>().HealthDown(1);
            }
            StartCoroutine(SlashAnim());
        }
    }

    IEnumerator SlashAnim()
    {
        knifeAnim.SetBool("Slash", true);
        yield return new WaitForSeconds(0.5f);
        knifeAnim.SetBool("Slash", false);
        yield return new WaitForSeconds(attackCD);
        isAttacking = false;

    }
}
