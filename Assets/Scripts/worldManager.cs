using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class worldManager : MonoBehaviour
{
    public bool isPlayerTurn = true;
    public worldObject[] worldObjects;
    public float turnTime;
    
    public JumpPad[] jumpPads;

    playerManager player;
    private GravityObject[] gObjects;
    private ProjectileLauncher[] pObjects;
    private Spikes[] spikes;    
    private float timer;
    private int turnIndex;
    private bool check;
    int turnNum;
    int Counter;
    private void Start()
    {
        player = FindObjectOfType<playerManager>();
        gObjects = FindObjectsOfType<GravityObject>();
        pObjects = FindObjectsOfType<ProjectileLauncher>();
        spikes = FindObjectsOfType<Spikes>();
        timer = turnTime;
        turnIndex = 0;
        turnNum = 0;
        Counter = 0;
        check = false;
    }

    private void FixedUpdate()
    {
        if (!isPlayerTurn&&(player.isMoving ==false))
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                switch (turnIndex)
                {
                    case 0:
                        if (worldObjects.Length > 0)
                        {
                            check = false;
                            foreach (worldObject wo in worldObjects)
                            {
                                if (Counter == turnNum)
                                {
                                    wo.TakeAction();
                                }
                                Debug.Log("has taken an action");
                                check = check || wo.isMoving;
                            }
                            Counter++;
                            if (!check)
                            {
                                timer = turnTime;
                                turnIndex = 1;
                                Counter = turnNum + 1;
                            }
                        }
                        else 
                        {
                            timer = turnTime;
                            turnIndex = 1;
                            Counter = turnNum + 1;
                        }
                        break;
                    case 1:
                        check = false;
                        foreach (GravityObject go in gObjects)
                        {
                            go.ApplyGravity();
                            check = check || go.isMoving;
                        }
                        if (!check)
                        {
                            timer = turnTime;
                            turnIndex = 2;
                        }
                        break;
                    case 2:
                        if (jumpPads.Length > 0)
                        {
                            foreach (JumpPad jp in jumpPads)
                            {
                                jp.TakeAction();
                            }
                        }
                        turnIndex = 3;
                        timer = turnTime;
                        break;
                    case 3:
                        if (pObjects.Length > 0)
                        {
                            foreach (ProjectileLauncher po in pObjects)
                            {
                                po.TakeAction();
                            }
                            timer = turnTime;
                        }
                        turnIndex = 4;
                        timer = turnTime;
                        break;
                    case 4:
                        if (spikes.Length > 0)
                        {
                            foreach (Spikes s in spikes)
                            {
                                s.TakeAction();
                            }
                            timer = turnTime;
                        }

                        turnIndex = 0;
                        turnNum++;
                        player.startTurn();
                        break;
                    default:
                        break;
                }
                
            }
            
            
        }
    }

    public void turn()
    {
        //if (!isPlayerTurn) 
        //{
        //    foreach (worldObject wo in worldObjects)
        //    {
        //        wo.TakeAction();
        //        Debug.Log("has taken an action");
        //    }
        //    foreach (GravityObject go in gObjects)
        //    {
        //        go.ApplyGravity();
        //    }
        //    foreach (ProjectileLauncher po in pObjects)
        //    {
        //        po.TakeAction();
        //    }
        //    isPlayerTurn = true;
        //    player.startTurn(); 
        //}
    }
}
