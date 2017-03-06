﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomMap : MonoBehaviour 
{
	public int numberofRooms = 5;
	public int lengthofCorridors = 5;
	public int numberofCorners = 5;
	public int cornerPiece = 0;
	public LayerMask wall;

	string direction = "vert+";
	string lastdirection;
	int hallwayIndex = 0;
	int randomSize;
	int randomChoice;
	// Use this for initialization
	void Start () {

		Transform straightHallway = transform.GetChild (0);
		Transform leftHallway = transform.GetChild (1);
		Transform Corner1 = transform.GetChild (2);
		Transform Corner2 = transform.GetChild (3);
		Transform Corner3 = transform.GetChild (4);
		Transform Corner4 = transform.GetChild (5);
		Transform Room1 = transform.GetChild(6);
		Transform room1Plane = transform.GetChild(6);
		List<roomScript> rooms = new List<roomScript>();
		List<string> usedRoomDirections = new List<string> ();
		int roomNodeOffsets = 0;
		int totalRoomNodeOffsets = 0;

		//init rooms
		for (int i=0; i < numberofRooms; i++)
			rooms.Add (new roomScript());
		
		randomSize = Random.Range (10, lengthofCorridors);
		Transform[] currentHallway = { straightHallway, leftHallway, Corner1, Corner2 , Corner3, Corner4};

		Vector3 sizeofCurrentPlane = straightHallway.GetComponent<Renderer> ().bounds.size;

		Transform temp = transform;
		Ray wallRay = new Ray (this.transform.GetChild (0).transform.position, Vector3.forward);

		//init transform so they are not empty, but we don't use our own transform for these vars.
		Transform platform = transform;
		Transform platformNode = transform;

		for (int i = 0; i < numberofRooms; i++) {
			totalRoomNodeOffsets += roomNodeOffsets;
			roomNodeOffsets = 0;
			int dividedCorner = numberofCorners / rooms [i].numOfPaths;
			//for each path get a direction that hasn't been used yet
			getDirection();
			while (usedRoomDirections.Contains (direction)) {
				Debug.Log ("used room directions: " +usedRoomDirections);
				getDirection ();
			}
			Debug.Log ("Divided corner: " + dividedCorner);
			Debug.Log ("number of Paths: " + rooms [i].numOfPaths);
			for (int l = 0; l < rooms [i].numOfPaths; l++) {
				
				for (int k = 0; k < dividedCorner; k++) {
					randomSize = Random.Range (4, lengthofCorridors);
					for (int j = 0; j < randomSize - 1; j++) {
						if (j == 0 && i == 0)
							platform = currentHallway [hallwayIndex];
						else
							platform = transform.GetChild (transform.childCount - 1 - totalRoomNodeOffsets);

							
						sizeofCurrentPlane = platform.transform.GetComponent<Renderer> ().bounds.size;

						//if sizeofCurrentPlane is a room half it
						if (sizeofCurrentPlane.x > 10) {
							
							sizeofCurrentPlane = sizeofCurrentPlane / 2f;
							if (direction == "vert+")
								temp = Instantiate (currentHallway [hallwayIndex], new Vector3 (platform.position.x + sizeofCurrentPlane.x + 5f, platform.position.y, platform.position.z), Quaternion.identity);
							else if (direction == "vert-")
								temp = Instantiate (currentHallway [hallwayIndex], new Vector3 (platform.position.x - sizeofCurrentPlane.x - 5f, platform.position.y, platform.position.z), Quaternion.identity);
							else if (direction == "hort-")
								temp = Instantiate (currentHallway [hallwayIndex], new Vector3 (platform.position.x, platform.position.y, platform.position.z + sizeofCurrentPlane.z + 5f), currentHallway [hallwayIndex].localRotation);
							else if (direction == "hort+")
								temp = Instantiate (currentHallway [hallwayIndex], new Vector3 (platform.position.x, platform.position.y, platform.position.z - sizeofCurrentPlane.z - 5f), currentHallway [hallwayIndex].localRotation);
							temp.transform.SetParent (this.transform);
						} else {
							if (direction == "vert+")
								temp = Instantiate (currentHallway [hallwayIndex], new Vector3 (platform.position.x + sizeofCurrentPlane.x, platform.position.y, platform.position.z), Quaternion.identity);
							else if (direction == "vert-")
								temp = Instantiate (currentHallway [hallwayIndex], new Vector3 (platform.position.x - sizeofCurrentPlane.x, platform.position.y, platform.position.z), Quaternion.identity);
							else if (direction == "hort-")
								temp = Instantiate (currentHallway [hallwayIndex], new Vector3 (platform.position.x, platform.position.y, platform.position.z + sizeofCurrentPlane.z), currentHallway [hallwayIndex].localRotation);
							else if (direction == "hort+")
								temp = Instantiate (currentHallway [hallwayIndex], new Vector3 (platform.position.x, platform.position.y, platform.position.z - sizeofCurrentPlane.z), currentHallway [hallwayIndex].localRotation);
							temp.transform.SetParent (this.transform);
						}
						roomNodeOffsets++;
					}

					randomChoice = Random.Range (0, 3);



					lastdirection = direction;

					if (direction.Contains ("hort") && randomChoice == 0) {

						hallwayIndex = 0;

						if (direction.Contains ("hort+")) {
							cornerPiece = 5;
						} else {
							cornerPiece = 4;
						}

						direction = "vert+";

					} else if (direction.Contains ("hort") && randomChoice == 1) {

						hallwayIndex = 0;

						if (direction.Contains ("hort+")) {
							cornerPiece = 3;
						} else {
							cornerPiece = 2;
						}

						direction = "vert-";
					} else if (direction.Contains ("hort") && randomChoice == 2) {
						cornerPiece = 1;
					} else if (direction.Contains ("vert") && randomChoice == 0) {

						hallwayIndex = 1;

						if (direction.Contains ("vert+")) {
							cornerPiece = 2;
						} else {
							cornerPiece = 4;
						}

						direction = "hort+";

					} else if (direction.Contains ("vert") && randomChoice == 1) {

						hallwayIndex = 1;


						if (direction.Contains ("vert+")) {
							cornerPiece = 3;
						} else {
							cornerPiece = 5;
						}

						direction = "hort-";
					} else if (direction.Contains ("vert") && randomChoice == 2) {
						//do nothing, keep going in same direction
						cornerPiece = 0;
					}


					//add the last piece of tile here
					platform = transform.GetChild (transform.childCount - 1);



					platformNode = platform.transform.GetChild (1);
					sizeofCurrentPlane = platform.GetComponent<Renderer> ().bounds.size;

					if (lastdirection == "vert+")
						temp = Instantiate (currentHallway [cornerPiece], new Vector3 (platform.position.x + sizeofCurrentPlane.x, platform.position.y, platform.position.z), Quaternion.identity);
					else if (lastdirection == "vert-")
						temp = Instantiate (currentHallway [cornerPiece], new Vector3 (platform.position.x - sizeofCurrentPlane.x, platform.position.y, platform.position.z), Quaternion.identity);
					else if (lastdirection == "hort-")
						temp = Instantiate (currentHallway [cornerPiece], new Vector3 (platform.position.x, platform.position.y, platform.position.z + sizeofCurrentPlane.z), currentHallway [cornerPiece].localRotation);
					else if (lastdirection == "hort+")
						temp = Instantiate (currentHallway [cornerPiece], new Vector3 (platform.position.x, platform.position.y, platform.position.z - sizeofCurrentPlane.z), currentHallway [cornerPiece].localRotation);

					temp.transform.SetParent (this.transform);
					roomNodeOffsets++;
					usedRoomDirections.Add (direction);
				}
			}
			usedRoomDirections.Clear ();
			roomNodeOffsets = 0;
			platform = transform.GetChild (transform.childCount - 1);

			sizeofCurrentPlane = room1Plane.GetComponent<Renderer> ().bounds.size;

			Debug.Log (platform.position);
			Debug.Log (platform.GetComponent<Renderer> ().bounds.size.x / 2f);
			Debug.Log (sizeofCurrentPlane);
			sizeofCurrentPlane = (new Vector3 (sizeofCurrentPlane.x + platform.GetComponent<Renderer> ().bounds.size.x / 2f + 5f, sizeofCurrentPlane.y, sizeofCurrentPlane.z + platform.GetComponent<Renderer> ().bounds.size.z / 2f + 5f));
			Debug.Log (sizeofCurrentPlane);
			if (direction == "vert+")
				temp = Instantiate (Room1, new Vector3 (platform.position.x + sizeofCurrentPlane.x / 2f, platform.position.y, platform.position.z), Quaternion.identity);
			else if (direction == "vert-")
				temp = Instantiate (Room1, new Vector3 (platform.position.x - sizeofCurrentPlane.x / 2f, platform.position.y, platform.position.z), Quaternion.identity);
			else if (direction == "hort-")
				temp = Instantiate (Room1, new Vector3 (platform.position.x, platform.position.y, platform.position.z + sizeofCurrentPlane.z / 2f), Quaternion.identity);
			else if (direction == "hort+")
				temp = Instantiate (Room1, new Vector3 (platform.position.x, platform.position.y, platform.position.z - sizeofCurrentPlane.z / 2f), Quaternion.identity);

			temp.transform.SetParent (this.transform);

			randomChoice = Random.Range (0, 3);

			//set direction for platform after room

			lastdirection = direction;

			if (direction.Contains ("hort") && randomChoice == 0) {

				hallwayIndex = 0;

				if (direction.Contains ("hort+")) {
					cornerPiece = 5;
				} else {
					cornerPiece = 4;
				}

				direction = "vert+";

			} else if (direction.Contains ("hort") && randomChoice == 1) {

				hallwayIndex = 0;

				if (direction.Contains ("hort+")) {
					cornerPiece = 3;
				} else {
					cornerPiece = 2;
				}

				direction = "vert-";
			} else if (direction.Contains ("hort") && randomChoice == 2) {
				cornerPiece = 1;
			} else if (direction.Contains ("vert") && randomChoice == 0) {

				hallwayIndex = 1;

				if (direction.Contains ("vert+")) {
					cornerPiece = 2;
				} else {
					cornerPiece = 4;
				}

				direction = "hort+";

			} else if (direction.Contains ("vert") && randomChoice == 1) {

				hallwayIndex = 1;


				if (direction.Contains ("vert+")) {
					cornerPiece = 3;
				} else {
					cornerPiece = 5;
				}

				direction = "hort-";
			} else if (direction.Contains ("vert") && randomChoice == 2) {
				//do nothing, keep going in same direction
				cornerPiece = 0;
			}
		}
	}

	// Update is called once per frame
	void Update ()
	{

	}

	void getDirection(){


		lastdirection = direction;

		if (direction.Contains ("hort") && randomChoice == 0) {

			hallwayIndex = 0;

			if (direction.Contains ("hort+")) {
				cornerPiece = 5;
			} else {
				cornerPiece = 4;
			}

			direction = "vert+";

		} else if (direction.Contains ("hort") && randomChoice == 1) {

			hallwayIndex = 0;

			if (direction.Contains ("hort+")) {
				cornerPiece = 3;
			} else {
				cornerPiece = 2;
			}

			direction = "vert-";
		} else if (direction.Contains ("hort") && randomChoice == 2) {
			cornerPiece = 1;
		} else if (direction.Contains ("vert") && randomChoice == 0) {

			hallwayIndex = 1;

			if (direction.Contains ("vert+")) {
				cornerPiece = 2;
			} else {
				cornerPiece = 4;
			}

			direction = "hort+";

		} else if (direction.Contains ("vert") && randomChoice == 1) {

			hallwayIndex = 1;


			if (direction.Contains ("vert+")) {
				cornerPiece = 3;
			} else {
				cornerPiece = 5;
			}

			direction = "hort-";
		} else if (direction.Contains ("vert") && randomChoice == 2) {
			//do nothing, keep going in same direction
			cornerPiece = 0;
		}


	}
}
