using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RedScreenScript : MonoBehaviour {

    public float intensity = 0.3f;
    public float decaySpeed = 1.0f;
    float timer = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (timer != 0)
            timer -= Time.deltaTime*decaySpeed;
        gameObject.GetComponent<Image>().color = new Color(1, 0, 0, timer);

	}

    public void GotHit()
    {
        timer = intensity;
    }
}
