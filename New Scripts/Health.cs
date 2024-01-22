using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int maxPlates = 2;
    public int maxHealth;
    public int currentHealth;

    public float iFramesDur;
    private bool isInvinc;
    public Animator playerAnim;
    public AudioSource hitSoundSrc;

    //TODO implement handling more than 2 plates

    [Header("UI Animations")]
    public Image plate1;
    public Image plate2;
    public Image healthBar;

    [Header("Plating")]
    public KeyCode plateupKey;
    public GameObject plateupIndicator;
    public GameObject uiPlateAnim;
    public float platingDur;
    public float platingSlow;
    private float timer;
    public bool isPlating = false;
    public Sprite[] plateUISprites;
    private int plateBeingLoaded;
    public GameObject playerMain;
    private float initialSpeed;

    private void Start()
    {
        maxHealth = maxPlates + 1;
        currentHealth = maxHealth;
    }

    public void UpdateHealthVisual()
    {
        if(currentHealth == maxHealth)
        {
            plate1.gameObject.GetComponent<Animator>().SetBool("IsFull", true);
            plate2.gameObject.GetComponent<Animator>().SetBool("IsFull", true);
        }
        if(currentHealth == 2)
        {
            plate1.gameObject.GetComponent<Animator>().SetBool("IsFull", true);
            plate2.gameObject.GetComponent<Animator>().SetBool("IsFull", false);
        }
        if(currentHealth == 1)
        {
            plate1.gameObject.GetComponent<Animator>().SetBool("IsFull", false);
            plate2.gameObject.GetComponent<Animator>().SetBool("IsFull", false);
        }
    }

    public void HealthDown(int ammount)
    {
        if (isInvinc == false)
        {
            currentHealth -= ammount;
            hitSoundSrc.Play();
            UpdateHealthVisual();
            if (currentHealth > 0)
            {
                StartCoroutine(IFrames(iFramesDur));
            }
            else
            {
                //TODO Game over
            }
        }
    }

    IEnumerator IFrames(float duration)
    {
        isInvinc = true;
        playerAnim.SetBool("Hit", true);
        yield return new WaitForSeconds(duration);
        playerAnim.SetBool("Hit", false);
        isInvinc = false;
    }


    //Handling Plating up
    private void Update()
    {
        if (Input.GetKeyDown(plateupKey) && isPlating == false && currentHealth != maxHealth)
        {
            PlateUp();
        }
        if (isPlating == true)
        {
            if (timer >= platingDur)
            {
                currentHealth++;
                //TODO Make it use up one plate from interaction script
                playerMain.GetComponent<Movement>().speed = initialSpeed;
                playerMain.GetComponent<Movement>().weaponTrans.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                isPlating = false;
                UpdateHealthVisual();
            }
            timer += Time.deltaTime;
        }
    }

    public void PlateUp()
    {
        isPlating = true;
        if(currentHealth < 3)
        {
            plateBeingLoaded = currentHealth;
        }
        initialSpeed = playerMain.GetComponent<Movement>().speed;
        playerMain.GetComponent<Movement>().speed = initialSpeed - platingSlow;
        playerMain.GetComponent<Movement>().weaponTrans.gameObject.GetComponent<SpriteRenderer>().enabled = false;

    }

    IEnumerator PlatingAnimUI()
    {
        for (int i = 0; i < 14; i++)
        {
            if(isPlating == true)
            {
                if (plateBeingLoaded == 2)
                {
                    plate2.sprite = plateUISprites[i];
                }
                if (plateBeingLoaded == 1)
                {
                    plate1.sprite = plateUISprites[i];
                }
            }
            else
            {
                if(plateBeingLoaded == 2)
                {
                    plate2.sprite = plateUISprites[0];
                }
                if (plateBeingLoaded == 1)
                {
                    plate1.sprite = plateUISprites[0];
                }
            }
            yield return new WaitForSeconds(platingDur / 14);
        }
    }
}