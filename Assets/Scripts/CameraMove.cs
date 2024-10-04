using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject viewPoint;
    public GameObject mapPoint;
    public bool firstPerson;
    
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.M)) 
        {
            firstPerson = !firstPerson;
        }
        
        if (firstPerson)
        {
            mainCamera.transform.position = viewPoint.transform.position;
            mainCamera.transform.rotation = viewPoint.transform.rotation;
        }
        else 
        {
            mainCamera.transform.position = mapPoint.transform.position;
            mainCamera.transform.rotation = mapPoint.transform.rotation;
        }
    }
}
