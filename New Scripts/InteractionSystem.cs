using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionSystem : MonoBehaviour
{
    public int BodyArmor;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Item")
        {
            switch (collision.gameObject.name)
            {
                case "BodyArmor":
                    BodyArmor++;
                    PickupAnimation(collision.gameObject);
                    break;

            }
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
