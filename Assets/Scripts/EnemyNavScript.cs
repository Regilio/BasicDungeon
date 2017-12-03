using UnityEngine;
using System.Collections;

public class EnemyNavScript : MonoBehaviour
{

    public Transform[] TargetPositions;
    public bool inMission = false;
    public bool followingPlayer = false;
    public GameObject player = null;
    float visionRange = 0;
    public GameObject EnemySpottedText;
    EnemySpottedUIScript ESUI;


    // Use this for initialization
    void Start()
    {
        GoToNextDestination();
        visionRange = gameObject.GetComponent<EnemyVisionScript>().visionRange;
        ESUI = EnemySpottedText.GetComponent<EnemySpottedUIScript>();
    }

    void Update()
    {

        if (followingPlayer && player != null)
        {
            FollowPlayer(player);
        }
        if (followingPlayer && GetComponent<NavMeshAgent>().remainingDistance > visionRange)
        {
            followingPlayer = false;
            inMission = false;
            player = null;
            //Debug.Log("Player to follow is too far away");
            ESUI.Spotted(false);
        }
        
        if (GetComponent<NavMeshAgent>().remainingDistance < 0.3f && inMission && !followingPlayer)
        {
            inMission = false;
        }
        if (GetComponent<NavMeshAgent>().remainingDistance < 0.5f && !inMission)
        {
            GoToNextDestination();
        }
    }

    public void GoToNextDestination()
    {
        GetComponent<NavMeshAgent>().destination = TargetPositions[Random.Range(0, TargetPositions.Length)].position;
    }
    public void ReachPoint(Vector3 Destination)
    {
        if (!inMission)
            GetComponent<NavMeshAgent>().destination = Destination;
        inMission = true;
    }
    public void FollowPlayer(GameObject Player)
    {
        if (!inMission || followingPlayer)
        {
            inMission = true;
            followingPlayer = true;
            player = Player;
            if (Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(player.transform.position.x, 0, player.transform.position.z)) > 2.5)
            {
                GetComponent<NavMeshAgent>().destination = Player.transform.position;
            }
            else
            {
                //Debug.Log("Too close");
                GetComponent<NavMeshAgent>().destination = new Vector3(transform.position.x, 0, transform.position.z);
            }
        }

    }
}
