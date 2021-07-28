using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GranadeThrower : MonoBehaviour
{
    public float throwForce = 40f;
    public GameObject grenadePrefab;
    private AudioSource boom;
    private DateTime timeLastThrown;
    private int bombDelayInSeconds = 10;

    void Start()
    {
        boom = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (AllowGrenade())
            {
            ThrowGrenade();
            boom.Play();
            }
            else
            {
               // Debug.Log("NoBomb");
            }
           
        }
    }

    void ThrowGrenade()
    {
       GameObject grenade = Instantiate(grenadePrefab, transform.position, transform.rotation);
       Rigidbody rb = grenade.GetComponent<Rigidbody>();
       rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
    }

    bool AllowGrenade()
    {
        DateTime bombtime = DateTime.Now;
        TimeSpan ts = bombtime - timeLastThrown; 
        if (ts.TotalSeconds > bombDelayInSeconds)
        {
            timeLastThrown = bombtime;
            return true;
        } 
        Debug.Log(ts.TotalSeconds.ToString());
        return false;
    }
}
