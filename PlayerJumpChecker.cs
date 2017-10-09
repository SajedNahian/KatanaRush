using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpChecker : MonoBehaviour {

    private PlayerMovement script;
	// Use this for initialization
	void Start () {
        script = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.tag == "Ground") || (collision.tag == "EnemyHead"))
        {
            script.LandedOnGround();
        }

    }
}
