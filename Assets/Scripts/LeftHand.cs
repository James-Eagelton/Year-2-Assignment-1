using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHand : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject leftHand;
    
    
    
    
    
    void Start()
    {
        Instantiate(leftHand, gameObject.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
