using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private MobileJoystick playerJoystick;
    private Rigidbody2D _rb;
    
    [Header("Settings")]
    [SerializeField] private float moveSpeed;
    

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        
    }

    private void FixedUpdate()
    {
        _rb.velocity = playerJoystick.GetMoveVector() * (moveSpeed * Time.deltaTime);
    }
}
