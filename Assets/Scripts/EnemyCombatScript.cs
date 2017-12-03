using UnityEngine;
using System.Collections;

public class EnemyCombatScript : MonoBehaviour {
    
    WeaponScript myWeaponScript;
    EnemyNavScript myNavScript;

    // Use this for initialization
    void Start ()
    {
        myWeaponScript = gameObject.GetComponent<WeaponScript>();
        myNavScript = gameObject.GetComponent<EnemyNavScript>();

    }
	
	// Update is called once per frame
	void Update () {
	    if(myNavScript.followingPlayer && myNavScript.player != null && !(myWeaponScript.currentWeaponType == null) && !(myWeaponScript.currentWeaponType == "")){
            if(myWeaponScript.currentWeaponType == "Gun")
            {
                transform.LookAt(myNavScript.player.transform);
                myWeaponScript.FireBullet();
            }
            if (myWeaponScript.currentWeaponType == "Laser")
            {
                //transform.LookAt(myNavScript.player.transform);
                myWeaponScript.FireLaser();
            }
            if (myWeaponScript.currentWeaponType == "Sword")
            {
                transform.LookAt( new Vector3(myNavScript.player.transform.position.x, gameObject.transform.position.y, myNavScript.player.transform.position.z));
                if (Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(myNavScript.player.transform.position.x, 0, myNavScript.player.transform.position.z)) < 5)
                {
                    myWeaponScript.AttackSword();
                }
            }
        }
	}
}
