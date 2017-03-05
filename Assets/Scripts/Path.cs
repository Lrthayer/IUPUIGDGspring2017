using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour {

	public GameObject nodeUser;
	public GameObject player;
	public Transform[] nodes;
	private int currentNode;
	public float reachDistance = 1.0f;
	public float sightDistance = 10;
	public float speed;
	private bool chasing = false;

	// Use this for initialization
	void Start () {
		currentNode = 0;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 dir = nodes [currentNode].position - nodeUser.transform.position;

		if (!chasing)
		{
			nodeUser.transform.position += dir * Time.deltaTime * speed;
		}
		if (dir.magnitude <= reachDistance) {
			currentNode++;
		}

		if (currentNode == 4) {
			currentNode = 0;
		}
		//check for user being in sight
		checkSight();

	}


	void checkSight()
	{
		Vector3 playerDir = player.transform.position - nodeUser.transform.position ;

		if (playerDir.magnitude <= sightDistance)
		{
			nodeUser.transform.position += playerDir * Time.deltaTime * speed;
			chasing = true;
		}
		else
		{
			chasing = false;
		}

	}
		
}