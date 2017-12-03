using UnityEngine;
using System.Collections;

public class EnemyVisionScript : MonoBehaviour
{

    public GameObject visionPoint;
    LineRenderer laser;
    public float visionRange;
    WeaponScript myWeaponScript;
    EnemyNavScript myNavScript;
    public LayerMask RaycastDetection;
    public GameObject EnemySpottedText;
    EnemySpottedUIScript ESUI;
    public int visionSpeed;

    // Use this for initialization
    void Start()
    {

        laser = visionPoint.GetComponent<LineRenderer>();
        myWeaponScript = gameObject.GetComponent<WeaponScript>();
        myNavScript = gameObject.GetComponent<EnemyNavScript>();
        ESUI = EnemySpottedText.GetComponent<EnemySpottedUIScript>();
    }

    // Update is called once per frame
    void Update()
    {

        Ray ray = new Ray(visionPoint.transform.position, visionPoint.transform.forward);
        RaycastHit hit;

        //Décommenter pour voir la vision des ennemis
       // laser.enabled = true;
        laser.SetPosition(0, ray.origin);

        if (Physics.Raycast(ray, out hit, visionRange, RaycastDetection))
        {
            laser.SetPosition(1, hit.point);
            if (hit.collider.tag == "Weapon")
            {
                WeaponPickupScript detectedWeapon = hit.collider.GetComponent<WeaponPickupScript>();
                if (myWeaponScript.currentWeaponType == null || myWeaponScript.currentWeaponType == "")
                {
                    myNavScript.ReachPoint(hit.point);
                    //Debug.Log("New Weapon Detected");
                }
                else 
                {
                    if (detectedWeapon.weaponType == myWeaponScript.currentWeaponType && detectedWeapon.dmg > myWeaponScript.currentWeaponDmg)
                    {
                        myNavScript.ReachPoint(hit.point);
                        //Debug.Log("Better Weapon Detected");
                    }
                }
            }
            if (hit.collider.tag == "Player")
            {
                if (!(myWeaponScript.currentWeaponType == null) && !(myWeaponScript.currentWeaponType == ""))
                {
                    myNavScript.FollowPlayer(hit.collider.gameObject);
                    ESUI.Spotted(true);
                }
            }
        }
        else
        {
            laser.SetPosition(1, ray.GetPoint(visionRange));
        }

        visionPoint.transform.localEulerAngles = new Vector3(0, Mathf.PingPong(Time.time * visionSpeed, 70) - 35, 0);
    }
}
