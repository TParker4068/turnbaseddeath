using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSwitchButton : Button
{
    public GameObject[] blocks;
    public Sprite active;
    public Sprite deactive;

    bool[] blocksWhenPressed;
    bool[] blocksWhenReleased;

    private void Start()
    {
        blocksWhenPressed = SetBlocksWhenPressed();
        blocksWhenReleased = SetBlocksWhenReleased();
        clickSFX = GetComponent<AudioSource>();
    }

    private bool[] SetBlocksWhenReleased()
    {
        int i = 0;
        bool[] blocksActive = new bool[blocks.Length];
        foreach (GameObject obj in blocks)
        {
            if (obj.GetComponent<Collider2D>())
            {
                if (obj.GetComponent<Collider2D>().enabled == true)
                {
                    blocksActive[i] = true;
                    i++;
                }
                else
                {
                    blocksActive[i] = false;
                    i++;
                }
            }
            else if (obj.GetComponent<Animator>())
            {
                if (obj.GetComponent<Animator>().enabled == true)
                {
                    blocksActive[i] = true;
                    i++;
                }
                else
                {
                    blocksActive[i] = false;
                    i++;
                }
            }
        }
        return blocksActive;
    }

    private bool[] SetBlocksWhenPressed()
    {
        int i = 0;
        bool[] blocksActive = new bool[blocks.Length];
        foreach (GameObject obj in blocks)
        {
            if (obj.GetComponent<Collider2D>())
            {
                if (obj.GetComponent<Collider2D>().enabled == true)
                {
                    blocksActive[i] = false;
                    i++;
                }
                else
                {
                    blocksActive[i] = true;
                    i++;
                }
            } 
            else if (obj.GetComponent<Animator>())
            {
                if (obj.GetComponent<Animator>().enabled == true)
                {
                    blocksActive[i] = false;
                    i++;
                }
                else
                {
                    blocksActive[i] = true;
                    i++;
                }
            }
            
        }
        return blocksActive;
    }

    public override void ButtonPressed()
    {
        int i = 0;
        foreach (GameObject b in blocks)
        {
            //blocks[i].SetActive(blocksWhenPressed[i]);
            SpriteRenderer renderer = b.GetComponent<SpriteRenderer>();
            
            if (b.GetComponent<BoxCollider2D>()) b.GetComponent<BoxCollider2D>().enabled = blocksWhenPressed[i];
            if (blocksWhenPressed[i])
            {
                renderer.sprite = active;
                b.GetComponent<SpriteRenderer>().color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 255);
            }
            else
            {
                renderer.sprite = deactive;
                b.GetComponent<SpriteRenderer>().color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, .56f);
            }

            if (b.tag == "Door")
            {
                if (b.GetComponent<BoxCollider2D>()) { b.GetComponent<BoxCollider2D>().enabled = true; }
                renderer.sprite = active;
                if (b.GetComponent<Animator>())
                {
                    b.GetComponent<Animator>().enabled = true;
                }
                if (b.GetComponent<JumpPad>())
                {
                    b.GetComponent<JumpPad>().enabled = true;
                }
            }

            i++;
        }
    }

    public override void ButtonReleased()
    {
        int i = 0;
        foreach (GameObject b in blocks)
        {
            SpriteRenderer renderer = b.GetComponent<SpriteRenderer>();
            
            if (b.GetComponent<BoxCollider2D>()) b.GetComponent<BoxCollider2D>().enabled = blocksWhenReleased[i];
            if (blocksWhenReleased[i])
            {
                renderer.sprite = active;
                b.GetComponent<SpriteRenderer>().color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 255);
            }
            else 
            {
                renderer.sprite = deactive; 
                b.GetComponent<SpriteRenderer>().color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, .56f);
            }
            //blocks[i].SetActive(blocksWhenReleased[i]);

            if (b.tag == "Door")
            {
                if(b.GetComponent<Animator>())
                    { b.GetComponent<Animator>().enabled = false; }
                if (b.GetComponent<BoxCollider2D>()) { b.GetComponent<BoxCollider2D>().enabled = false; }
                renderer.sprite = deactive;
                if (b.GetComponent<JumpPad>())
                {
                    b.GetComponent<JumpPad>().enabled = false;
                }
            }


            i++;
        }
    }
}
