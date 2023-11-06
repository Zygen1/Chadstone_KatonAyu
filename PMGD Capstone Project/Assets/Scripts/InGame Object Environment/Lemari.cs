using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lemari : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite newSprite;
    public Sprite oldSprite;
    private bool isOff = false;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeSprite() 
    {
        isOff = !isOff;
        if (isOff)
        {
            spriteRenderer.sprite = newSprite;
        }
        else 
        {
            spriteRenderer.sprite = oldSprite;
        }
        
    }
}
