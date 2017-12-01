using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class WeaponScript : MonoBehaviour {

    public string currentWeaponType;
    public int currentWeaponDmg;
    public List<HeldWeapon> HeldWeapons;
    public Text weaponText;
    public bool holdingWeapon = false;
    public List<NewWeapon> NewWeapons;
    

	// Use this for initialization
	void Start () {
        foreach(HeldWeapon weapon in HeldWeapons)
        {
            weapon.WeaponPrefab.SetActive(false);
        }

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void AddWeapon(string type, int dmg)
    {
        switch (type)
        {
            case "Sword":
                //show sword
                DropWeapon();
                holdingWeapon = true;
                break;
            case "Gun":
                //show gun
                DropWeapon();
                holdingWeapon = true;
                break;
            case "Laser":
                //show laser
                DropWeapon();
                holdingWeapon = true;
                break;
            default:
                Debug.Log("Arme non supportée");
                break;
        }

        if (holdingWeapon)
        {
            currentWeaponType = type;
            currentWeaponDmg = dmg;
            weaponText.text = "Current weapon : " + type + " (Damage: " + dmg + ")";
        }
        else
        {
            currentWeaponType = null;
            currentWeaponDmg = 0;
            weaponText.text = "";
        }
        foreach (HeldWeapon weapon in HeldWeapons)
        {
            if(weapon.Type == currentWeaponType)
                weapon.WeaponPrefab.SetActive(true);
        }
    }

    public void DropWeapon()
    {
        if (holdingWeapon)
        {
            foreach (HeldWeapon weapon in HeldWeapons)
            {
                weapon.WeaponPrefab.SetActive(false);
            }
            foreach (NewWeapon weapon in NewWeapons)
            {
                // à ameliorer: quand ramassé: pas ramassable qqs temps. Initialiser les données de l'arme
                if((currentWeaponType == weapon.Type))
                {
                    GameObject newWeapon = Instantiate(weapon.WeaponPrefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z+2), Quaternion.identity) as GameObject;
                    Debug.Log("name = "+newWeapon.name);
                    newWeapon.GetComponent<WeaponPickupScript>().justSpawned = this.gameObject;
                    //newWeapon.GetComponent<WeaponPickupScript>().GetComponentInChildren<Light>().enabled = false;


                }
            }
            
        }
        holdingWeapon = false;
    }
}

[Serializable]
public class HeldWeapon
{
    public string Type;
    public GameObject WeaponPrefab;
}

[Serializable]
public class NewWeapon
{
    public string Type;
    public Transform WeaponPrefab;
}
