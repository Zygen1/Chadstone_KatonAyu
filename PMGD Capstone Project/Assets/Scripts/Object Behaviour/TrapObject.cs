using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TrapDamageType { FREEZE, HEALTH }
public class TrapObject : MonoBehaviour
{
    public TrapDamageType type;
    public float healthDamage;
    public float freezeDamage;

    [SerializeField] bool isActive;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            if (type == TrapDamageType.FREEZE)
            {
                PlayerStats.instance.currentFreezing += (freezeDamage + PlayerStats.instance.freezingRecovery) * Time.deltaTime;
                if (PlayerStats.instance.currentFreezing >= PlayerStats.instance.maxFreezing)
                {
                    PlayerStats.instance.currentHealth -= healthDamage * Time.deltaTime;
                }
            }
            else if (type == TrapDamageType.HEALTH)
            {
                PlayerStats.instance.currentHealth -= healthDamage * Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isActive = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isActive = false;
        }
    }

}
