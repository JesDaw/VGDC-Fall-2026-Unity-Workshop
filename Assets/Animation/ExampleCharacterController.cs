using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ExampleCharacterController : MonoBehaviour
{
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    private Animator _animator;
    private PlayerInput _playerInput;
    private InputAction _run, _attack, _jump;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;
    
    private float _currentVelocity;
    private bool _isGrounded = true, _inLag = false, _jumpSquat = false, _attacking = false;
    
    [Header("Movement")]
    [SerializeField] private float _maxSpeed = 6f;
    [SerializeField] private float _groundAcceleration = 80f;
    [SerializeField] private float _groundDeceleration = 120f;
    [SerializeField] private float _airAcceleration = 20f;
    
    [Header("Jumping")]
    [SerializeField] private float _shortHopForce = 6f;
    [SerializeField] private float _jumpForce = 8f;
    [SerializeField] private float _jumpSquatDuration = 0.1f;
    [SerializeField] private float _fallMultiplier = 2f;
    // [SerializeField] private float _lowJumpMultiplier = 1.6f;
    [SerializeField] private float _landingDuration = 0.1f;

    [SerializeField] private AnimationClip attackClip;

    private float _horizontalInput;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerInput = GetComponent<PlayerInput>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        InputActionMap map = _playerInput.currentActionMap;
        map.Enable();
        _run = map.FindAction("Run");
        _attack = map.FindAction("Attack");
        _jump = map.FindAction("Jump");
        
        _run.performed += context => OnRun(context.ReadValue<float>());
        _run.canceled += context => OnStopRun();
        _jump.started += context => OnJump();
        _attack.performed += context => OnAttack();

        PauseStateController.Instance.OnPause += () => map.Disable();
        PauseStateController.Instance.OnPause += () => map.Enable();
    }

    void Update()
    {
        if (_isGrounded && _run.inProgress && !_inLag)
        {
            _spriteRenderer.flipX = _horizontalInput < 0;
        }
    }

    private void LateUpdate()
    {
        if (!_isGrounded)
        {
            CheckLanding();
        }
    }

    private void FixedUpdate()
    {
        float targetSpeed = _horizontalInput * _maxSpeed;
        float currentAccel = 0f;
        if (_isGrounded)
        {
            currentAccel = Mathf.Abs(_horizontalInput) > 0.01f ? _groundAcceleration : _groundDeceleration;
        }
        else
        {
            currentAccel = _airAcceleration;
        }

        float newX = Mathf.MoveTowards(_rigidbody.linearVelocity.x, targetSpeed, currentAccel * Time.fixedDeltaTime);
        _rigidbody.linearVelocity = new Vector2(newX, _rigidbody.linearVelocity.y);
        
        if (_rigidbody.linearVelocity.y < 0 && !_jump.inProgress) 
        {
            _rigidbody.linearVelocity += Vector2.up * (Physics2D.gravity.y * (_fallMultiplier - 1) * Time.fixedDeltaTime);
        }

    }

    private void OnRun(float value)
    {
        _animator.SetBool("LRInput", true);
        if (!_attacking)
        {
            _horizontalInput = value;
        }
    }

    private void OnStopRun()
    {
        _animator.SetBool("LRInput", false);
        _horizontalInput = 0;
    }

    private void OnJump()
    {
        if (_isGrounded && !_inLag)
        {
            StartCoroutine(JumpCoroutine());
        }
    }

    private IEnumerator JumpCoroutine()
    {
        _horizontalInput = 0;
        _inLag = true;
        _jumpSquat = true;
        _animator.SetTrigger("JumpSquatTrigger");
        yield return new WaitForSeconds(_jumpSquatDuration);
        _jumpSquat = false;
        _animator.SetTrigger("JumpTrigger");
        _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, _jump.inProgress ? _jumpForce : _shortHopForce);
        _horizontalInput = _run.ReadValue<float>();
        _isGrounded = false;
        while (!_isGrounded)
        {
            if (_rigidbody.linearVelocity.y < 0)
            {
                _animator.SetTrigger("FallTrigger");
                yield break;
            }
            yield return null;
        }
    }

    private void CheckLanding()
    {
        bool checkGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundMask) && _rigidbody.linearVelocity.y <= 0;
        if (checkGrounded)
        {
            StartCoroutine(LandingCoroutine());
        }
    }

    private IEnumerator LandingCoroutine()
    {
        _isGrounded = true;
        _inLag = true;
        _animator.SetBool("LandingLag", true);
        _horizontalInput = 0;
        yield return new WaitForSeconds(_landingDuration);
        _inLag = false;
        _animator.SetBool("LandingLag", false);
        _horizontalInput = _run.ReadValue<float>();
    }

    private void OnAttack()
    {
        if (_isGrounded && !_inLag)
        {
            StartCoroutine(AttackCoroutine());
        }
    }

    private IEnumerator AttackCoroutine()
    {
        _attacking = true;
        _inLag = true;
        _animator.SetTrigger("AttackTrigger");
        _horizontalInput = 0;
        yield return new WaitForSeconds(attackClip.length / (_animator.speed * _animator.GetCurrentAnimatorStateInfo(0).speed));
        _inLag = false;
        _attacking = false;
        _horizontalInput = _run.ReadValue<float>();
    }
}
