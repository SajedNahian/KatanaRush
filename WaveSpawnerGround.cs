using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawnerGround : MonoBehaviour {
    [SerializeField]
    private GameObject groundEnemy, shurikenEnemy;
    private float randInt;
    private float whenCanSpawn;
    private GameObject player;
    private PlayerMovement script;
    private float min, max;
    private float whenToMakeLevelHarder;
    [SerializeField]
    private Text scoreText, waveText;
    private int waveNum = 1;
    private float startTime;
    private int score;
    // Use this for initialization
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        script = player.GetComponent<PlayerMovement>();
        min = 8f;
        max = 12f;
        whenToMakeLevelHarder = Time.time + 20f;
        waveText.text = "Wave: " + waveNum.ToString();
        scoreText.text = "Score: 0";
        startTime = Time.time;
    }
    void Start () {
        randInt = Random.Range(1, 5);
        whenCanSpawn = Time.time + randInt;
	}
	
	// Update is called once per frame
	void Update () {
		if ((Time.time > whenCanSpawn) && (script.hp > 0))
        {
            Spawn();
            randInt = Random.Range(min, max);
            whenCanSpawn = Time.time + randInt;
        }

        if (Time.time > whenToMakeLevelHarder)
        {
            if ((min >= 2) && (min + 2 < max))
            {
                int x = Random.Range(1, 3);
                if ((x == 1) && (min >= 2))
                {
                    min -= 1;
                } else if (min + 2 < max)
                {
                    max -= 1;
                }
            } else if ((min >= 2))
            {
                min -= 1;
            } else if ((min + 2 < max))
            {
                max -= 1;
            }
            whenToMakeLevelHarder = Time.time + 20f;
            waveNum += 1;
            waveText.text = "Wave: " + waveNum.ToString();
        }
        if (script.hp > 0)
        {
            score = Mathf.FloorToInt(Time.time - startTime);
        }
        scoreText.text = "Score: " + score.ToString();
	}


    void Spawn()
    {
        if (Random.Range(1, 4) == 1)
        {
            Instantiate(shurikenEnemy, transform.position, transform.rotation);
        }
        else
        {
            Instantiate(groundEnemy, transform.position, transform.rotation);
        }
        //Instantiate(shurikenEnemy, transform.position, transform.rotation);
        //Instantiate(groundEnemy, transform.position, transform.rotation);
    }

    public int getScore ()
    {
        return score;
    }
}
