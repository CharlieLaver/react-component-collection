using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDie : MonoBehaviour
{
    public float health = 50f;
    
    public bool drops;
    public Transform dropPoint;
    public GameObject dropObject; 
    
   
    public void TakeDamage (float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
           Die();
        }
    }
    public void Die ()
    {
        Destroy(this.gameObject);
         if (drops) Instantiate(dropObject, dropPoint.position, dropPoint.rotation);
    }
}
