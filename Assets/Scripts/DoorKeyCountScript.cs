using UnityEngine;
using System.Collections;

public class DoorKeyCountScript : MonoBehaviour {

    public TextMesh DoorKeyText;
    public GameObject Door;
    public int RemainingKeys;
    // Use this for initialization
    void Start ()
    {
        RemainingKeys = GameObject.FindGameObjectsWithTag("Key").Length;
        UpdateKeyText();
    }
	
	// Update is called once per frame
	public void FoundKey () {
        RemainingKeys--;
        UpdateKeyText();

    }

    void UpdateKeyText()
    {
        if (RemainingKeys == 1)
        {
            DoorKeyText.text = "Il te reste 1 clé\n"
                                + "pour ouvrir la porte\n"
                                + "et progresser";
            Door.GetComponent<DoorOpenScript>().CloseDoor();
        }
        else if (RemainingKeys == 0)
        {
            DoorKeyText.text = "Tu as toutes les\n"
                                + "clés pour continuer\n";
            Door.GetComponent<DoorOpenScript>().OpenDoor();
        }
        else
        {
            DoorKeyText.text = "Il te reste " + RemainingKeys + " clés\n"
                               + "pour ouvrir la porte\n"
                               + "et progresser";
            Door.GetComponent<DoorOpenScript>().CloseDoor();
        }
    }
}
