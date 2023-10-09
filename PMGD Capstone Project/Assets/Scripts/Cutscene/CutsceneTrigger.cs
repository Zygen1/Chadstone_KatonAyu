using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneTrigger : MonoBehaviour
{
    public GameObject cutscene;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
           cutscene.SetActive(true);
           Destroy(gameObject);
        }
    }

    public void StartCutscene()
    {
        cutscene.SetActive(true);
        Destroy(gameObject);
    }
}
