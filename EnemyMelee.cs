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

    private bool isAttacking = false;
    public Animator knifeAnim;
    public float attackCD;

    private void Start()
    {
        playerTrans = GameObject.Find("Player").transform;
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

    public void SlashAttack()
    {
        //TODO make attak deal damage

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
