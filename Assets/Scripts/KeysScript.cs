using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class KeysScript : MonoBehaviour {

    public int Keys = 0;
    public Text UIKeyText;
    public TextMesh DoorKeyText;
    
	public void AddKey()
    {
        Keys++;
        UIKeyText.text = "Keys : " + Keys;
        DoorKeyText.GetComponent<DoorKeyCountScript>().FoundKey();
    }
}
