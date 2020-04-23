using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeatScript : MonoBehaviour
{
    Animator meat_Anim;

    public List<Sprite> meatImgs;
    public float meatQuantity;
    public float meatTimer;
    public float eatRate;

    float playerCount = 0f;
    Movement player = null;

    float meat;
    float feedTimer;
    bool playerIsOn = false;

    private void Start()
    {
        meat_Anim = GetComponent<Animator>();
        meat = meatQuantity;
    }

    private void Update()
    {
        //update meat sprite
        meat_Anim.SetInteger("meatQuantity", (int)meat);
        //Update feed timer
        if (playerIsOn)
        {
            feedTimer += Time.deltaTime * 1f;
        }
        //Give Player Health
        if (playerIsOn && meat != 0 && player && (feedTimer >= meatTimer))
        {
            
            if((player.Health + eatRate) <= player.maxHealth && playerCount!=0)
            {
                meat -= eatRate/playerCount;
                player.Health += eatRate/playerCount;
                feedTimer = 0;
            }
        }
        //Destroy meat
        if(meat <= 0)
        {
            Debug.Log("Meat has finished");
            Destroy(this.gameObject,1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            playerCount++;
            player = collision.GetComponent<Movement>();
            Debug.Log("Player entered");
            playerIsOn = true;
            feedTimer = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            playerCount--;
            player = null;
            playerIsOn = false;
            feedTimer = 0;
        }
    }



}
