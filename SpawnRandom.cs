using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnRandom : MonoBehaviour
{
	public GameObject pedestrainPrefab;
	public Transform center;
    void Start()
    {
		float range = 100.0f;
		for (int i = 0; i <100; i++)
        {
            Vector3 randomPoint = center.position + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 50.0f, NavMesh.AllAreas)){		
				GameObject obj = GameObject.Instantiate(pedestrainPrefab);
				obj.transform.position=hit.position;
			}
        }
   }
}
