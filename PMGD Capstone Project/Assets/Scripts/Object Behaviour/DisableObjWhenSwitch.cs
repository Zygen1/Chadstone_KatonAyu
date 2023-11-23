using UnityEngine;


public class DisableObjWhenSwitch : MonoBehaviour
{
    [SerializeField] GameObject[] objToDisable;
    SwitchObject switchObject;

    // Start is called before the first frame update
    void Start()
    {
        switchObject = GetComponent<SwitchObject>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < objToDisable.Length; i++)
        {
            objToDisable[i].SetActive(!switchObject.isOn);
        }
    }
}
