using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitmarker : MonoBehaviour{
    private AudioSource src;
    public float soundDur;

    private void Start(){
        src = gameObject.GetComponent<AudioSource>();
        StartCoroutine(WaitForDeletion());
    }

    IEnumerator WaitForDeletion(){
        src.Play();
        yield return new WaitForSeconds(soundDur);
        Destroy(gameObject);
    }
}