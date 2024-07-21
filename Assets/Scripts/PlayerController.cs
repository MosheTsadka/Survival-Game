using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private MobileJoystick playerJoystick;
    [SerializeField] private float moveSpeed;
    private Rigidbody2D _rb;
    

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        _rb.velocity = playerJoystick.GetMoveVector() * (moveSpeed * Time.deltaTime);
    }
}
