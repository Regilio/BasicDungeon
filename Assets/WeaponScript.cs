using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using UnityStandardAssets.CrossPlatformInput;

public class WeaponScript : MonoBehaviour {

    public string currentWeaponType;
    public int currentWeaponDmg;
    public bool holdingWeapon = false;

    public string whoCanIAttack = "Enemy";

    public Text weaponText;

    public List<NewWeapon> NewWeapons;
    public List<HeldWeapon> HeldWeapons;

    public GameObject laserTransform;
    LineRenderer laser;
    bool canShootLaser = true;
    public float laserDeleteTime = 0.1f;
    public float laserCooldownTime = 0.1f;
    float laserTimer = 0;
    public float laserRange;
    public bool laserDealtDmg = false;

    public GameObject bulletTransform;
    public GameObject bulletPrefab;
    bool canShootBullet = true;
    public float bulletCooldownTime = 0.3f;
    float bulletTimer = 0;

    bool canAttackSword = true;
    public float swordCooldownTime = 0.3f;
    float swordTimer = 0;

    // Use this for initialization
    void Start () {
        if (gameObject.tag == "Enemy")
            whoCanIAttack = "Player";

        foreach (HeldWeapon weapon in HeldWeapons)
        {
            weapon.WeaponGameObject.SetActive(false);
        }
        laser = laserTransform.GetComponent<LineRenderer>();

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire2") && whoCanIAttack == "Enemy")
        {
            switch (currentWeaponType)
            {
                case "Sword":
                    AttackSword();
                    break;
                case "Gun":
                    FireBullet();
                    break;
                case "Laser":
                    FireLaser();
                    break;
                default:
                    Debug.Log("No Weapon");
                    break;
            }
        }

        if (!canShootLaser)
        {
            laserTimer += Time.deltaTime;
            if (laserTimer < laserDeleteTime)
            {
                Ray ray = new Ray(laserTransform.transform.position, laserTransform.transform.forward);
                RaycastHit hit;

                laser.enabled = true;
                laser.SetPosition(0, ray.origin);

                if (Physics.Raycast(ray, out hit, laserRange))
                {
                    //Debug.Log(laserTransform.transform.position + "," + laserTransform.transform.forward + "," + hit.transform.gameObject.name);
                    laser.SetPosition(1, hit.point);
                    //Debug.Log(hit.transform.gameObject.name);
                    if (hit.transform.gameObject.tag == "Player" && whoCanIAttack == "Player" && !laserDealtDmg)
                    {
                        hit.transform.gameObject.GetComponent<HealthScript>().Hit(currentWeaponDmg);
                        laserDealtDmg = true;
                    }
                }
                else
                {
                    laser.SetPosition(1, ray.GetPoint(laserRange));
                }
            }
            else
            {
                laser.enabled = false;

            }

            if (laserTimer > laserCooldownTime)
            {
                laserTimer = 0;
                canShootLaser = true;
                laserDealtDmg = false;
            }
        }

        if (!canShootBullet)
        {
            bulletTimer += Time.deltaTime;

            if (bulletTimer > bulletCooldownTime)
            {
                bulletTimer = 0;
                canShootBullet = true;
            }
        }

        if (!canAttackSword)
        {
            swordTimer += Time.deltaTime;

            if (swordTimer > swordCooldownTime)
            {
                swordTimer = 0;
                canAttackSword = true;
            }
        }
    }

    public void AttackSword()
    {
        //To do: sword damage from enemy
        if (canAttackSword)
        {
            canAttackSword = false;
            foreach (HeldWeapon weapon in HeldWeapons)
            {
                if (weapon.Type == "Sword")
                {
                    Animator swordAnim = weapon.WeaponGameObject.GetComponent<Animator>();
                    swordAnim.SetTrigger("Attack");
                }
            }
        }
    }

    public void FireLaser()
    {
        if (canShootLaser)
        {
            canShootLaser = false;
        }
    }

    public void FireBullet()
    {
        if (canShootBullet)
        {
            canShootBullet = false;
            GameObject newBullet = Instantiate(bulletPrefab, bulletTransform.transform.position, bulletTransform.transform.rotation) as GameObject;

            newBullet.GetComponent<Rigidbody>().velocity = newBullet.transform.forward * 15;
            newBullet.GetComponent<BulletCollisionScript>().damage = currentWeaponDmg;
            newBullet.GetComponent<BulletCollisionScript>().whoCanIAttack = whoCanIAttack;
            Destroy(newBullet, 3.0f);
        }
    }

    public void AddWeapon(string type, int dmg)
    {
        switch (type)
        {
            case "Sword":
                DropWeapon();
                holdingWeapon = true;
                break;
            case "Gun":
                DropWeapon();
                holdingWeapon = true;
                break;
            case "Laser":
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
            if (whoCanIAttack == "Enemy")
                weaponText.text = "Current weapon : " + type + " (Damage: " + dmg + ")";
        }
        else if (!holdingWeapon)
        {
            currentWeaponType = null;
            currentWeaponDmg = 0;
            if (whoCanIAttack == "Enemy")
                weaponText.text = "";
        }
        foreach (HeldWeapon weapon in HeldWeapons)
        {
            if(weapon.Type == currentWeaponType)
                weapon.WeaponGameObject.SetActive(true);
        }
    }

    public void DropWeapon()
    {
        if (holdingWeapon)
        {
            foreach (HeldWeapon weapon in HeldWeapons)
            {
                weapon.WeaponGameObject.SetActive(false);
            }
            foreach (NewWeapon weapon in NewWeapons)
            {
                // à ameliorer: quand ramassé: pas ramassable qqs temps. Initialiser les données de l'arme
                if((currentWeaponType == weapon.Type))
                {
                    GameObject newWeapon = Instantiate(weapon.WeaponPrefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity) as GameObject;
                    //Debug.Log("name = "+newWeapon.name);
                    newWeapon.GetComponent<WeaponPickupScript>().justSpawned = this.gameObject;
                    newWeapon.GetComponent<WeaponPickupScript>().GetComponentInChildren<Light>().enabled = false;


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
    public GameObject WeaponGameObject;
}

[Serializable]
public class NewWeapon
{
    public string Type;
    public GameObject WeaponPrefab;
}
