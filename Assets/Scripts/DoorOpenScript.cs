using UnityEngine;
using System.Collections;

public class DoorOpenScript : MonoBehaviour {


    public bool isOpen = false;
    public ParticleSystem particle1;
    public ParticleSystem particle2;

    public void OpenDoor()
    {
        isOpen = true;
        particle1.Play();
        particle2.Play();
    }
    public void CloseDoor()
    {
        isOpen = false;
        particle1.Stop();
        particle2.Stop();
    }
}
