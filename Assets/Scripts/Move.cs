using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

	bool isMove;
	public float speed;
	public GameObject SManager;

	// Use this for initialization
	void Start () {
		isMove = true;
		SManager = GameObject.Find("GameManager");
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown(0) && isMove)
		{
			isMove = false;
			SManager.GetComponent<SpawnManager>().CubeCut();
		}
		
		if (this.transform.position.z <= -3.5f || this.transform.position.z >= 3.5f)
		{
			speed *= -1;
		}

		if (isMove)
		{
			this.transform.Translate(new Vector3(0.0f, 0.0f, speed * Time.deltaTime));
		}
		
	}
}
