using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : worldObject
{
    public Transform detection;
    public int jumpPadValue;
    public int layerValue;

    LayerMask layerMask;
   


    // Start is called before the first frame update
    void Start()
    {
        layerMask = 1 << layerValue;
        layerMask = ~layerMask;
    }
    void FixedUpdate()
    {
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, Mathf.Infinity, layerMask);
        Debug.DrawRay(transform.position, Vector2.up * jumpPadValue, Color.green);
    }
    public override void TakeAction()
    {

        int Dis = DistanceToJump();
        Collider2D collisionTest;
        collisionTest = Physics2D.OverlapCircle(detection.position, .1f);
        if (collisionTest != null)
        {
            Debug.Log("collision test is not null");
            if (collisionTest.gameObject.tag == "Player")
            {
                Debug.Log("collisionTest is a player");
                collisionTest.gameObject.transform.position += new Vector3(0, Dis, 0);
            }
        }else
        {
            Debug.Log("collisionTest is null");
        }
    }

    private int DistanceToJump()
    {
        Debug.Log("calculating jump height");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, jumpPadValue, layerMask);
        if (hit.collider != null)
        {
            Debug.Log(hit.collider.gameObject.name);
            float distance = Mathf.Abs(hit.point.y - (transform.position.y));
            if (hit.collider.tag == "Death" && this.gameObject.name == "Player")
            {
                this.GetComponent<playerManager>().TakeLife();
                return 0;
            }
            return ((int)(distance));
        } else
        {
            return jumpPadValue;
        }
    }

    private void Update()
    {
        /*Collider2D collisionTest;
        collisionTest = Physics2D.OverlapCircle(detection.position, .1f);
        if (collisionTest != null)
        {
            if (collisionTest.gameObject.name == "ghostPlayer")
            {
                collisionTest.gameObject.transform.position += new Vector3(0, jumpPadValue, 0);
            }
        }*/
    }
}
