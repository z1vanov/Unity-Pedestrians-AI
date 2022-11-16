using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrolv3 : MonoBehaviour
{
    public NavMeshAgent agent;
    private int currentWayPoint;
    private int lenght;
    public Transform[] wayPoints;

    private GameObject[] agents;
    private int lenght_agents;
    public static int num_agents;

    private bool m_HitDetect_fwr;
    private bool m_HitDetect_lft;

    private RaycastHit m_Hit_fwr;
    private RaycastHit m_Hit_lft;

    public int m_MaxDistance_fwr;
    public int m_MaxDistance_lft;

    private Vector3 box_fwr;
    private Vector3 box_lft;

    public float box_fwr_x;
    public float box_fwr_y;
    public int front_ofset;

    public float box_lft_x;
    public float box_lft_y;
    public int left_ofset;

    // Start is called before the first frame update
    void Start()
    {
        lenght_agents = 0;
        box_fwr.y = 10;
        box_lft.y = 10;

        if(m_MaxDistance_fwr==0)
        {
            m_MaxDistance_fwr = 20;
            m_MaxDistance_lft = 40;
            box_fwr_x = 50;
            box_fwr_y = 30;
            front_ofset = 23;
            box_lft_x = 40;
            box_lft_y = 20;
            left_ofset = 23;
        }
        
        GetAgents();

        if (agent == null)
        {
            agent.GetComponent<NavMeshAgent>();
        }
        lenght=wayPoints.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if(num_agents != lenght_agents){
            GetAgents();
        }
        AgentDistance();
        Pat();
    }

    private void AgentDistance()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        Vector3 lft = transform.TransformDirection(Vector3.left);
        box_fwr = transform.forward;
        bool flag = false;
        if(box_fwr.z > 0.9 || box_fwr.z < -0.9)
        {
            box_fwr.z = box_fwr_y;
            box_fwr.x = box_fwr_x;

            box_lft.x = box_lft_y;
            box_lft.z = box_lft_x;
        }
        else
        {
            box_fwr.z = box_fwr_x;
            box_fwr.x = box_fwr_y;

            box_lft.x = box_lft_x;
            box_lft.z = box_lft_y;
        }

        m_HitDetect_fwr = Physics.BoxCast(transform.position + -transform.right*front_ofset, 
            box_fwr, fwd, out m_Hit_fwr, transform.rotation, m_MaxDistance_fwr,1);
        m_HitDetect_lft = Physics.BoxCast(transform.position + transform.forward*left_ofset, 
            box_lft, lft, out m_Hit_lft, transform.rotation, m_MaxDistance_lft,1);
        for(int i=0;i<lenght_agents;i++)
        {
            Vector3 toOther = agents[i].transform.position - transform.position;
            if (Vector3.Dot(fwd, toOther) > 0 && 
                Vector3.Distance(agents[i].transform.position, transform.position)<35)
            {
                flag = true;
            }
        }
        
        if(m_HitDetect_fwr || m_HitDetect_lft || flag)
        {
            agent.isStopped=true;
            flag = false;
        }
        else
        {
            agent.isStopped=false;
        }      
    }
    
    private void Pat()
    {
        if(agent.destination != wayPoints[currentWayPoint].position)
        {
            agent.destination = wayPoints[currentWayPoint].position;
        }
        if (HasReached())
        {
           currentWayPoint = (currentWayPoint + 1) % lenght;
        }
    }

    private bool HasReached()
    {
        return (agent.hasPath && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance);
    }

    private void GetAgents()
    {
        agents = GameObject.FindGameObjectsWithTag("Agent");
        lenght_agents = agents.Length;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;


        if (m_HitDetect_fwr)
        {
//           Gizmos.DrawRay(transform.position + -transform.right*front_ofset, transform.forward * m_Hit_fwr.distance);
            Gizmos.DrawWireCube(transform.position + -transform.right*front_ofset + transform.forward * m_Hit_fwr.distance, box_fwr);
        }
        else
        {
//            Gizmos.DrawRay(transform.position + -transform.right*front_ofset, transform.forward * m_MaxDistance_fwr);
           Gizmos.DrawWireCube(transform.position + -transform.right*front_ofset + transform.forward * m_MaxDistance_fwr, box_fwr);
        }

        if (m_HitDetect_lft){
//            Gizmos.DrawRay(transform.position + transform.forward*left_ofset, -transform.right * m_Hit_lft.distance);
            Gizmos.DrawWireCube(transform.position + transform.forward*left_ofset + -transform.right * m_Hit_lft.distance, box_lft);
        }
        else{
//            Gizmos.DrawRay(transform.position + transform.forward*left_ofset, -transform.right * m_MaxDistance_lft);
            Gizmos.DrawWireCube(transform.position + transform.forward*left_ofset + -transform.right * m_MaxDistance_lft, box_lft);
        }
    }
}