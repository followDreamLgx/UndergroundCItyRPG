using UnityEngine;
using System.Collections;

public class PlayerVillageMove : MonoBehaviour {

    public float velocity = 5;
    Rigidbody _rigidBody;
    private NavMeshAgent agent;

    void Awake()
    {
        agent = this.GetComponent<NavMeshAgent>();
        _rigidBody = this.GetComponent<Rigidbody>();
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 vel = _rigidBody.velocity;
        
        if(Mathf.Abs(h) > 0.5f || Mathf.Abs(v) > 0.5f)
        {
            _rigidBody.velocity = new Vector3(-h * velocity, vel.y, -v * velocity);
            transform.rotation = Quaternion.LookRotation(new Vector3(-h, 0,-v));
            Debug.Log("根据输入进行了旋转");
        }
        else if(agent.enabled == false)
        {
            _rigidBody.velocity = Vector3.zero;
        }
        if(agent.enabled == true)
        {
            //transform.rotation = Quaternion.LookRotation(agent.velocity);
        }
	}
}
