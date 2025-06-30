using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBackgroundImage : MonoBehaviour
{
    public SpriteRenderer bgSpriteRenderer;
    public Sprite[] bgSprites;
    private int sId;
    
    // Start is called before the first frame update
    void Start()
    {
        if (bgSprites.Length > 0)
        {
            sId = 0;
            bgSpriteRenderer.sprite = bgSprites[sId];
        }
        else
        {
            Debug.LogError("You did not set any background sprites for the photobooth");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeSprite()
    {
        sId++;
        if (sId == bgSprites.Length)
        {
            sId = 0;
        }
        bgSpriteRenderer.sprite = bgSprites[sId];
    }
}
