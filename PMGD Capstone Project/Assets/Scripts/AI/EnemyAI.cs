using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class EnemyAI : MonoBehaviour
{

    public Transform target;

    public float speed;
    public float nextWayPointDistance = 3f;
    private Vector2 direction;

    Path path;
    int currentWayPoint = 0;
    bool reachedEndOfPath = false; 

    Seeker seeker;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, .2f);
        
    }

    void UpdatePath() 
    {
        if (seeker.IsDone()) 
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error) 
        {
            path = p;
            currentWayPoint = 0;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (path == null)
        {
            return;
        }
        else
        if (currentWayPoint >= path.vectorPath.Count) 
        {
            reachedEndOfPath = true;
            return;
        } else 
        {
            reachedEndOfPath = false;
            
        }

        direction = ((Vector2)path.vectorPath[currentWayPoint] - rb.position).normalized;

        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);
        

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);

        if(distance < nextWayPointDistance) 
        {
            currentWayPoint++;
        }

    }

    public Vector2 GetDirection() 
    {
        return direction;
    }

}
