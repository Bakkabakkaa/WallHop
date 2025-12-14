using System;
using UnityEngine;
using UnityEngine.InputSystem;
using TouchPhase = UnityEngine.TouchPhase;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _jumpForce;
    [SerializeField] private int _maxJumpCount = 2;
    [SerializeField] private AudioSource _moveAudio;
    [SerializeField] private AudioSource _landingAudio;

    private Rigidbody2D _rigidbody2D;
    private int _jumpCount;
    private EchoEffects _echoEffects;
    private bool _isGrounded;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _echoEffects = GetComponent<EchoEffects>();
        
        _jumpCount = 0;
    }

    private void Update()
    {
        if (CanJump() && CheckJumpInput())
        {
            Jump();
            _echoEffects.CanShowEcho(true);
        }
    }

    private void Jump()
    {
        _moveAudio.Play();
        _rigidbody2D.linearVelocity = Vector2.up * (_jumpForce * _rigidbody2D.gravityScale);
        _jumpCount--;
    }

    private bool CheckJumpInput()
    {
        var isSpaceButton = Input.GetKeyDown(KeyCode.Space);
        var isTouchingInput = Input.touches.Length > 0
                              && Input.GetTouch(0).phase == TouchPhase.Began;

        return isSpaceButton || isTouchingInput;
    }

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.collider.CompareTag(GlobalConstants.FLOOR_TAG) && !_isGrounded)
        {
            _landingAudio.Play();
            _jumpCount = _maxJumpCount;
            _isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision2D)
    {
        _isGrounded = false;
    }

    private bool CanJump()
    {
        return _jumpCount > 0;
    }
}