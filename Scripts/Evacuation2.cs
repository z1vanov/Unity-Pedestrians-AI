using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Evacuation2 : MonoBehaviour
{
    public Transform escape_point;
    private NavMeshHit navMeshHit;
    private NavMeshAgent agent;
    bool flag=true;
    public float distance_max;
    public float distance_min;

    // Update is called once per frame
    void Update()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = escape_point.position;
        agent.speed = 15;
        agent.avoidancePriority = 51;
        if(agent.radius != distance_min && agent.radius != distance_max){
            agent.radius = distance_max;
        }
        if(NavMesh.SamplePosition(agent.transform.position, out navMeshHit, 0.1f, -1)) 
        {
            if(navMeshHit.mask==16){
                agent.radius=distance_min;
            }
            else if(navMeshHit.mask==1)
            { 
                agent.radius=distance_max;
            }
            if(navMeshHit.mask==64 && flag){
                flag = false;
                TimeCheck.count++;
            }
            if(navMeshHit.mask==64)
                agent.radius = 0.1f;
        }
        if(agent.hasPath && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.isStopped=true;
            agent.speed = Random.Range(2f, 5f);
            this.GetComponent<Animator>().SetBool("move", false);
        }
    }
}