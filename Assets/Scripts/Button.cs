using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public AudioSource clickSFX;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Corpse")
        {
            Debug.Log("Button Pressed by " + collision.gameObject.name);
            ButtonPressed();
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Button Released");
        ButtonReleased();
        clickSFX.Play(0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        clickSFX.Play(0);
    }

    virtual public void ButtonPressed() { return; }

    virtual public void ButtonReleased() { return; }

}
