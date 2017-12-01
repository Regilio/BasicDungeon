using UnityEngine;
using System.Collections;

public class PositionResetScript : MonoBehaviour {


    public GameObject player;
    private Vector3 initialPos;

	// Use this for initialization
	void Start () {
        initialPos = player.transform.position;

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.transform.position = initialPos;
        }
    }
}
