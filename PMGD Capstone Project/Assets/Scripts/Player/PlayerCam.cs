using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float smoothSpeed = 5.0f;

    [SerializeField] private Vector3 offset;
    [SerializeField] private bool offsetIsSet;

    // Update is called once per frame
    void LateUpdate()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        if(player != null)
        {
            if(offsetIsSet == false)
            {
                offset = transform.position - player.position;
                offsetIsSet = true;
            }

            Vector3 targetPosition = player.position + new Vector3(0, 0, offset.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.fixedDeltaTime);
        }
        else
        {
            offsetIsSet = false;
        }
    }
}
