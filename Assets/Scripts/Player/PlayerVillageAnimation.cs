using UnityEngine;
using System.Collections;

public class PlayerVillageAnimation : MonoBehaviour {

    private Animator _anim;
    private Rigidbody _rigidBody;
    private NavMeshAgent agent;
	// Use this for initialization
	
    void Awake()
    {
        _anim = this.GetComponent<Animator>();
        _rigidBody = this.GetComponent<Rigidbody>();
        agent = this.GetComponent<NavMeshAgent>();
    }
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        if (_rigidBody.velocity.magnitude > 0.5f)
        {
            _anim.SetBool("Move", true);
        }
        else
        {
            _anim.SetBool("Move", false);
        }
        if (agent.enabled == true)
            _anim.SetBool("Move", true);

	}
}
