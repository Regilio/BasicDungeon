using UnityEngine;
using System.Collections;

public class BulletCollisionScript : MonoBehaviour {

    public int damage;
    public string whoCanIAttack = "Enemy";
    
    public float singleAttackSafetyTimer = 0.1f;
    float timer = 0.0f;
    bool count = false;

    void Update()
    {
        if (count)
        {
            timer += Time.deltaTime;
        }
        if (timer > singleAttackSafetyTimer)
        {
            count = false;
            timer = 0;
        }
    }
    void OnCollisionEnter(Collision c)
    {
        if(c.gameObject.tag == whoCanIAttack)
        {
            if(whoCanIAttack == "Player")
            {
                c.gameObject.GetComponent<HealthScript>().Hit(damage);
                count = true;
            }
            if (whoCanIAttack == "Enemy")
            {
                c.gameObject.GetComponent<EnemyHealthScript>().Hit(damage);
                count = true;
            }
        }
        Destroy(gameObject);
    }
}
