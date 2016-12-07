using UnityEngine;
using System.Collections;

public class ScrollingBackground : MonoBehaviour
{
    public float scrollSpeed = 0.05F; // speed 

    void Start()
    {
    }

    void Update()
    {
            //smooth
            float offset = Time.time * scrollSpeed;

            //texture offset
            GetComponent<Renderer>().material.mainTextureOffset = new Vector2(0, -offset);
        
    }
}