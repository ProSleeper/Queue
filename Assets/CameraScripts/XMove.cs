using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XMove : MonoBehaviour {

	public bool isMove;
	public float speed;
	public bool isLR;

	void Start () {
		isMove = true;
		speed = -5.0f;
	}
	
	void Update () {


		if (isLR)
		{
            if (this.transform.position.x <= -4.0f || this.transform.position.x >= 4.0f)
            {
                speed *= -1;
            }

            if (isMove)
            {
                this.transform.Translate(new Vector3(speed * Time.deltaTime, 0.0f, 0.0f));
            }
        }
        else
        {
            if (this.transform.position.z <= -4.0f || this.transform.position.z >= 4.0f)
            {
                speed *= -1;
            }

            if (isMove)
            {
                this.transform.Translate(new Vector3(0.0f, 0.0f, speed * Time.deltaTime));
            }
        }
	}
}
