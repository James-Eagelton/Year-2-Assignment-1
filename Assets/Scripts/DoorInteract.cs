using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DoorInteract : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public Text playerTextBox;
    public float delay = 0.1f;
    float startDelay;
    public float DownTimer = 1;
    string textBoxText = "";
    public string doorOpenText;
    public Animator playerTextBoxAnimations;
    public Animator door;
    int stringLength;
    public bool typing = false;


    private void Start()
    {
        startDelay = delay;
    }

    void Update()
    {

        if (playerMovement.currentObject == this.gameObject && playerMovement.interacting && !typing) 
        {
            playerTextBoxAnimations.SetBool("Float Up", true);
            StartCoroutine(ShowText());
            playerMovement.interacting = false;
            door.SetBool("Open Door", true);
        }
    
    }

    IEnumerator ShowText() 
    {
        playerMovement.canMove = false;
        playerMovement.canRotate = false;
        delay = 1;
        typing = true;
        stringLength = doorOpenText.Length;
        for (int i = 0; i < doorOpenText.Length + 1; i++) 
        {
            textBoxText = doorOpenText.Substring(0, i);
            playerTextBox.text = textBoxText;
            yield return new WaitForSeconds(delay);
            delay = startDelay;
            if (i == stringLength) 
            {
                playerTextBoxAnimations.SetBool("Drop Down", true);

            }
        }

        playerMovement.canMove = true;
        playerMovement.canRotate = true;
    }



}
