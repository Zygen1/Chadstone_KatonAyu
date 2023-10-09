using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager instance;

    public GameObject[] equipmentList;
    public GameObject currentActiveEquipment;
    public bool isCurrentlyEquip;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < equipmentList.Length; i++)
        {
            if (equipmentList[i].activeSelf)
            {
                currentActiveEquipment = equipmentList[i];
                return;
            }
            else
            {
                currentActiveEquipment = null;
            }
        }
    }

    public void SetEquipment(string equipmentName, bool isEquip)
    {
        for(int i = 0; i < equipmentList.Length; i++)
        {
            EquipmentObject equipmentObject = equipmentList[i].GetComponent<EquipmentObject>();
            if(equipmentObject.equipmentObjectName == equipmentName)
            {
                equipmentList[i].SetActive(isEquip);
            }
        }

        Debug.Log("Set Equipment");
    }
}
