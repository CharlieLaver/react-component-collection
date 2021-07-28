using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject pauseButton;

     void Update()   
    {
       IsPaused();
       
    }

    public void IsPaused()
    {
        if (Input.GetKeyDown (KeyCode.Return))
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                pauseButton.SetActive (true);
                 
                
            }
            else
            {
                Time.timeScale = 1;
                pauseButton.SetActive (false);
            }
        }
    }


}
