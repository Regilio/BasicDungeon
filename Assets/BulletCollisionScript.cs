using UnityEngine;
using System.Collections;

public class BulletCollisionScript : MonoBehaviour {

    public int damage;
    public string whoCanIAttack = "Enemy";
    void OnCollisionEnter(Collision c)
    {
        if(c.gameObject.tag == whoCanIAttack)
        {
            if(whoCanIAttack == "Player")
            {
                c.gameObject.GetComponent<HealthScript>().Hit(damage);
            }
        }
        Destroy(gameObject);
    }
}
