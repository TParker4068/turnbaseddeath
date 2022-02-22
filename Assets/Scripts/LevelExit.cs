using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelExit : worldObject
{
    public Text VictoryDebugText;
    public GameObject VictoryUILayer;
    Collider2D collisionTest;
    private playerManager player;
    private int par;
    public string nextLevel;
    // Start is called before the first frame update
    void Start()
    {
        VictoryDebugText.text = "";
        player = FindObjectOfType<playerManager>();
        par = player.DeathPar;
    }

    // Update is called once per frame
    void Update()
    {
      
        collisionTest = Physics2D.OverlapCircle(this.transform.position, .1f);
        if (collisionTest != null)
        {
            if (collisionTest.gameObject.tag == "Player"&&collisionTest.gameObject.name=="Player")
            {
                VictoryUILayer.SetActive(true);
                VictoryDebugText.text = "You Win";
                if (player.deaths < par) { VictoryDebugText.text += "\n well done birdy"; }
                else if (player.deaths == par) { VictoryDebugText.text += "\n good enough Par"; }
                else { VictoryDebugText.text += "\n try harder bogey"; }
            }
        }
    }

    public override void TakeAction()
    {
        collisionTest = Physics2D.OverlapCircle(this.transform.position, .1f);
        if (collisionTest != null)
        {
            if (collisionTest.gameObject.tag == "Player")
            {
                VictoryDebugText.text = "You Win";
                if (player.deaths < par) { VictoryDebugText.text += "\n well done birdy"; }
                else if (player.deaths == par) { VictoryDebugText.text += "\n good enough Par"; }
                else { VictoryDebugText.text += "\n try harder bogey"; }
            }
        }
    }

    public void LoadNextLevel() 
    {
        SceneManager.LoadScene(nextLevel);
    }
}
