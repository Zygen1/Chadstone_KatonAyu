using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AICustom : MonoBehaviour
{
    [SerializeField] string targetTag;

    [Header("Debug")]
    [SerializeField] AIDestinationSetter AIDestinationSetter;

    // Start is called before the first frame update
    void Start()
    {
        AIDestinationSetter = GetComponent<AIDestinationSetter>();
    }

    // Update is called once per frame
    void Update()
    {
        AIDestinationSetter.target = GameObject.FindGameObjectWithTag(targetTag).transform;
        /*RaycastHit2D hit;
        if(Physics2D.Raycast(transform.position, ))
        {

        }*/
    }
}
