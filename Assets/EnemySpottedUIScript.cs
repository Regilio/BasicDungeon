using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemySpottedUIScript : MonoBehaviour {

    float timer = 0.0f;
    bool count = false;
    public Text text;
	// Use this for initialization
	void Start () {
        text.text = "";
	}
	
	// Update is called once per frame
	void Update () {
        if (count)
            timer += Time.deltaTime;
        if (timer > 3.0f)
        {
            text.text = "";
            count = false;
            timer = 0.0f;
        }
	}
    
    public void Spotted(bool isSpotted)
    {
        count = true;
        timer = 0.0f;
        if (isSpotted)
           text.text = "An enemy has spotted you !";
        else
            text.text = "An enemy has lost you from sight";
    }
}
