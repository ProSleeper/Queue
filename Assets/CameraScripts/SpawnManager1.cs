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

        cubeList.Add(tempGameObject);

         //블럭 생성 후 어느 방향으로 움직일지 결정
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

        //크기를 계산하기 위해서 있는 변수 xx,zz
       

        //x값과 z값을 계속 바꿔서 젠 위치를 바꿔줌
        float temp = Xvalue;
        Xvalue = Zvalue;
        Zvalue = temp;

        Yvalue += 0.5f;        //높이는 항상 일정하게 증가함




        if (cubeList.Count > 2)
        {
            Transform backThree = cubeList[cubeList.Count - 3].transform;       //현재 움직이는 블럭 아래 블럭
            Transform backTwo = cubeList[cubeList.Count - 2].transform;         //현재 움직이는 블럭
            Transform tempObj = tempGameObject.transform;                       //새로 만들어진 블럭
            cubeList[cubeList.Count - 2].GetComponent<XMove>().isMove = false;
            float xx;
            float zz;

            if (backTwo.transform.position.x > backThree.position.x )      //큰값에서 작은 값을 빼주기 위해서 필요한 조건
            {
                xx = backThree.position.x - backTwo.transform.position.x;
            }
            else
            {
                xx = backTwo.transform.position.x - backThree.position.x;
            }


            if (backTwo.transform.position.z > backThree.position.z)      //큰값에서 작은 값을 빼주기 위해서 필요한 조건
            {
                zz  = backThree.position.z - backTwo.transform.position.z;
            }
            else
            {
                zz = backTwo.transform.position.z - backThree.position.z;
            }

            Debug.Log("xx: " + xx);
            Debug.Log("zz: " + zz);
            //클릭시 블럭의 움직임을 멈추게 할 코드
            if (backTwo.GetComponent<XMove>().isLR)     //x축으로 움직이는경우
            {
                //Debug.Log("xx: " + xx);

                backTwo.localScale = new Vector3(backTwo.transform.localScale.x + xx, backTwo.transform.localScale.y, backTwo.transform.localScale.z);
                
                //backTwo.position = new Vector3(backTwo.transform.position.x - (xx / 2), backTwo.transform.position.y, backTwo.transform.position.z);
                
                //Debug.Log("멈춤 블럭의 x: " + backTwo.transform.position.x);
                tempObj.position = new Vector3(backTwo.transform.position.x, tempObj.position.y, tempObj.position.z);
                tempObj.localScale = new Vector3(backTwo.transform.localScale.x, tempObj.localScale.y, backTwo.transform.localScale.z);

            }
            else    //z축으로 움직이는 경우
            {
                //Debug.Log("zz: " + zz);
                backTwo.localScale = new Vector3(backTwo.localScale.x, backTwo.localScale.y, backTwo.localScale.z + zz);
                
               //backTwo.position = new Vector3(backTwo.position.x, backTwo.position.y, backTwo.position.z - (zz / 2));
                
                //Debug.Log("멈춤 블럭의 z: " + backTwo.transform.position.z);
                tempObj.position = new Vector3(tempObj.position.x, tempObj.position.y, backTwo.position.z);
                tempObj.localScale = new Vector3(backTwo.localScale.x, tempObj.localScale.y, backTwo.localScale.z);
            }
        }
    }
}