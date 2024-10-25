using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DoorInteractFail : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public Text playerTextBox;
    public float delay = 0.1f;
    float startDelay;
    public float DownTimer = 1;
    string textBoxText = "";
    public string doorOpenFailText;
    public Animator playerTextBoxAnimations;
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
        }

    }

    IEnumerator ShowText()
    {
        delay = 1;
        playerMovement.canMove = false;
        playerMovement.canRotate = false;
        typing = true;
        stringLength = doorOpenFailText.Length;
        for (int i = 0; i < doorOpenFailText.Length + 1; i++)
        {
            textBoxText = doorOpenFailText.Substring(0, i);
            playerTextBox.text = textBoxText;
            yield return new WaitForSeconds(delay);
            delay = startDelay;
            if (i == stringLength)
            {
                playerTextBoxAnimations.SetBool("Drop Down", true);
                typing = false;

            }
        }
        playerMovement.canMove = true;
        playerMovement.canRotate = true;
    }
}
