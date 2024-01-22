using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitmarker : MonoBehaviour{
    public AudioSource src;
    public float soundDur;

    private void Start(){
        StartCoroutine(WaitForDeletion());
    }

    IEnumerator WaitForDeletion(){
        src.Play();
        yield return new WaitForSeconds(soundDur);
        Destroy(gameObject);
    }
}