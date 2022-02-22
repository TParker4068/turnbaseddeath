using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserLauncher : ProjectileLauncher
{
    public GameObject laserGhost;
    public GameObject laserShot;
    public Transform dir;
    AudioSource shotSFX;

    public int turnsToCharge;
    private int turnsToFire;
    private bool isFiring;
    private Vector3 startScale;
    private Vector3 endScale;
    float distance;


    int ignoreLayer = 1 << 10;
    int tilemapLayer = 1 << 8;
    private Vector2 rayDir;
    private bool fire;
    float timer;



    private void Start()
    {
        ignoreLayer = ~ignoreLayer;
        turnsToFire = turnsToCharge;
        isFiring = false;
        startScale = new Vector3(0.1f, 0.1f, 0.1f);
        endScale = new Vector3(15, 0.1f, 0.1f);
        Vector2 temp = new Vector2(dir.position.x, dir.position.y);
        rayDir = temp - new Vector2(this.transform.position.x, this.transform.position.y);
        fire = false;
        shotSFX = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        Debug.DrawRay(transform.position, rayDir * 10, Color.green);
        if (fire)
        {
            
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                turnsToFire = turnsToCharge;
                laserGhost.SetActive(false);
                laserShot.SetActive(false);
                fire = false;
            }
        }
    }

    public override void TakeAction()
    {
        turnsToFire--;
        if (turnsToFire == 1)
        {
            laserGhost.SetActive(true);
            // set scale using tilemapLayer
            RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDir, Mathf.Infinity, tilemapLayer);
            if (hit.collider != null)
            {
                distance = Mathf.Abs((hit.point.x - (transform.position.x))*rayDir.x + (hit.point.y - (transform.position.y)) * rayDir.y);
                laserGhost.transform.localScale = new Vector3(distance, 0.1f, 0.1f);
                laserGhost.transform.position = new Vector3((transform.position.x + (distance / 2) * rayDir.x), transform.position.y + ((distance / 2) * rayDir.y), transform.position.x) ;

                Debug.Log(hit.collider.name);
            }
        }
        else if (turnsToFire == 0)
        {
            laserGhost.SetActive(false);
            laserShot.SetActive(true);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDir, Mathf.Infinity, ignoreLayer);
            if (hit.collider != null)
            {
                distance = Mathf.Abs((hit.point.x - (transform.position.x)) * rayDir.x + (hit.point.y - (transform.position.y)) * rayDir.y);
                if (hit.collider.tag == "Corpse"|| hit.collider.tag == "Player") 
                {
                    distance += 0.5f;
                }
                laserShot.transform.localScale = new Vector3(distance, 0.1f, 0.1f);
                Debug.Log(hit.point);
                shotSFX.Play(0);
                laserShot.transform.position = new Vector3((transform.position.x + (distance / 2) * rayDir.x), transform.position.y + ((distance / 2) * rayDir.y), transform.position.x); ;
                if (hit.collider.tag == "Player")
                {
                    Debug.Log(hit.collider.gameObject.transform.position);
                    hit.collider.gameObject.GetComponent<playerManager>().TakeLife(true);
                }
                timer = 0.5f;
                fire = true;
                
            }
        }
        //else if (turnsToFire < 0) 
        //{
        //    turnsToFire = turnsToCharge;
        //    laserGhost.SetActive(false);
        //    laserShot.SetActive(false);
        //}
    }
}
