using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

	public GameObject cube;
    public GameObject startCube;
    public GameObject mainCamera;
    

    List<GameObject> cubeList = new List<GameObject>();
    float Xvalue, Yvalue;
    float ChangeRotation;
    float cameraYvalue;
    Quaternion rotation;

    static int idx = 1;

    // Use this for initialization
    void Start()
    {
        //StartCoroutine(CubeSpawn());
        InitCube();
		cubeList.Add(startCube);
		//Debug.Log(cubeList[0].transform.position);
		CubeSpawn();
		
    }

    void InitCube()
    {
        Xvalue = 3.0f;
        Yvalue = 0.0f;
        ChangeRotation = 45.0f;
        cubeList.Clear();
        rotation = Quaternion.Euler(0.0f, ChangeRotation, 0.0f);

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CubeSpawn();
            //mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, cameraYvalue , mainCamera.transform.position.z);
			iTween.MoveBy(mainCamera, iTween.Hash("y", 0.34f, "z", -0.34f, "time", 0.2f, "easetype", "linear"));
        }
    }

	void CubeSpawn()
	{
		cubeList.Add(Instantiate(cube, new Vector3(Xvalue, Yvalue, 3.0f), rotation) as GameObject);
		
		//겹치는지 확인 코드
        
		///////////////////

        

		Xvalue *= -1;
		Yvalue += 0.5f;
		ChangeRotation *= -1;
		rotation = Quaternion.Euler(0.0f, ChangeRotation, 0.0f);
		//cameraYvalue = mainCamera.transform.position.y + 0.5f;
		//Debug.Log(cubeList.Count);
	}

    public void CubeCut()
    {
        Debug.Log(cubeList.Count);
        Debug.Log("idx: " + idx);
        cubeList[idx].transform.position = new Vector3(cubeList[idx - 1].transform.position.x,cubeList[idx].transform.position.y, cubeList[idx - 1].transform.position.z);
        idx++;
    }
}