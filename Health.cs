using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour{
    public int maxPlates;
    public int maxHealth = 1;
    private int currentHealth;

    public float iFramesDur;
    private bool isInvinc;
    public Animator playerAnim;
    public AudioSource hitSoundSrc;

    //TODO implement handling more than 2 plates

    [Header("UI Animations")];
    public Image plate1;
    public Image plate2;
    public Image healthBar;

    private void Start(){
        maxHealth = maxPlates + 1;
    }

    public void UpdateHealthVisual(){
        switch(currentHealth){
            case maxHealth:
                plate1.GameObject.GetComponent<Animator>().SetBool("IsFull", true);
                plate2.GameObject.GetComponent<Animator>().SetBool("IsFull", true);
                break;
            case 2:
                plate1.GameObject.GetComponent<Animator>().SetBool("IsFull", true);
                plate2.GameObject.GetComponent<Animator>().SetBool("IsFull", false);
                break;
            case 1:
                plate1.GameObject.GetComponent<Animator>().SetBool("IsFull", false);
                plate1.GameObject.GetComponent<Animator>().SetBool("IsFull", false);
                break;
        }
    }

    public void HealthDown(int ammount){
        if(isInvinc == false){
            currentHealth -= ammount;
            hitSoundSrc.Play();
            UpdateHealthVisual();
            if(currentHealth > 0){
                StartCoroutine(IFrames(iFramesDur));
            }else{
                //TODO Game over
            }
        }
    }

    IEnumerator IFrames(float duration){
        isInvinc = true;
        playerAnim.SetBool("Hit", true);
        yield return new WaitForSeconds(duration);
        playerAnim.SetBool("Hit", false);
        isInvinc = false;
    }
}