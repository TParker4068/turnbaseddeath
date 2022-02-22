using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : worldObject
{
    BoxCollider2D boxCollider;
    playerManager player;
    bool hasCorpse;
    AudioSource stabSFX;

    public Transform upper_detection;
    public Sprite spikeDeath;
    public Sprite spriteDefault;

    private void Start()
    {
        player = FindObjectOfType<playerManager>();
        hasCorpse = false;
        stabSFX = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (hasCorpse) { return; }
        Collider2D collisionTest;
        collisionTest = Physics2D.OverlapCircle(upper_detection.position, .1f);
        if (collisionTest != null)
        {
            //Debug.Log("spike found object");
            if (collisionTest.gameObject.name == "ghostPlayer")
            {
                Debug.Log("spike found ghost");
                this.GetComponent<SpriteRenderer>().sprite = spikeDeath;
                this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);

            }
            
        }
        else
        {
            this.GetComponent<SpriteRenderer>().sprite = spriteDefault;
            this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }
    }

    public override void TakeAction()
    {
        if (hasCorpse)
        {
            return;
        }
        //Debug.Log("spike action taken");
        Collider2D collisionTest;
        collisionTest = Physics2D.OverlapCircle(upper_detection.position, .1f);
        if (collisionTest != null)
        {
           //Debug.Log("spike found object");
           if (collisionTest.gameObject.tag == "Player")
           {
                Debug.Log("spike found player");
                this.GetComponent<SpriteRenderer>().sprite = spikeDeath;
                this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                player.TakeLife();
                stabSFX.Play(0);
                hasCorpse = true;
           }

           if (collisionTest.gameObject.tag == "Corpse")
            {
                Debug.Log("spike found a corpse");
                this.GetComponent<SpriteRenderer>().sprite = spikeDeath;
                this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                collisionTest.gameObject.SetActive(false);
                stabSFX.Play(0);
                hasCorpse = true;
            }
        }
    }
}
