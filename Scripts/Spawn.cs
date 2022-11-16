using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawn : MonoBehaviour
{
	public GameObject pedestrainPrefab;
	public int pedestriansToSpawn;
	private Vector3[] wayPoints;
	

    void Start()
    {
		GameObject[] buff = GameObject.FindGameObjectsWithTag("Waypoint");
		int len = buff.Length;
		wayPoints = new Vector3[len];

		Patrolv3.num_agents = pedestriansToSpawn;
		TimeCheck.num_agents = pedestriansToSpawn;

		for(int i=0;i<len;i++){
			wayPoints[i]=buff[i].transform.position;
		}

		int count=0;
		while(count<pedestriansToSpawn)
		{
			GameObject obj = GameObject.Instantiate(pedestrainPrefab);
			obj.transform.position=wayPoints[(Mathf.RoundToInt(Random.Range(0, pedestriansToSpawn))) % len];
			obj.GetComponent<NavMeshAgent>().speed = Random.Range(2f, 5f);
			obj.GetComponent<NavMeshAgent>().avoidancePriority = Mathf.RoundToInt(Random.Range(40, 50));
			count++;
		}
	}
}
