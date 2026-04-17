using UnityEngine;
using UnityEngine.InputSystem;

public class Jogador : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private float speed = 5f;

    private InputAction moveAction;
    private Vector2 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        InputSystem.actions.Disable();
        playerInput.currentActionMap?.Enable();

        moveAction = playerInput.actions?.FindAction("Move");

        
        moveAction.performed += OnMovePerformed;
        moveAction.canceled += OnMoveCanceled;
    }

    void FixedUpdate()
    {
        PlayerMovement();
    }

    void PlayerMovement()
    {
        // Atualiza velocidade do Rigidbody usando o input lido
        Vector3 velocity = new Vector3(moveInput.x, rb.linearVelocity.y, moveInput.y) * speed;
        rb.linearVelocity = velocity;
    }

    void OnMovePerformed(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    void OnMoveCanceled(InputAction.CallbackContext context)
    {
        moveInput = Vector2.zero;
    }

  
}
