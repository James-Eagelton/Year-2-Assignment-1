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
    public bool canMove = true;
    bool moving;
    public GameObject currentPoint;
    public GameObject nextPoint = null;
    public float moveCooldown;
    [Header("Rotation")]
    float angle;
    public float rotationTarget;
    float r;
    public bool canRotate = true;
    Rigidbody rb;
    [Header("Interaction")]
    bool canInteract;
    public bool interacting;
    public GameObject currentObject;
    bool interactCooldown;



    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        
        //Moves the player to the "Current Point" when first loading up the game. 
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

            //Checks if the raycast is touching an object with the tag "Stop".
            //If touching a stop, it allows the player to move.
            if (hit.collider.tag.Equals("Stop"))
            {
                Debug.Log("Hit Stop Point");
                nextPoint = hit.collider.gameObject;
                canMove = true;
            }

            //Checks if the raycast is touching an object with the tag "Interact".
            //If touching an interactable, It allows the player to interact, and sets the current object as the one the raycast is currently touching.
            //If NOT touching an interactable, The player is NOT allowed to interact, and the current object is set to null. 
            if (hit.collider.tag.Equals("Interact"))
            {
                Debug.Log("Hit Interactable");
                currentObject = hit.collider.gameObject;
                canInteract = true;
            }
            else 
            {
                canInteract = false;
                currentObject = null;
                interacting = false;
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

        //Keeps the player moving until they reach their next "Stop".
        if (currentPoint != null && gameObject.transform.position == currentPoint.transform.position) 
        {
            currentPoint = null;
            moving = false;
        }
        if (moving) 
        {
            
            Movement();
        }

        //Stops the Player moving when pressing Space.
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            moving = false;
        }

        if (Input.GetKeyDown(KeyCode.F) && canInteract && !interactCooldown) 
        {
            interacting = true;
            interactCooldown = true;
        }

        if (interactCooldown) 
        {
            Invoke("IntCooldown", 5F);
        }
        if (!interactCooldown) 
        {
            CancelInvoke("IntCooldown");
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

    public void IntCooldown() 
    {
        interactCooldown = false;
    }
}
