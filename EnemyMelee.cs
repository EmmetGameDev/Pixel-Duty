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
    public Animator knifeAnim;
    public float attackCD;

    public Transform attackpoint;
    public float attackRange;
    public LayerMask playerLayer;

    [Header("Health")];
    public int maxHealth;
    private float currentHealth;
    public float armor;
    public AudioSource hitAudioSrc;

    private void Start()
    {
        playerTrans = GameObject.Find("Player").transform;
        currentHealth = maxHealth;
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
        if(distance > distanceBuffer && isAttacking == false)
        {
            myTrans.position = Vector2.MoveTowards(myTrans.position, playerTrans.position, speed * Time.deltaTime);
            myAnim.SetBool("IsMoving", true);
        }
        else
        {
            myAnim.SetBool("IsMoving", false);
            if(isAttacking == false)
            {
                SlashAttack();
            }
        }

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

    public void SlashAttack()
    {
        Collider2D hitPlayer = Physics2D.OverlapCircleAll(attackpoint.position, attackRange, playerLayer);
        if(hitPlayer != null && hitPlayer.GameObject.name == "Player"){
            hitPlayer.GameObject.GetComponent<Health>().HealthDown(1);
        }
        //Animation
        StartCoroutine(SlashAnim());
    }

    IEnumerator SlashAnim()
    {
        isAttacking = true;
        knifeAnim.SetBool("Slash", true);
        yield return new WaitForSeconds(0.5f);
        knifeAnim.SetBool("Slash", false);
        yield return new WaitForSeconds(attackCD);
        isAttacking = false;
        
    }
}
