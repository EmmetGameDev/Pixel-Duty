using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int damage;
    public GameObject HitMarker;
    public bool isEnemy;

    private void Start()
    {
        damage = GameObject.Find("Player").GetComponentInChildren<Shooting>().damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!isEnemy){
            if (collision.gameObject.tag.Equals("Enemy")){
                EnemyMelee scr = collision.gameObject.GetComponentInChildren<EnemyMelee>();
                if(scr != null){
                    scr.HealthDown(damage);
                }
                else
                {
                    //Made for handling other enemy types
                    EnemyShooting scr2 = collision.gameObject.GetComponentInChildren<EnemyShooting>();
                    if(scr2){
                        scr2.HealthDown(damage);
                    }
                }
                Instantiate(HitMarker, transform.position, Quaternion.identity);
            }
        }else{
            if(collision.gameObject.tag.Equals("Player")){
                Health scr = collision.gameObject.GetComponentInChildren<Health>();
                if(scr){
                    scr.HealthDown(1);
                }
            }
        }
        Destroy(gameObject);
    }
}