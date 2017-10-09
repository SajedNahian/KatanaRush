using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NinjaGirl : MonoBehaviour {
    private bool ninjaGirlDied = false;
    private static GameObject gameOverPanel;
    [SerializeField]
    private Text reasonForGOText, finalScoreText;
    private WaveSpawnerGround script;
    private Animator anim;

    // Use this for initialization
    void Awake()
    {
        gameOverPanel = GameObject.FindGameObjectWithTag("GameOverPanel");
        script = GameObject.FindGameObjectWithTag("WaveSpawner1").GetComponent<WaveSpawnerGround>();
        anim = GetComponent<Animator>();
    }
    void Start () {
        gameOverPanel.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.tag == "Enemy")
    //    {
    //        ninjaGirlDied = true;
    //        GameOver();
    //    }
    //}

    public void GameOver()
    {
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
        finalScoreText.text = "Score: " + script.getScore();
        if (ninjaGirlDied)
        {
            reasonForGOText.text = "The Ninja Princess Died";
        } else
        {
            reasonForGOText.text = "You lost all your HP";
        }
    }

    public void goBackToMainMenu ()
    {
        Time.timeScale = 1f;
        Application.LoadLevel("mainmenu");
    }

    public void QuitGame ()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }

    public void testDeathPrincess (GameObject whoAttacking)
    {
        Vector3 temp;
        temp = whoAttacking.transform.position;
        if ((Mathf.Abs(temp.x - transform.position.x)) < 1f)
        {
            //Debug.Log("WHAT");
            anim.Play("NinjaGirl_Died");
            ninjaGirlDied = true;
        }
    }

    public void restartGame ()
    {
        Time.timeScale = 1f;
        Application.LoadLevel("gameplay");
    }

    void launchGameOver ()
    {
        GameOver();
    }
}
