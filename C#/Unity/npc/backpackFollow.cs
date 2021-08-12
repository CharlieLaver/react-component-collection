using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backpackFollow : MonoBehaviour
{
    private Transform player;
    private float speed = 50;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) > 20)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }
}
