using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    public GameObject gameOverImage;
    public GameObject gameOverText;
    public Text gameOverTexttext;
    bool gameOver = false;
    public float gameOverTimer = 5.0f;

    public float deathFadeSpeed = 1.0f;
    float deathFadeTimer = 1.0f;
    bool death = false;
    Vector3 initialPos;
    WeaponScript myWeaponScript;

    // Use this for initialization
    void Start () {
        currentHP = HPMax;
        hpBarScript = healthBar.GetComponent<HPBarScript>();
        redScreenScript = redScreenImage.GetComponent<RedScreenScript>();
        hpBarScript.HPMax = HPMax;
        gameOverImage.SetActive(false);
        gameOverText.SetActive(false);
        initialPos = gameObject.transform.position;
        myWeaponScript = gameObject.GetComponent<WeaponScript>();
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

        if (gameOver)
        {
            gameOverTimer -= Time.deltaTime;
            gameOverTexttext.text = "Game Over...\nRespawning in " + ((int)gameOverTimer).ToString();
            if (gameOverTimer <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        if (death)
        {
            myWeaponScript.DropWeapon(true);
            gameObject.transform.position = initialPos;
            if(deathFadeTimer > 0)
            {
                deathFadeTimer -= Time.deltaTime * deathFadeSpeed;
                gameOverImage.GetComponent<Image>().color = new Color(0, 0, 0, deathFadeTimer);
            }
            else
            {
                deathFadeTimer = 1;
                gameOverImage.SetActive(false);
                death = false;
            }
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
                GameOver();
            }else
            {
                Death();
            }
        }
        refreshUI();
    }

    void OnTriggerEnter(Collider c)
    {
        if(c.tag == "Food")
        {
            food++;
            refreshUI();
            Destroy(c.gameObject);
        }
    }

    void GameOver()
    {
        gameOver = true;
        gameOverImage.SetActive(true);
        gameOverImage.GetComponent<Image>().color = new Color(0, 0, 0, 1);
        gameOverText.SetActive(true);
    }

    void Death()
    {
        death = true;
        gameOverImage.SetActive(true);
    }
}
