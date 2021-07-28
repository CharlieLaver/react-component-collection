using UnityEngine;
using System;

public class Target : MonoBehaviour
{
    public float health = 50f;
    public Animator animator;
    public AudioSource die;
    
    public bool drops;
    public Transform dropPoint;
    public GameObject dropObject; 

    private DateTime timeLastDie;
    private int dieDelayInSeconds = 10;
    
  
    public void TakeDamage (float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            if (AllowDie())
            {
                Die();
            }
            
           
            
        }
    }

    public void Die ()
    {
        animator.SetBool("Dying", true);  
        die.Play();
         if (drops) Instantiate(dropObject, dropPoint.position, dropPoint.rotation);
    }

     bool AllowDie()
    {
        DateTime dieTime = DateTime.Now;
        TimeSpan ts = dieTime - timeLastDie; 
        if (ts.TotalSeconds > dieDelayInSeconds)
        {
            timeLastDie = dieTime;
            return true;
        } 
        Debug.Log(ts.TotalSeconds.ToString());
        return false;
    }

}
