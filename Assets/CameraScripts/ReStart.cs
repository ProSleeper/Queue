using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReStart : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        this.gameObject.GetComponent<Button>().onClick.AddListener(ReLoadScene);
    }

    // Update is called once per frame
    void ReLoadScene()
    {
		SceneManager.LoadScene(0);
    }
}
