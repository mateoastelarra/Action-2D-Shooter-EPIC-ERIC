using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteScroller : MonoBehaviour
{
    [SerializeField] Vector2 ScrollSpeed;
    Vector2 offset;
    Material material;
    void Awake() 
    {
        material = GetComponent<SpriteRenderer>().material;
    }

    void Update()
    {
        offset = ScrollSpeed * Time.deltaTime;
        material.mainTextureOffset += offset;
    }
}
