using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class PauseMenu : MonoBehaviour
{
    public void resume() 
    {
        this.gameObject.SetActive(false);
        // make no user inputs, stop game steps while paused
    }

    public void Reset()
    {
        Debug.Log("reset");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Exit( )
    {
        
    }

}
