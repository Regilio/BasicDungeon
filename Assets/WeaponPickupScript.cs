using UnityEngine;
using System.Collections;

public class WeaponPickupScript : MonoBehaviour {

    public string weaponType;
    public int dmg;
    public GameObject justSpawned = null;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Player" && justSpawned == null)
        {
            c.gameObject.GetComponent<WeaponScript>().AddWeapon(weaponType, dmg);
            Destroy(gameObject);
        }
    }

    void OnTriggerExit(Collider c)
    {
        if (c.gameObject == justSpawned)
        {
            justSpawned = null;
            gameObject.GetComponentInChildren<Light>().enabled = true;
        }
    }
}
