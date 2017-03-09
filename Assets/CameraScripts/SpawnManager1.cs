using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SpawnManager1 : MonoBehaviour
{

	public GameObject cube;
    public GameObject cutCube;
    public GameObject startCube;
    public GameObject mainCamera;
    GameObject tempGameObject;

    List<GameObject> cubeList = new List<GameObject>();
    float Xvalue, Yvalue, Zvalue;
    bool checkXY;
    bool isGameOver;
    

    void Start()
    {


        InitCube();
    }

    void Update()
    {

        if (isGameOver)
        {
            SceneManager.LoadScene(0);
        }

        if (Input.GetMouseButtonDown(0))
        {
            CubeCut();
            CubeMake();
			iTween.MoveBy(mainCamera, iTween.Hash("y", 0.35f, "z", -0.35f, "time", 0.1f, "easetype", "linear"));
        }
    }

    void InitCube()
    {
        isGameOver = false;
        checkXY = true;
        Xvalue = 3.0f;
        Yvalue = 0.0f;
        Zvalue = 0.0f;
        cubeList.Clear();
        cubeList.Add(startCube);
        CubeMake();
    }

    void CubeMake()
    {
        tempGameObject = Instantiate(cube, new Vector3(Xvalue, Yvalue, Zvalue), Quaternion.identity) as GameObject;

        //현재 만든 블럭의 크기를 바로 밑의 블럭의 크기로 맞춤
        tempGameObject.transform.position = new Vector3(cubeList[cubeList.Count - 1].transform.position.x + Xvalue, Yvalue, cubeList[cubeList.Count - 1].transform.position.z + Zvalue);
        tempGameObject.transform.localScale = cubeList[cubeList.Count - 1].transform.localScale;

        cubeList.Add(tempGameObject);

        //여기에 생성된 블럭의 크기를 밑의 블럭과 같게 만들어야함.

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

        //x값과 z값을 계속 바꿔서 젠 위치를 바꿔줌
        float temp = Xvalue;
        Xvalue = Zvalue;
        Zvalue = temp;

        Yvalue += 0.5f;        //높이는 항상 일정하게 증가함
    }

    void CubeCut()
    {
        //움직이는 블럭 멈춤
        cubeList[cubeList.Count - 1].GetComponent<XMove>().isMove = false;

        Transform backOne = cubeList[cubeList.Count - 2].transform;       //기준 블럭
        Transform backTwo = cubeList[cubeList.Count - 1].transform;         //움직이는 블럭


        //X와 Z 어느 방향으로 움직이는지 판별
        if (backTwo.GetComponent<XMove>().isLR)     //x축으로 움직일때
        {

            // if (neverNumber(backTwo.position.x - backOne.position.x) > 2.0f ||
            //     )
            if ((backTwo.position.x - (backTwo.localScale.x / 2.0f)) >= (backOne.position.x + (backOne.localScale.x / 2.0f)) || 
                (backTwo.position.x + (backTwo.localScale.x / 2.0f) <= (backOne.position.x - (backOne.localScale.x / 2.0f))))
                {
                    Debug.Log("GameOver");
                    isGameOver = true;
                    return;
                }

            //기준 블럭보다 X값이 클때
            if (backTwo.position.x - backOne.position.x >= 0)
            {
                //겹치는 부분의 블럭
                float xPos = (backOne.position.x + (backOne.localScale.x / 2.0f)) - (backTwo.position.x - (backTwo.localScale.x / 2.0f));
                float cutXpos = (backTwo.position.x + (backTwo.localScale.x / 2.0f)) - (backOne.position.x + (backOne.localScale.x / 2.0f));
                
                GameObject cut = Instantiate(cutCube, new Vector3(Xvalue, backTwo.position.y, Zvalue), Quaternion.identity) as GameObject;
                cut.transform.position = new Vector3(backTwo.position.x + (backTwo.localScale.x / 2.0f) - (cutXpos / 2.0f), backTwo.position.y, backTwo.position.z);
                cut.transform.localScale = new Vector3(cutXpos, 0.5f, backTwo.localScale.z);

                backTwo.position = new Vector3((backTwo.position.x - backTwo.localScale.x / 2.0f) + (xPos / 2.0f), backTwo.position.y, backTwo.position.z);
                backTwo.localScale = new Vector3(xPos, backTwo.localScale.y, backTwo.localScale.z);
            }
            //기준 블럭보다 X값이 작을때
            else
            {
                float xPos = (backTwo.position.x + (backTwo.localScale.x / 2.0f)) - (backOne.position.x - (backOne.localScale.x / 2.0f));
                float cutXpos = (backOne.position.x - (backOne.localScale.x / 2.0f)) - (backTwo.position.x - (backTwo.localScale.x / 2.0f));

                GameObject cut = Instantiate(cutCube, new Vector3(Xvalue, backTwo.position.y, Zvalue), Quaternion.identity) as GameObject;
                cut.transform.position = new Vector3(backTwo.position.x - (backTwo.localScale.x / 2.0f) + (cutXpos / 2.0f), backTwo.position.y, backTwo.position.z);
                cut.transform.localScale = new Vector3(cutXpos, 0.5f, backTwo.localScale.z);

                backTwo.position = new Vector3((backTwo.position.x + backTwo.localScale.x / 2.0f) - (xPos / 2.0f), backTwo.position.y, backTwo.position.z);
                backTwo.localScale = new Vector3(xPos, backTwo.localScale.y, backTwo.localScale.z);
            }
        }
        else        //z축으로 움직일때
        {
            if ((backTwo.position.z - (backTwo.localScale.z / 2.0f)) >= (backOne.position.z + (backOne.localScale.z / 2.0f)) ||
                (backTwo.position.z + (backTwo.localScale.z / 2.0f) <= (backOne.position.z - (backOne.localScale.z / 2.0f))))
            {
                Debug.Log("GameOver");
                isGameOver = true;
                return;
            }

            //기준 블럭보다 Z값이 클때
            if (backTwo.position.z - backOne.position.z >= 0)
            {
                float zPos = (backOne.position.z + (backOne.localScale.z / 2.0f)) - (backTwo.position.z - (backTwo.localScale.z / 2.0f));
                float cutZpos = (backTwo.position.z + (backTwo.localScale.z / 2.0f)) - (backOne.position.z + (backOne.localScale.z / 2.0f));

                GameObject cut = Instantiate(cutCube, new Vector3(Xvalue, backTwo.position.y, Zvalue), Quaternion.identity) as GameObject;
                cut.transform.position = new Vector3(backTwo.position.x, backTwo.position.y, backTwo.position.z + (backTwo.localScale.z / 2.0f) - (cutZpos / 2.0f));
                cut.transform.localScale = new Vector3(backTwo.localScale.x, 0.5f, cutZpos);

                backTwo.position = new Vector3(backTwo.position.x, backTwo.position.y, (backTwo.position.z - (backTwo.localScale.z / 2.0f) + (zPos / 2.0f)));
                backTwo.localScale = new Vector3(backTwo.localScale.x, backTwo.localScale.y, zPos);
                
            }
            //기준 블럭보다 Z값이 작을때
            else
            {
                float zPos = (backTwo.position.z + (backTwo.localScale.z / 2.0f) - (backOne.position.z - (backOne.localScale.z / 2.0f)));
                float cutZpos = (backOne.position.z - (backOne.localScale.z / 2.0f)) - (backTwo.position.z - (backTwo.localScale.z / 2.0f));

                GameObject cut = Instantiate(cutCube, new Vector3(Xvalue, backTwo.position.y, Zvalue), Quaternion.identity) as GameObject;
                cut.transform.position = new Vector3(backTwo.position.x, backTwo.position.y, backTwo.position.z - (backTwo.localScale.z / 2.0f) + (cutZpos / 2.0f));
                cut.transform.localScale = new Vector3(backTwo.localScale.x, 0.5f, cutZpos);

                backTwo.position = new Vector3(backTwo.position.x, backTwo.position.y, (backTwo.position.z + (backTwo.localScale.z / 2.0f) - (zPos / 2.0f)));
                backTwo.localScale = new Vector3(backTwo.localScale.x, backTwo.localScale.y, zPos);
            }
        }
    }

    float neverNumber(float num)
    {
        if (num < 0.0f)
        {
            return (num * -1.0f);
        }
        return num;
    }
}





