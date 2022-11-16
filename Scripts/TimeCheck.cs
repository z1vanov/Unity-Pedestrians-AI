using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCheck : MonoBehaviour
{
    static public int count;
    public int count_show;
    public Text currentText;
    private float currentTime;
    static public int num_agents;
    static public bool flag;
    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        flag = false;
        currentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        count_show = count;
        if(flag){
            currentTime = currentTime + Time.deltaTime;
            currentText.text = currentTime.ToString();
            if(count == num_agents + 1)
                flag = false;
        }
    }
}
