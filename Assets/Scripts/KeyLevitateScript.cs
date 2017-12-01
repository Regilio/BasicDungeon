using UnityEngine;
using System.Collections;

public class KeyLevitateScript : MonoBehaviour {

    float initialPos;
    float random;

    void Start()
    {
        initialPos = transform.position.y;
        random = Random.Range(0.0f, 2.0f);
    }

	// Update is called once per frame
	void Update () {

        transform.position = new Vector3(transform.position.x, initialPos + Mathf.PingPong((Time.time/2)+ random, 1), transform.position.z);
    }
}
