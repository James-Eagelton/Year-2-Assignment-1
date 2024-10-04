using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class PlayerMovement : MonoBehaviour
{
    [Header("Raycast Point")]
    public Transform rayPoint;
    public float raycastLength;
    [Header("Movement")]
    public float moveSpeed;
    bool canMove = true;
    bool moving;
    public GameObject currentPoint;
    public GameObject nextPoint = null;
    public float moveCooldown;
    [Header("Rotation")]
    float angle;
    public float rotationTarget;
    float r;
    bool canRotate = true;
    Rigidbody rb;
    public Highlight highlight;


    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        gameObject.transform.position = currentPoint.transform.position;
    }

   
    void Update()
    {

        //Raycast To Stop The Player Moving Before Hitting A Wall
        canMove = false;
        
        if (Physics.Raycast(rayPoint.transform.position, transform.forward, out RaycastHit hit, raycastLength))
        {
            Debug.DrawLine(rayPoint.transform.position, hit.point);
            Debug.Log(hit.collider.tag);

            if (hit.collider.tag.Equals("Stop"))
            {
                Debug.Log("Hit");
                nextPoint = hit.collider.gameObject;
                canMove = true;
            }
        }
      

        //Rotating Left or Right
        if (Input.GetKey(KeyCode.A) && canRotate)
        {
            canMove = false;
            canRotate = false;
            ChangeAngle(rotationTarget - 90);
            Invoke("MoveCooldown",moveCooldown);
        }
        if (Input.GetKey(KeyCode.D) && canRotate)
        {
            canMove = false;
            canRotate = false;
            ChangeAngle(rotationTarget + 90);
            Invoke("MoveCooldown",moveCooldown);
        }

        if (gameObject.transform.rotation.y != angle) 
        { 
            angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationTarget, ref r, 0.1f);
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }


        //Moving The Player Forward
        if (Input.GetKey(KeyCode.W) && canMove) 
        {
            moving = true;
            currentPoint = nextPoint;
        }


        if (currentPoint != null && gameObject.transform.position == currentPoint.transform.position) 
        {
            currentPoint = null;
            moving = false;
        }

        if (moving) 
        {
            
            Movement();
        }


        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            moving = false;
        }

    }

    public void Movement() 
    {
        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(gameObject.transform.position, currentPoint.transform.position, step);
    }

    public void ChangeAngle(float targetAngle) 
    {
        rotationTarget = targetAngle;
    }

    public void MoveCooldown() 
    {
        canMove = true;
        canRotate = true;
    }
}
