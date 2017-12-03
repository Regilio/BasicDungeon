using UnityEngine;
using System.Collections;

public class NextLevelScript : MonoBehaviour {

    public GameObject Door;

    void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Player")
        {
            if (Door.GetComponent<DoorOpenScript>().isOpen)
            {
                Debug.Log("ToDo: Teleport to next level");
            }
        }
    }
}
