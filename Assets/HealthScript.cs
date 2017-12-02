using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour {

    public GameObject healthBar;
    HPBarScript hpBarScript;
    RedScreenScript redScreenScript;
    public GameObject redScreenImage;
    public Text lifeText;
    public Text foodText;
    public int remainingLife;
    public int HPMax = 50;
    public int currentHP;
    public int food = 3;
    public int foodHPRestoration = 20;

    // Use this for initialization
    void Start () {
        currentHP = HPMax;
        hpBarScript = healthBar.GetComponent<HPBarScript>();
        redScreenScript = redScreenImage.GetComponent<RedScreenScript>();
        hpBarScript.HPMax = HPMax;
        refreshUI();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.R) && currentHP != HPMax && food >= 0)
        {
            currentHP += foodHPRestoration;
            if (currentHP > HPMax)
            {
                currentHP = HPMax;
            }
            food--;
            refreshUI();
        }
	}

    public void refreshUI()
    {
        hpBarScript.UpdateHPBar(currentHP);
        foodText.text = "Food : " + food + " (press R)";
        string lifeString = "";
        for (int i = 0; i < remainingLife; i++)
        {
            if(i==0)
                lifeString += "♥";
            else
                lifeString += " ♥";
        }
        lifeText.text = lifeString;
    }

    public void Hit(int damage)
    {
        currentHP -= damage;
        if (damage > 0)
        {
            redScreenScript.GotHit();

        }
        if(currentHP <= 0)
        {
            // à faire: recharger la scene actuelle en gardant vies et nourriture, mais reset armes/ennemis/clés
            currentHP = HPMax;
            remainingLife--;
            if(remainingLife == 0)
            {
                Debug.Log("Game over");
                // à faire : game over
            }
        }
        refreshUI();
    }
}
