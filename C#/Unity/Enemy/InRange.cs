using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class InRange : MonoBehaviour
{
    private Transform player;
    private float dist;
    public float moveSpeed;
    public float howclose;

    public Animator animator;
    GameObject target;
    NavMeshAgent agent;
    [SerializeField] int damage; 
    [SerializeField] float stoppingDistance;

    public AudioSource run;
    public AudioSource attack;
    private DateTime lastSound;
    private int soundDelayInSeconds = 100;
    private DateTime lastAttackSound;
    private int soundAttackDelayInSeconds = 100;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        dist = Vector3.Distance(player.position, transform.position);

        PlayerInRange();

    }

    public void PlayerInRange()
    {
      if(dist <= howclose)
        {
            transform.LookAt(player);
            GetComponent<Rigidbody>().AddForce(transform.forward * moveSpeed);
             animator.SetBool("Run", true);
             animator.SetBool("Idle", false);
             if (AllowSound())
             {
                 run.Play();
             }
        if (dist < stoppingDistance)
        {
            StopEnemy();
            animator.SetBool("Run", false);
            animator.SetBool("Attack", true);
            target.GetComponent<PlayerHealth>().TakeDamage(damage);
            
        }
        else
        {
            GoToTarget();
            animator.SetBool("Run", true);
            animator.SetBool("Attack", false);
        }
        }
        else
        {
            animator.SetBool("Idle", true);
            animator.SetBool("Run", false);
        }
        
    }

    private void GoToTarget()
    {
        agent.isStopped = false;
    }

    public void StopEnemy()
    {
        agent.isStopped = true;
        if (AllowDieSound())
        {
            attack.Play();
        }             
            
    }

    bool AllowSound()
    {
        DateTime soundtime = DateTime.Now;
        TimeSpan ts = soundtime - lastSound; 
        if (ts.TotalSeconds > soundDelayInSeconds)
        {
            lastSound = soundtime;
            return true;
            
        
        } 
        return false;
        
    }

    bool AllowDieSound()
    {
        DateTime soundAttackTime = DateTime.Now;
        TimeSpan ts = soundAttackTime - lastAttackSound; 
        if (ts.TotalSeconds > soundAttackDelayInSeconds)
        {
            lastAttackSound = soundAttackTime;
            return true;
            
        
        } 
        return false;
    }
}
