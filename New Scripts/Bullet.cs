using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int damage;
    public GameObject HitMarker;

    private void Start()
    {
        damage = GameObject.Find("Player").GetComponentInChildren<Shooting>().damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            EnemyMelee scr = collision.gameObject.GetComponentInChildren<EnemyMelee>();
            if(scr != null){
                scr.HealthDown(damage);
            }
            else
            {
                //Made for handling other enemy types
            }
            Instantiate(HitMarker, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}