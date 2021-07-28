using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLock : MonoBehaviour
{

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
     if(Input.GetKeyDown("0"))
     {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
     }
        
    }
}
