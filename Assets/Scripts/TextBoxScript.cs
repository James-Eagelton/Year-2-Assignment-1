using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBoxScript : MonoBehaviour
{
    public Animator textBoxAnimation;
    public DoorInteract doorInteract;
    

    public void PopUpEnd() 
    {
        textBoxAnimation.SetBool("Float Up", false);
    }

    public void DropDownEnd() 
    {
        textBoxAnimation.SetBool("Drop Down", false);
        doorInteract.typing = false;
    }
}
