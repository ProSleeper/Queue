using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager1 : MonoBehaviour
{

	public GameObject cube;
    public GameObject startCube;
    public GameObject mainCamera;
    GameObject tempGameObject;
    

    List<GameObject> cubeList = new List<GameObject>();
    float Xvalue, Yvalue, Zvalue;
    bool checkXY;

    void Start()
    {
        checkXY = true;
        InitCube();
		cubeList.Add(startCube);
		CubeSpawn();
        
    }

    void InitCube()
    {
        Xvalue = 3.0f;
        Yvalue = 0.0f;
        Zvalue = 0.0f;
        cubeList.Clear();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CubeSpawn();
			iTween.MoveBy(mainCamera, iTween.Hash("y", 0.35f, "z", -0.35f, "time", 0.2f, "easetype", "linear"));
        }
    }

	void CubeSpawn()
	{
        tempGameObject = Instantiate(cube, new Vector3(Xvalue, Yvalue, Zvalue), Quaternion.identity) as GameObject;
        //Debug.Log(cubeList.Count);

        Debug.Log(cubeList[cubeList.Count - 1].transform.localScale.x);
        Debug.Log(tempGameObject.transform.localScale.y);
        Debug.Log(cubeList[cubeList.Count - 1].transform.localScale.z);

        cubeList.Add(tempGameObject);
        
        if (checkXY)
        {
            tempGameObject.GetComponent<XMove>().isLR = true;
            checkXY = false;
        }
        else
        {
            tempGameObject.GetComponent<XMove>().isLR = false;
            checkXY = true;
        }

        if (cubeList.Count > 2)
        {
            cubeList[cubeList.Count - 2].GetComponent<XMove>().isMove = false;

            float xx = cubeList[cubeList.Count - 3].transform.position.x + cubeList[cubeList.Count - 2].transform.position.x;
            float zz = cubeList[cubeList.Count - 3].transform.position.z + cubeList[cubeList.Count - 2].transform.position.z;

            if (cubeList[cubeList.Count - 2].GetComponent<XMove>().isLR)
            {
                if (xx > 0.0f)
                {
                    xx *= -1.0f;
                }
                cubeList[cubeList.Count - 2].transform.position = new Vector3(cubeList[cubeList.Count - 2].transform.position.x / 2, cubeList[cubeList.Count - 2].transform.position.y, cubeList[cubeList.Count - 2].transform.position.z);
                cubeList[cubeList.Count - 2].transform.localScale = new Vector3(cubeList[cubeList.Count - 2].transform.localScale.x + xx, cubeList[cubeList.Count - 2].transform.localScale.y, cubeList[cubeList.Count - 2].transform.localScale.z);
                
                
                tempGameObject.transform.localScale = new Vector3(cubeList[cubeList.Count - 2].transform.localScale.x, tempGameObject.transform.localScale.y, cubeList[cubeList.Count - 2].transform.localScale.z);
               
               
                tempGameObject.transform.position = new Vector3(cubeList[cubeList.Count - 2].transform.position.x, tempGameObject.transform.position.y, tempGameObject.transform.position.z);
            
            }
            else
            {
                if (zz > 0.0f)
                {
                    zz *= -1.0f;
                }
                cubeList[cubeList.Count - 2].transform.position = new Vector3(cubeList[cubeList.Count - 2].transform.position.x, cubeList[cubeList.Count - 2].transform.position.y, cubeList[cubeList.Count - 2].transform.position.z/2);
                cubeList[cubeList.Count - 2].transform.localScale = new Vector3(cubeList[cubeList.Count - 2].transform.localScale.x, cubeList[cubeList.Count - 2].transform.localScale.y, cubeList[cubeList.Count - 2].transform.localScale.z + zz);
                
                
                tempGameObject.transform.localScale = new Vector3(cubeList[cubeList.Count - 2].transform.localScale.x, tempGameObject.transform.localScale.y, cubeList[cubeList.Count - 2].transform.localScale.z);
                
                
                tempGameObject.transform.position = new Vector3(tempGameObject.transform.position.x, tempGameObject.transform.position.y, cubeList[cubeList.Count - 2].transform.position.z);
            }

            // Debug.Log("xx: " + xx);
            // Debug.Log("zz: " + zz);
            
        

        }

        float temp = Xvalue;
        Xvalue = Zvalue;
        Zvalue = temp;

		Yvalue += 0.5f;
        
	}
}