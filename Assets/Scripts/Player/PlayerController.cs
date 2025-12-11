using System;
using UnityEngine;
using UnityEngine.InputSystem;
using TouchPhase = UnityEngine.TouchPhase;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _jumpForce;

    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (CheckJumpInput())
        {
            Jump();
        }
    }

    private void Jump()
    {
        _rigidbody2D.linearVelocity = Vector2.up * (_jumpForce * _rigidbody2D.gravityScale);
    }

    private bool CheckJumpInput()
    {
        var isSpaceButton = Input.GetKeyDown(KeyCode.Space);
        var isTouchingInput = Input.touches.Length > 0
                              && Input.GetTouch(0).phase == TouchPhase.Began;

        return isSpaceButton || isTouchingInput;
    }
}
