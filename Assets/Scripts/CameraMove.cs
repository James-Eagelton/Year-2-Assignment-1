using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraMove : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject viewPoint;
    public GameObject mapPoint;
    public float mouseSensitivity = 100f;
    float xRotation = 0f;
    float yRotation = 0f;

    public bool freeRotation;

 

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt)) 
        {
            freeRotation = !freeRotation;
            
        }

        if (freeRotation) 
        {
            
            Cursor.lockState = CursorLockMode.Locked;
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            yRotation += mouseX;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            yRotation = Mathf.Clamp(yRotation, -90f, 90f);

            mainCamera.transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
            mainCamera.transform.Rotate(Vector3.up * mouseX);
            mainCamera.transform.Rotate(Vector3.left * mouseY);

        }
        
        if (!freeRotation)
        {
            Cursor.lockState = CursorLockMode.None;
            mainCamera.transform.position = viewPoint.transform.position;
            mainCamera.transform.rotation = viewPoint.transform.rotation;
        }


       


       
    }
}
