using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerManager : MonoBehaviour
{
    Vector3 startPos;
    float temp;
    worldManager man;
    private bool actionTaken;
    Vector3 lastPos;
    bool canMove;
    private bool dead;
    public bool isMoving;
    float timeElapsed;
    float lerpDuration = 3f;
    Transform lerpStart;
    [Header("UI")]

    public Text DeathText;
    public Text CantMoveWarningText;
    public Text parText;
    [Header("Collision")]

    public Transform right_detection;
    public Transform left_detection;

    [Header("death system")]
    public int deathStrokeout;
    public int deaths;
    public int DeathPar;
    public GameObject DeathParticle;
    public GameObject Ghost;
    public GameObject DeadBody;


    // Start is called before the first frame update
    void Start()
    {
        man = FindObjectOfType<worldManager>();
        actionTaken = false;
        lastPos = transform.position;
        startPos = transform.position;
        deaths = 0;
        canMove = false;
        dead = false;
        isMoving = false;
        parText.text = "Deaths Par: " + DeathPar.ToString();
        GetComponent<Animator>().SetBool("IsMoving", false);
    }

    // Update is called once per frame
    void Update()
    {
        Ghost.transform.position = new Vector3(Ghost.transform.position.x, transform.position.y, Ghost.transform.position.z);
        if (man.isPlayerTurn) 
        {
            if (!isMoving)
            {
                if (!actionTaken)
                {

                    temp = Input.GetAxis("Horizontal");
                    if (temp > 0)
                    {
                        temp = 1;
                        canMove = CanMove(right_detection);
                    }
                    if (temp < 0)
                    {
                        temp = -1;
                        canMove = CanMove(left_detection);
                    }

                    if (canMove)
                    {
                        Ghost.transform.position += new Vector3(temp, 0, 0);
                        Ghost.transform.localScale = new Vector3(temp * Mathf.Abs(Ghost.transform.localScale.x), Ghost.transform.localScale.y, Ghost.transform.localScale.z);
                    }
                    else
                    {
                        //StartCoroutine(CantMove());
                    }

                    if (temp != 0 && canMove)
                    {
                        actionTaken = true;
                    }
                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.Return)) // enter key
                    {
                        lerpStart = transform;
                        //transform.position = Ghost.transform.position;
                        //lastPos = transform.position;
                        Debug.Log("player moved");
                        isMoving = true;
                        canMove = false;
                        if (temp == 1)
                        {
                            this.GetComponent<SpriteRenderer>().flipX = false;
                        }
                        else 
                        {
                            this.GetComponent<SpriteRenderer>().flipX = true;
                        }
                        //transform.localScale = new Vector3(temp*Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

                        GetComponent<Animator>().SetBool("IsMoving", true);

                    }
                    else if (Input.GetKeyDown(KeyCode.Backspace)) // backspace
                    {
                        Ghost.transform.position = lastPos;
                        actionTaken = false;
                        canMove = false;
                    }

                }
            }
            else if (isMoving) 
            {
                
                if (transform.position == Ghost.transform.position)
                {
                    man.isPlayerTurn = false;
                    isMoving = false;
                    timeElapsed = 0;
                    lastPos = transform.position;
                    GetComponent<Animator>().SetBool("IsMoving", false);
                }
                else 
                {
                    if (timeElapsed < lerpDuration)
                    {
                        //GetComponent<Animator>().SetBool("IsMoving", true);
                        transform.position = Vector3.Lerp(lerpStart.position, Ghost.transform.position, timeElapsed / lerpDuration);
                        timeElapsed += Time.deltaTime;
                    }
                }
            }
        }
    }

    //currently does not work
    IEnumerator CantMove()
    {
        Debug.Log("coroutine started");
        CantMoveWarningText.text = "Invalid Move";

        yield return new WaitForSeconds(.5f);

        CantMoveWarningText.text = "";
        Debug.Log("coroutine ended");

    }

    public void startTurn() 
    {
        actionTaken = false;
        man.isPlayerTurn = true;
    }

    public void Reset()
    {
        this.transform.position = startPos;
        Ghost.transform.position = startPos;
        lastPos = startPos;
    }

    public void TakeLife(bool leaveBody = false) 
    {
        Instantiate(DeathParticle, transform.position, transform.rotation);

        deaths += 1;
        if (deaths >= deathStrokeout) 
        {
            //restart level
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (leaveBody)
        {
            Instantiate(DeadBody,transform.position-new Vector3(0f,0.0f,0f),transform.rotation);
        }
        DeathText.text = "Deaths used: " + deaths.ToString();
        Reset();
    }

    private bool CanMove(Transform moveTo)
    {
        Collider2D collisionTest;
        collisionTest = Physics2D.OverlapCircle(moveTo.position, .1f, 7 << 8);
        if (collisionTest != null)
        {
            if (collisionTest.gameObject.tag == "Corpse")
            {
                return true;
            }else
            {
                return false;
            }
            
        } 
        else
        {
            return true;
        }
    }


}