// //크기를 계산하기 위해서 있는 변수 xx,zz
//             float xx;
//             float zz;

//             if (backTwo.transform.position.x > backThree.position.x )      //큰값에서 작은 값을 빼주기 위해서 필요한 조건
//             {
//                 xx = backThree.position.x - backTwo.transform.position.x;
//             }
//             else
//             {
//                 xx = backTwo.transform.position.x - backThree.position.x;
//             }


//             if (backTwo.transform.position.z > backThree.position.z)      //큰값에서 작은 값을 빼주기 위해서 필요한 조건
//             {
//                 zz  = backThree.position.z - backTwo.transform.position.z;
//             }
//             else
//             {
//                 zz = backTwo.transform.position.z - backThree.position.z;
//             }

//             Debug.Log("xx: " + xx);
//             Debug.Log("zz: " + zz);
//             //클릭시 블럭의 움직임을 멈추게 할 코드
//             if (backTwo.GetComponent<XMove>().isLR)     //x축으로 움직이는경우
//             {
//                 //Debug.Log("xx: " + xx);

//                 backTwo.localScale = new Vector3(backTwo.transform.localScale.x + xx, backTwo.transform.localScale.y, backTwo.transform.localScale.z);
                
//                 //backTwo.position = new Vector3(backTwo.transform.position.x - (xx / 2), backTwo.transform.position.y, backTwo.transform.position.z);
                
//                 //Debug.Log("멈춤 블럭의 x: " + backTwo.transform.position.x);
//                 tempObj.position = new Vector3(backTwo.transform.position.x, tempObj.position.y, tempObj.position.z);
//                 tempObj.localScale = new Vector3(backTwo.transform.localScale.x, tempObj.localScale.y, backTwo.transform.localScale.z);

//             }
//             else    //z축으로 움직이는 경우
//             {
//                 //Debug.Log("zz: " + zz);
//                 backTwo.localScale = new Vector3(backTwo.localScale.x, backTwo.localScale.y, backTwo.localScale.z + zz);
                
//                //backTwo.position = new Vector3(backTwo.position.x, backTwo.position.y, backTwo.position.z - (zz / 2));
                
//                 //Debug.Log("멈춤 블럭의 z: " + backTwo.transform.position.z);
//                 tempObj.position = new Vector3(tempObj.position.x, tempObj.position.y, backTwo.position.z);
//                 tempObj.localScale = new Vector3(backTwo.localScale.x, tempObj.localScale.y, backTwo.localScale.z);
//             }