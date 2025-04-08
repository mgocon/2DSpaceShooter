using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Wave Config", menuName = "WaveConfig")]
public class WaveConfigSO : ScriptableObject
{
    [SerializeField] List<GameObject> enemyPrefab; //Prefab of the enemy to spawn
    [SerializeField] Transform pathPrefab; //Prefab of the path to follow
    [SerializeField] float moveSpeed = 5f; //Speed of the enemy

    public Transform GetStartingWaypoint()
    {
        return pathPrefab.GetChild(0); //return the first child of the path prefab as the starting waypoint
    }

    public List<Transform> GetWaypoints()
    {
        List<Transform> waveWaypoints = new List<Transform>(); //create a new list to store the waypoints
        foreach (Transform child in pathPrefab) //loop through each child of the path prefab
        {
            waveWaypoints.Add(child); //Add each child to the list of waypoints
        }
        return waveWaypoints; //return the list of waypoints
    }

    public GameObject GetEnemyPrefab(int index)
    {
        return enemyPrefab[index]; //return the enemy prefab
    }

    public float GetMoveSpeed()
    {
        return moveSpeed; //return the move speed of the enemy
    }

    public int GetEnemyCount()
    {
        return enemyPrefab.Count; //return the number of enemies in the wave
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
