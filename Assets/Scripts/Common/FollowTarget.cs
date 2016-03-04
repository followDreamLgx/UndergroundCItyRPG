using UnityEngine;
using System.Collections;

public class FollowTarget : MonoBehaviour {

    public Vector3 offSet;
    private Transform player;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform.Find("Bip01").GetComponent<Transform>();
    }
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = player.position + offSet;

	}
}
