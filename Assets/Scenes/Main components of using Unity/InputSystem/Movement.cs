using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float moveSpeed = 5f;
    float _horizontalInput;
    public void OnMove(InputAction.CallbackContext context)
    {
        _horizontalInput = context.ReadValue<float>();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(_horizontalInput * moveSpeed, rb.linearVelocity.y);
    }
}