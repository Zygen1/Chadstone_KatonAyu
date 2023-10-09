using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GLOBAL_Test : MonoBehaviour
{
    public void RunFunction()
    {
        Debug.Log("Function is Run!");
    }

    public void TestAddHealth(float value)
    {
        PlayerStats.instance.currentHealth += value;
    }
}
