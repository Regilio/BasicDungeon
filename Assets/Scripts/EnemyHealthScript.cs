using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyHealthScript : MonoBehaviour {

    public GameObject healthBar;
    HPBarScript hpBarScript;
    public int HPMax = 50;
    public int currentHP;

    EnemyNavScript myNavScript;
    public GameObject player;

    public GameObject food;

    // Use this for initialization
    void Start()
    {
        currentHP = HPMax;
        hpBarScript = healthBar.GetComponent<HPBarScript>();
        myNavScript = gameObject.GetComponent<EnemyNavScript>();
        hpBarScript.HPMax = HPMax;
        refreshUI();
    }


    public void refreshUI()
    {
        hpBarScript.UpdateHPBar(currentHP);
    }

    public void Hit(int damage)
    {
        myNavScript.FollowPlayer(player);
        currentHP -= damage;
        if (currentHP <= 0)
        {
            Instantiate(food, new Vector3(transform.position.x + 1, transform.position.y, transform.position.z), transform.rotation);
            Instantiate(food, new Vector3(transform.position.x - 0.7f, transform.position.y, transform.position.z + 0.7f), transform.rotation);
            Instantiate(food, new Vector3(transform.position.x - 0.7f, transform.position.y, transform.position.z - 0.7f), transform.rotation);
            gameObject.GetComponent<WeaponScript>().DropWeapon(true);
            Destroy(gameObject);
        }
        refreshUI();
    }
}
