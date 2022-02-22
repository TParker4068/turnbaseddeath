using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScoll : MonoBehaviour
{
    // Start is called before the first frame update

    playerManager myPlayer;



    public int minCamX;
    public int maxCamX;

    void Start()
    {
        myPlayer = FindObjectOfType<playerManager>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (myPlayer.transform.position.x <= minCamX)
        {
            transform.position = new Vector3(minCamX, transform.position.y, transform.position.z);
        }
        else if (myPlayer.transform.position.x >= maxCamX)
        {
            transform.position = new Vector3(maxCamX, transform.position.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(myPlayer.transform.position.x, transform.position.y, transform.position.z);
        }
    }
}
