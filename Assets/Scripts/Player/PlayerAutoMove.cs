using UnityEngine;
using System.Collections;

public class PlayerAutoMove : MonoBehaviour {


    private NavMeshAgent agent;
    public Transform target;
	// Use this for initialization
	void Start () {
        agent = this.GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
	    if(agent.enabled == true)
        {
            if(agent.remainingDistance < 2 && agent.remainingDistance != 0)//与目标点的
            {
                Debug.Log("到啦到啦");
                TaskManager._instance.OnArriveDestination();
                agent.Stop();
                agent.enabled = false;
            }
        }
        //float h = Input.GetAxis("Horizontal");
        //float v = Input.GetAxis("Vertical");
        //if (Mathf.Abs(h) > 0.5f || Mathf.Abs(v) > 0.5f)
        //{
        //    //StopAuto();//寻路过程中按下移动控制键就停止寻路
        //}
	}
    public void SetDestination(Vector3 tarPos)
    {
        agent.enabled = true;
        agent.SetDestination(tarPos);
    }
    public void StopAuto()
    {
        if(agent.enabled)
        {
            agent.Stop();
            agent.enabled = false;
        }
    }
}
