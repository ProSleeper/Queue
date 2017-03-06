using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

	bool isMove;
	public float speed;

	// Use this for initialization
	void Start () {
		isMove = true;
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown(0))
		{
			isMove = false;
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
