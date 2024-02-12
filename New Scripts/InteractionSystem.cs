using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionSystem : MonoBehaviour
{
    [Header("General Scripts")]
    public Movement moveScr;

    [Header("Body Armor")]
    public int BodyArmor;

    [Header("Coffee")]
    public int Coffee;
    public float additMS = 0.25f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            switch (collision.gameObject.name)
            {
                //Here handle all items

                case "BodyArmor":
                    BodyArmor++;
                    break;
                case "Coffee":
                    Coffee++;
                    moveScr.speed += additMS;
                    break;
            }
            PickupAnimation(collision.gameObject);
        }
    }

    public void PickupAnimation(GameObject obj)
    {
        obj.GetComponent<AudioSource>().Play();
        obj.GetComponent<ParticleSystem>().Play();
        obj.GetComponent<Collider2D>().enabled = false;
        obj.GetComponent<SpriteRenderer>().enabled = false;
        StartCoroutine(waitForAnimation(obj));
    }

    IEnumerator waitForAnimation(GameObject objDe)
    {
        yield return new WaitForSeconds(1f);
        objDe.SetActive(false);
    }
}
