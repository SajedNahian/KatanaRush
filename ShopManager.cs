using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour {
    [SerializeField]
    private GameObject shopPanel, player;
    private PlayerMovement script;
    private bool strengthPotionActive;
    private float whenPotionEnds;
    [SerializeField]
    private Text buyTextStrength;

	// Use this for initialization
	void Awake () {
        shopPanel.SetActive(false);
        script = player.GetComponent<PlayerMovement>();
        strengthPotionActive = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (strengthPotionActive)
        {
            if (Time.time > whenPotionEnds)
            {
                script.setStrength(1);
                buyTextStrength.text = "Buy";
            }
        }
	}

    public void shopButton ()
    {
        shopPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void closeShopbutton ()
    {
        shopPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void buyHealthPotion ()
    {
        script.buyHealthPotion();
    }

    public void buyStrengthPotion()
    {
        script.buyStrengthPotionsFromShop();
    }

    public void successfulPotion ()
    {
        script.setStrength(2);
        whenPotionEnds = Time.time + 60f;
        strengthPotionActive = true;
        buyTextStrength.text = "Active";
    }
}
