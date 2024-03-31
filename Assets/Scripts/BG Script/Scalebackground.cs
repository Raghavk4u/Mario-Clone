using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scalebackground : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        transform.localScale = Vector3.one;

        float width = sr.sprite.bounds.size.x;           //width and height of the background image
        float height = sr.sprite.bounds.size.y;

        float worldHeight = Camera.main.orthographicSize * 2f;
        float worldWidth = worldHeight/ Screen.width * Screen.height;

        Vector3 temp = transform.localScale;
        temp.x = worldWidth / width + 0.7f;
        temp.y = worldHeight / height + 0.5f;
        transform.localScale = temp;


        
    }


    
}
