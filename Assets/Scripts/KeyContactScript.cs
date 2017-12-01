using UnityEngine;
using System.Collections;

public class KeyContactScript : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<KeysScript>().AddKey();
            Destroy(gameObject);
        }
    }
}
