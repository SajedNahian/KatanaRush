using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {
    public float speed = 12f;
    private Rigidbody2D myBody;
    private Animator anim;
    private bool InAir = false;
    private bool movingLeft, movingRight;
    public int hp = 20;
    private float coins = 0;
    private int strength;
    //private float LastAttackTime = 0f;
    //private float attackCooldown = .01f;
    // Use this for initialization
    [SerializeField]
    private GameObject minX, maxX;
    [SerializeField]
    private Text hpText, coinText;
    private ShopManager shopScript;
    private NinjaGirl script;
    [SerializeField]
    private GameObject shield;
    private bool shieldActive;
    private float whenShieldEnds;

    void Awake () {
        myBody = GetComponent<Rigidbody2D>(); //or use [SerializeField]
        anim = GetComponent<Animator>();
        movingLeft = false;
        movingRight = false;
        shield.SetActive(false);
        hpText.text = "Health: 20";
        coinText.text = "Coins: " + coins.ToString();
        strength = 1;
        shopScript = GameObject.FindGameObjectWithTag("ShopManager").GetComponent<ShopManager>();
        script = GameObject.FindGameObjectWithTag("NGirl").GetComponent<NinjaGirl>();
    }
	
	// Update is called once per frame
	void Update () {
        //PlayerMoveKeyboard();
        //PlayerAttack();
        if (hp > 0)
        {
            PlayerMoveButton();
        }
        if (hp <= 0)
        {
            playerDie();
        }
        //if (myBody.velocity.x == 0)
        //{
            //anim.SetBool("Walk", false);
        //}
	}

    void playerDie()
    {
        anim.Play("Player_Die");
        //script.GameOver();
        //Destroy(this.gameObject, 1f);
    }

    void deathAnimationFinalFrameFunction ()
    {
        script.GameOver();
    }

    void PlayerMoveKeyboard()
    {
        float h = Input.GetAxisRaw("Horizontal");
        if (h > 0)
        {
            Vector3 temp = transform.position;
            Vector3 temp2 = transform.localScale;
            temp.x += speed * Time.deltaTime;
            temp2.x = 1f;
            transform.position = temp;
            transform.localScale = temp2;
            anim.SetBool("Walk", true);
        }
        else if (h < 0)
        {
            Vector3 temp = transform.position;
            Vector3 temp2 = transform.localScale;
            temp.x -= speed * Time.deltaTime;
            temp2.x = -1f;
            transform.position = temp;
            transform.localScale = temp2;
            anim.SetBool("Walk", true);

        }
        else
        {
            anim.SetBool("Walk", false);
        }


        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && !InAir)
        {
            myBody.AddForce(new Vector2(0, 6), ForceMode2D.Impulse);
            anim.SetBool("InAir", true);
            InAir = true;
        }
    }

    void PlayerAttack ()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            //if (Time.time > LastAttackTime + attackCooldown)
            //{
            //LastAttackTime = Time.time;
            anim.SetTrigger("SwordAttack");
            //}
        }
    }

    public void LandedOnGround ()
    {
        myBody.velocity = new Vector2(0, 0);
        anim.SetBool("InAir", false);
        InAir = false;
    }

    public void MoveLeftButton ()
    {
        movingLeft = true;
    }

    public void ShieldButton ()
    {
        shield.SetActive(true);
        shieldActive = true;
    }

    public void StopShieldButton ()
    {
        shield.SetActive(false);
        shieldActive = false;
    }

    public void StopMoveLeftButton()
    {
        movingLeft = false;
    }

    public void MoveRightButton ()
    {
        movingRight = true;
    }

    public void StopMoveRightButton()
    {
        movingRight = false;
    }

    public void JumpButton ()
    {
        if ((!InAir && (hp > 0)))
        { 
            myBody.AddForce(new Vector2(0, 6), ForceMode2D.Impulse);
            anim.SetBool("InAir", true);
            InAir = true;
        }
    }
    
    public void AttackButton ()
    {
        if (hp > 0)
        {
            anim.SetTrigger("SwordAttack");
        }      
    }

    void AttackAnimationFunction ()
    {
        GroundEnemy[] scripts = FindObjectsOfType<GroundEnemy>();
        for (int i = 0; i < scripts.Length; i++)
        {
            scripts[i].CheckHit(strength);
        }

        ThrowingEnemy[] scripts2 = FindObjectsOfType<ThrowingEnemy>();
        for (int i = 0; i < scripts2.Length; i++)
        {
            scripts2[i].CheckHit(strength);
        }

    }

    void PlayerMoveButton ()
    {
        if ((movingRight && movingLeft) || (!movingLeft && !movingRight))
        {
            anim.SetBool("Walk", false);
        } else if (movingRight)
        {
            Vector3 temp = transform.position;
            Vector3 temp2 = transform.localScale;
            temp.x += speed * Time.deltaTime;
            temp2.x = 1f;
            transform.position = temp;
            transform.localScale = temp2;
            anim.SetBool("Walk", true);
            if (transform.position.x > maxX.transform.position.x)
            {
                Vector3 temp3 = transform.position;
                temp3.x = maxX.transform.position.x;
                transform.position = temp3;
            }
        } else
        {
            Vector3 temp = transform.position;
            Vector3 temp2 = transform.localScale;
            temp.x -= speed * Time.deltaTime;
            temp2.x = -1f;
            transform.position = temp;
            transform.localScale = temp2;
            if (transform.position.x < minX.transform.position.x)
            {
                Vector3 temp3 = transform.position;
                temp3.x = minX.transform.position.x;
                transform.position = temp3;
            }
            anim.SetBool("Walk", true);
        }
    }

    public void takeDamage (int amount)
    {
        //
        //Debug.Log(amount + " <- damage  hp left-> " + hp);
        hp -= amount;
        hpText.text = "Health: " + hp.ToString();
    }

    public void testIfDamageTaken(GameObject enemy, int damage)
    {
        if (!shieldActive)
        {
            Vector3 temp = enemy.transform.position;
            Vector3 temp2 = enemy.transform.localScale;

            if (((Mathf.Abs(temp.x - transform.position.x)) < 1.2f) && ((Mathf.Abs(temp.y - transform.position.y)) < .5f))
            {
                if ((temp2.x == -1) && (transform.position.x < temp.x))
                {
                    hp -= damage;
                    myBody.AddForce(new Vector2(-2, 0), ForceMode2D.Impulse);
                    hpText.text = "Health: " + hp.ToString();
                    //Debug.Log("Hit");
                    //Debug.Log(hp);
                }

                if ((temp2.x == 1) && (transform.position.x > temp.x))
                {
                    hp -= damage;
                    myBody.AddForce(new Vector2(2, 0), ForceMode2D.Impulse);
                    hpText.text = "Health: " + hp.ToString();
                    //Debug.Log("Hit");
                    //Debug.Log(hp);
                }
            }
        }
    }

    public void buyHealthPotion ()
    {
        if (coins >= 12)
        {
            coins -= 12;
            hp += 10;
            hpText.text = "Health: " + hp.ToString();
            coinText.text = "Coins: " + coins.ToString();
        }
    }

    public void increaseCoin (int x)
    {
        coins += x;
        coinText.text = "Coins: " + coins.ToString();
    }


    public void setStrength (int num)
    {
        strength = num;
    }

    public void shurikenHit ()
    {
        if (!shieldActive)
        {
            hp -= 1;
            hpText.text = "Health: " + hp.ToString();
        }
    }

    public void buyStrengthPotionsFromShop()
    {
        if (coins >= 4)
        {
            coins -= 4;
            shopScript.successfulPotion();
        }
    }
}
