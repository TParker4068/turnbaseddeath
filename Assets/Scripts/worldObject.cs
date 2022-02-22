using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class worldObject : MonoBehaviour
{
    public bool isMoving = false;
    public virtual void TakeAction() { return; }
}
