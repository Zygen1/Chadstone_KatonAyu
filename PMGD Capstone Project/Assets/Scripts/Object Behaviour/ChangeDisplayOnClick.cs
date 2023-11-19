using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDisplayOnClick : MonoBehaviour
{
    [SerializeField] GameObject[] objToSetInactive;
    [SerializeField] GameObject[] objToSetActive;


    public void OnMouseDown()
    {
        SetObject();
    }

    void SetObject()
    {
        for(int i = 0; i < objToSetInactive.Length; i++)
        {
            objToSetInactive[i].SetActive(false);
        }

        for(int j = 0; j < objToSetActive.Length; j++)
        {
            objToSetActive[j].SetActive(true);
        }
    }
}
