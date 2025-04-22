using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] WaveConfigSO waveConfig;
    EnemySpawner enemySpawner; //reference to the enemy spawner
    List<Transform> waypoints;
    int waypointIndex = 0; //index of the current waypoint
    void Start()
    {
        waypoints = waveConfig.GetWaypoints();
        transform.position = waveConfig.GetStartingWaypoint().transform.position; //set the position of the enemy to the starting waypoint
    }

    void Awake()
    {
        enemySpawner = GetComponentInParent<EnemySpawner>(); //get the enemy spawner component from the parent object
        waveConfig = enemySpawner.GetCurrentWave(); //get the current wave config from the enemy spawner
    }
    // Update is called once per frame
    void Update()
    {
        FollowPath();
    }

    void FollowPath()
    {
        if (waypointIndex <= waypoints.Count - 1) //Check if the waypoint index is within the bounds of the waypoints list
        {
            Vector3 targetPosition = waypoints[waypointIndex].position; //get the position of the current waypoint

            float delta = waveConfig.GetMoveSpeed() * Time.deltaTime; //Calculate the movement for this frame
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, delta); //move the enemy towards the target position

            if (transform.position == targetPosition) //Check if the enemy has reached the target position
            {
                waypointIndex++; //increment the waypoint index to move to the next waypoint
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
