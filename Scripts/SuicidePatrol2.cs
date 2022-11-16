using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SuicidePatrol2 : MonoBehaviour
{
    private GameObject[] other;
    int len;
    public GameObject fire;
    public GameObject expl;
    public float delay;
    // Start is called before the first frame update
    void Start()
    {
        other = GameObject.FindGameObjectsWithTag("Explosion");
        len=other.Length;
    }

    // Update is called once per frame
    void Update()
    {
        Burn();
    }

    private void Burn(){
        for(int i=0;i<len;i++)
        {
            if(Vector3.Distance(other[i].transform.position, transform.position)<55)
            {
                StartCoroutine(BurnCoroutine());
            }        
        }
    }
    IEnumerator BurnCoroutine ()
    {
        this.GetComponent<NavMeshAgent>().speed = 40;
        fire.SetActive(true);
        this.GetComponent<Animator>().SetBool("run", true);
        this.GetComponent<NavMeshAgent>().avoidancePriority = 30;
        yield return new WaitForSeconds(delay);
        this.gameObject.SetActive(false);
        Destroy(this.gameObject);
        Patrolv3.num_agents -= 1;
        TimeCheck.num_agents -= 1;
    }
}
