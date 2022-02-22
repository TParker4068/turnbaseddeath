using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityObject : MonoBehaviour
{

    int layerMask = 1 << 7;
    int ghostLayer = 1 << 10;
    public bool isMoving;
    float timeElapsed;
    float lerpDuration = 2f;
    Transform lerpStart;
    Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        layerMask += ghostLayer;
        layerMask = ~layerMask;
        isMoving = false;
        lerpStart = transform;
    }

    // Update is called once per frame
    void Update()
    {
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, Mathf.Infinity, layerMask);
        //Debug.DrawRay(transform.position, -Vector2.up*10, Color.green);
        if (isMoving)
        {
            if (transform.position == target)
            {
                isMoving = false;
                timeElapsed = 0;
                //Debug.Log(transform.position);
            }
            else
            {
                if (timeElapsed < lerpDuration)
                {
                    transform.position = Vector3.Lerp(lerpStart.position, target, timeElapsed / lerpDuration);
                    timeElapsed += Time.deltaTime;
                }
            }
        }
    }

    public void ApplyGravity()
    {
        if (!isMoving)
        {
            int Dis = distanceToFall();
            if (Dis > 0)
            {
                //this.transform.position += new Vector3(0, -Dis, 0);
                lerpStart.position = transform.position;
                target = lerpStart.position + new Vector3(0, -Dis, 0);
                //Debug.Log(target);
                isMoving = true;

            }
        }
    }

    int distanceToFall() 
    {
        //Debug.Log("player falling");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, Mathf.Infinity, layerMask);
        if (hit.collider != null)
        {
            Debug.Log(hit.collider.gameObject.name);
            float distance = Mathf.Abs(hit.point.y - (transform.position.y));
            if (hit.collider.tag == "Death"&&this.gameObject.name=="Player")
                {
                this.GetComponent<playerManager>().TakeLife();
                    return 0; 
                }
            return ((int) (distance));
        }
        return 0;
        
    }
}
