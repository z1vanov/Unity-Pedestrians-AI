using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Evacuation : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnButtonPress()
    {
        GameObject[] agent = GameObject.FindGameObjectsWithTag("Agent");
        for(int i=0;i<agent.Length;i++)
        {
            agent[i].GetComponent<Patrol>().enabled = !agent[i].GetComponent<Patrol>().enabled;
            agent[i].GetComponent<Evacuation2>().enabled = !agent[i].GetComponent<Evacuation2>().enabled;
            TimeCheck.flag = true;
        }
    }
}
