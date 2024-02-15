using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemReader : MonoBehaviour
{
    public Item item;
    
    private void Start(){
        item.name = gameObject.name;
        gameObject.GetComponent<SpriteRenderer>().sprite = icon;
        if(gameObject.transform.GetChild(0).gameObject != null && item.doesMakePassPart == true){
            gameObject.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().settings.startColor = item.particleColor;
        }else{
            Destroy(gameObject.transform.GetChild(0).gameObject);
        }
    }
}