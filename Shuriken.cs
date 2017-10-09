using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour {
    private float speed = 5.4f;
    private bool moveRight;
    private PlayerMovement script;
    // Use this for initialization
    void Awake () {
		if (transform.position.x < 0)
        {
            moveRight = true;
        } else
        {
            moveRight = false;
        }

        script = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update () {
		if (moveRight)
        {
            Vector3 temp = transform.position;
            Vector3 temp2 = transform.localScale;
            temp2.x = 1;
            transform.localScale = temp2;
            temp.x += speed * Time.deltaTime;
            transform.position = temp;
        } else
        {
            Vector3 temp = transform.position;
            Vector3 temp2 = transform.localScale;
            temp2.x = -1;
            transform.localScale = temp2;
            temp.x -= speed * Time.deltaTime;
            transform.position = temp;
        }
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            script.shurikenHit();
            Destroy(this.gameObject);
        }
    }
}
