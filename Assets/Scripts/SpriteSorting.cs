using System;
using UnityEngine;

public class SpriteSorting : MonoBehaviour
{
    [Header("Elements")] 
    private SpriteRenderer _sr;


    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        _sr.sortingOrder = (int)(transform.position.y * 10);
    }
}
