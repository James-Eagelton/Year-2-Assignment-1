using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CameraMove : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject viewPoint;
    public PlayerMovement playerMovement;
    public float mouseSensitivity = 100f;
    float xRotation = 0f;
    float yRotation = 0f;

    public bool freeRotation;
    public bool checkFreeRotation;



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            freeRotation = !freeRotation;
            checkFreeRotation = true;


        }

        if (freeRotation)
        {
            if (checkFreeRotation)
            {
                playerMovement.canRotate = false;
                checkFreeRotation = false;
            }



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
            mainCamera.transform.position = viewPoint.transform.position;
            
            if (checkFreeRotation)
            {
                playerMovement.canRotate = true;
                checkFreeRotation = false;
                xRotation = viewPoint.transform.rotation.x;
                yRotation = viewPoint.transform.rotation.y;
            }
            Vector3 viewPointsRotation = new Vector3(viewPoint.transform.rotation.eulerAngles.x, viewPoint.transform.rotation.eulerAngles.y, 0);
            Quaternion targetRotation = Quaternion.Euler(viewPointsRotation);
            mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, targetRotation, Time.deltaTime * 5);

            Cursor.lockState = CursorLockMode.None;
            
        }

  
    }
}
