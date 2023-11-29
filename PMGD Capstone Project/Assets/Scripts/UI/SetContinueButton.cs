using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetContinueButton : MonoBehaviour
{
    [SerializeField] GameObject continueBtn;

    // Start is called before the first frame update
    /*IEnumerator Start()
    {
        yield return new WaitForSeconds(1f);
        continueBtn.SetActive(DataManager.instance.isRoom1Unlock);
    }*/

    private void Update()
    {
        continueBtn.SetActive(DataManager.instance.isRoom1Unlock);
    }
}
