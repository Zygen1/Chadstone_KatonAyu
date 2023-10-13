using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialsNumber : MonoBehaviour
{
    private bool coroutineAllowed;

    private int dialsValue;

    // Start is called before the first frame update
    void Start()
    {
        coroutineAllowed = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (coroutineAllowed)
        {
            StartCoroutine("NextNumber");
        }
    }

    private IEnumerator NextNumber()
    {
        coroutineAllowed = false;

        for (int i = 0; i < 10; i++)
        {

            yield return new WaitForSeconds(0.01f);
        }

        coroutineAllowed = true;

        dialsValue += 1;

        if (dialsValue > 9)
        {
            dialsValue = 0;
        }
    }
}
