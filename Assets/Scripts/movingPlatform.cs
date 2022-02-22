using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlatform : worldObject
{
    public GameObject[] movementPath;
    public GameObject Ghost;

    bool movingForwards;
    int currentPositionIndex;

    float timeElapsed;
    float lerpDuration = .4f;
    Vector3 tempRot;
    Transform lerpStart;
    Vector3 target;
    // Start is called before the first frame update
    private void Start()
    {
        movingForwards = true;
        currentPositionIndex = 0;
        this.transform.position = movementPath[currentPositionIndex].transform.position;
        Ghost.transform.position = movementPath[currentPositionIndex + 1].transform.position;
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            if (transform.position == target)
            {
                isMoving = false;
                timeElapsed = 0;
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


    public override void TakeAction()
    {
        if (!isMoving)
        {
            lerpStart = transform;
            if (movingForwards)
            {
                //transform.rotation = Quaternion.Euler(0, 0, 0);
                currentPositionIndex++;
                //this.transform.position = movementPath[currentPositionIndex].transform.position;
                target = movementPath[currentPositionIndex].transform.position;
                isMoving = true;
                if (currentPositionIndex >= movementPath.Length - 1)
                {
                    movingForwards = false;
                    tempRot = transform.localScale;
                    tempRot.x *= -1;
                    transform.localScale = tempRot;
                    Ghost.transform.position = movementPath[currentPositionIndex - 1].transform.position;
                }
                else
                {
                    Ghost.transform.position = movementPath[currentPositionIndex + 1].transform.position;
                }
            }
            else if (!movingForwards)
            {
                //transform.rotation = Quaternion.Euler(0, 0, 180);
                currentPositionIndex--;
                //this.transform.position = movementPath[currentPositionIndex].transform.position;
                target = movementPath[currentPositionIndex].transform.position;
                isMoving = true;
                if (currentPositionIndex <= 0)
                {
                    movingForwards = true;
                    tempRot = transform.localScale;
                    tempRot.x *= -1;
                    transform.localScale = tempRot;
                    Ghost.transform.position = movementPath[currentPositionIndex + 1].transform.position;
                }
                else
                {
                    Ghost.transform.position = movementPath[currentPositionIndex - 1].transform.position;
                }
            }
        }
    }
}
