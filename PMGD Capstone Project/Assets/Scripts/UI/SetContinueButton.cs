using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetContinueButton : MonoBehaviour
{
    [SerializeField] GameObject continueBtn;

    // Start is called before the first frame update
    void Start()
    {
        continueBtn.SetActive(DataManager.instance.isRoom1Unlock);
    }
}
