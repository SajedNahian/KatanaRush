using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingEnemy : MonoBehaviour {
    private float speed = 3f;
    private Rigidbody2D myBody;
    private Animator anim;
    private int hp = 2;
    [SerializeField]
    private GameObject player, shuriken, whereToShootFrom;
    private float whenCanAttack;
    PlayerMovement script;
    private bool attacking;
    private bool addedCoin = false;
    private float whenToShoot;
    private GameObject ninjaGirl;
    private NinjaGirl script2;
    // Use this for initialization
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        myBody = GetComponent<Rigidbody2D>(); //or use [SerializeField]
        anim = GetComponent<Animator>();
        whenCanAttack = Time.time;
        script = player.GetComponent<PlayerMovement>();
        whenToShoot += Time.time + .5f;
        ninjaGirl = GameObject.FindGameObjectWithTag("NGirl");
        script2 = ninjaGirl.GetComponent<NinjaGirl>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((this.hp > 0) && (script.hp > 0) && (!attacking))
        {
            if (transform.position.x > 0)
            {
                EnemyMoveLeft();
            }
            else if (transform.position.x < 0)
            {
                EnemyMoveRight();
            }
        }
        if (this.hp <= 0)
        {
            Death();
        }
        Vector3 temp = player.transform.position;
        if (((Mathf.Abs(temp.x - transform.position.x)) < 1.2f) && ((Mathf.Abs(temp.y - transform.position.y)) < .5f))
        {
            if ((Time.time > whenCanAttack) && (script.hp > 0))
            {
                Attack();
            }
        }

        if (!attacking && Time.time > whenToShoot)
        {
            anim.SetTrigger("BladeThrowAttack");
            whenToShoot += Time.time + Random.Range(1.5f, 3f);
        }
        if ((Mathf.Abs(transform.position.x - ninjaGirl.transform.position.x)) < 1.2f)
        {
            attackPrincessNinja();
        }
    }

    void attackPrincessNinja()
    {
        attacking = true;
        anim.SetTrigger("SwordAttack");
        whenCanAttack = Time.time + 1.4f;
        attacking = false;
    }

    void Shoot()
    {
        Instantiate(shuriken, whereToShootFrom.transform.position, whereToShootFrom.transform.rotation);
    }

    void Attack()
    {
        attacking = true;
        if (player.transform.position.x >= transform.position.x)
        {
            Vector3 temp = transform.localScale;
            temp.x = 1;
            transform.localScale = temp;
        }
        else
        {
            Vector3 temp = transform.localScale;
            temp.x = -1;
            transform.localScale = temp;
        }
        anim.SetTrigger("SwordAttack");
        whenCanAttack = Time.time + 1.4f;
        attacking = false;
    }

    void CheckDamage()
    {
        script2.testDeathPrincess(this.gameObject);
        script.testIfDamageTaken(this.gameObject, 1);

    }

    void Death()
    {
        if (!addedCoin)
        {
            script.increaseCoin(1);
            addedCoin = true;
        }
        anim.Play("Enemy_Die");
        Destroy(this.gameObject, 1f);
    }
    void EnemyMoveRight()
    {
        Vector3 temp = transform.position;
        Vector3 temp2 = transform.localScale;
        temp2.x = 1;
        transform.localScale = temp2;
        temp.x += speed * Time.deltaTime;
        transform.position = temp;
    }

    void EnemyMoveLeft()
    {
        Vector3 temp = transform.position;
        Vector3 temp2 = transform.localScale;
        temp2.x = -1;
        transform.localScale = temp2;
        temp.x -= speed * Time.deltaTime;
        transform.position = temp;
    }

    public void CheckHit(int damage)
    {
        //Debug.Log("TEST");
        Vector3 temp = player.transform.position;
        Vector3 temp2 = player.transform.localScale;
        if (((Mathf.Abs(temp.x - transform.position.x)) < 1.2f) && ((Mathf.Abs(temp.y - transform.position.y)) < .5f))
        {
            if ((temp2.x == -1) && (transform.position.x < temp.x))
            {
                this.hp -= damage;
                myBody.AddForce(new Vector2(-2, 0), ForceMode2D.Impulse);
                //Debug.Log("Hit");
                //Debug.Log(hp);
            }

            if ((temp2.x == 1) && (transform.position.x > temp.x))
            {
                this.hp -= damage;
                myBody.AddForce(new Vector2(2, 0), ForceMode2D.Impulse);
                //Debug.Log("Hit");
                //Debug.Log(hp);
            }
        }
    }

}

