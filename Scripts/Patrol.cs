using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour
{
    public NavMeshAgent agent;
    private NavMeshHit navMeshHit;
    private Animator anim;
    
    private GameObject[] other;
    private Transform[] wayPoints;

    private int currentWayPoint;
    private int lenght_interestpoints;

    public float lookout_time;
    public float distance_tansition_time;
    public float distance_max;
    public float distance_min;
	
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator> ();

        GameObject[] buff = GameObject.FindGameObjectsWithTag("Waypoint");
	    wayPoints = new Transform[buff.Length];

        other = GameObject.FindGameObjectsWithTag("Interestpoint");
        lenght_interestpoints=other.Length;

        
	    currentWayPoint=0;
        if (agent == null)
        {
            agent.GetComponent<NavMeshAgent>();
        }

	    for(int i=0;i<buff.Length;i++)
        {
		    wayPoints[i]=buff[i].transform;
	    }
	    currentWayPoint = Mathf.RoundToInt(Random.Range(0f, wayPoints.Length-1));
    }

    // Update is called once per frame
    void Update()
    {	
        InterestpointDistance();
        Path(); 
        InterestpointAI();
    }

    private void InterestpointDistance(){
        if(NavMesh.SamplePosition(agent.transform.position, out navMeshHit, 0.1f, -1)) 
        {
            if(navMeshHit.mask==16 && agent.radius==distance_max){
                agent.radius=distance_min;
            }
            else if(navMeshHit.mask==1 && agent.radius==distance_min)
            { 
                StartCoroutine(DistanceTransitionCoroutine());
            }
        }
    }

    private void InterestpointAI(){
        for(int i=0;i<lenght_interestpoints;i++)
        {
            if(Vector3.Distance(other[i].transform.position, transform.position)<55)
            {
                StartCoroutine(LookOutCoroutine(i));
            }        
        }
    }

    IEnumerator LookOutCoroutine (int i){
        agent.isStopped=true;
        anim.SetBool("move", false);
        yield return new WaitForSeconds(Random.Range(0f,lookout_time));
        anim.SetBool("move", true);
        agent.isStopped=false;
    }

    private void Path()
    {
        if(agent.destination != wayPoints[currentWayPoint].position)
        {
            agent.destination = wayPoints[currentWayPoint].position;
        }
        if (HasReached())
        {
            currentWayPoint = Mathf.RoundToInt(Random.Range(0f, wayPoints.Length-1));
        }
    }
    private bool HasReached()
    {
        return (agent.hasPath && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance);
    }

    IEnumerator DistanceTransitionCoroutine (){
        agent.radius += (distance_max-distance_min)/2;
        yield return new WaitForSeconds(distance_tansition_time);
        agent.radius += (distance_max-distance_min)/2;
    }
}