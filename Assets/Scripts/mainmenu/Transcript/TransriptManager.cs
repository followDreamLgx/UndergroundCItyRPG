using UnityEngine;
using System.Collections;

public class TransriptManager : MonoBehaviour {

    public static TransriptManager _instance;
    public GameObject player;

    void Awake()
    {
        _instance = this;
        player = GameObject.FindGameObjectWithTag("Player") as GameObject;
    }

	
	
}
