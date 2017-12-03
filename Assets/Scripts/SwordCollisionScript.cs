using UnityEngine;
using System.Collections;

public class SwordCollisionScript : MonoBehaviour
{

    public int damage;
    public float singleAttackSafetyTimer = 0.1f;
    float timer = 0.0f;
    bool count = false;
    public string whoCanIAttack = "Enemy";
    

    void Update()
    {
        if (count)
        {
            timer += Time.deltaTime;
        }
        if(timer > singleAttackSafetyTimer)
        {
            count = false;
            timer = 0;
        }
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.tag == whoCanIAttack && !count)
        {
            if (whoCanIAttack == "Player" )
            {
                c.GetComponent<HealthScript>().Hit(damage);
                count = true;
            }
            if (whoCanIAttack == "Enemy")
            {
                c.GetComponent<EnemyHealthScript>().Hit(damage);
                count = true;
            }
        }
    }
}
