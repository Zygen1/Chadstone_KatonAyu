using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetContinueButton : MonoBehaviour
{
    [SerializeField] GameObject continueBtn;

    private void Update()
    {
        continueBtn.SetActive(DataManager.instance.isRoom1Unlock);
    }
}
