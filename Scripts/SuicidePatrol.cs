using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SuicidePatrol : MonoBehaviour
{
    public void OnButtonPress()
    {
        GameObject[] fire = GameObject.FindGameObjectsWithTag("Explosion");
        GameObject[] agent = GameObject.FindGameObjectsWithTag("Agent");
        for(int i=0;i<fire.Length;i++)
        {
            fire[i].GetComponent<NavMeshObstacle>().enabled = !fire[i].GetComponent<NavMeshObstacle>().enabled;
        }

        for(int i=0;i<agent.Length;i++)
        {
            agent[i].GetComponent<SuicidePatrol2>().enabled = !agent[i].GetComponent<SuicidePatrol2>().enabled;
        }
        if(fire[0].GetComponent<NavMeshObstacle>().enabled)
        {
            Debug.Log("Fire is OFF");
        }
        else
        {
            Debug.Log("Fire is ON");
        }
    }
}
