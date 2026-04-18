using UnityEngine;
using UnityEngine.InputSystem;

public class Jogador : MonoBehaviour
{
    private Rigidbody rb;
    private PlayerInput playerInput;
    private Vector2 movimento;
    private float velocidade = 5f;
    private bool jumped = true;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        
    }

    void FixedUpdate() {
        rb.MovePosition(transform.position + new Vector3(movimento.x, 0, movimento.y) * velocidade * Time.fixedDeltaTime);
    }

    public void OnMove(InputAction.CallbackContext context) {    
        movimento = context.ReadValue<Vector2>();
        rb.linearVelocity = new Vector3(movimento.x, 0, movimento.y) * velocidade * Time.fixedDeltaTime;
    }

    public void Jump(InputAction.CallbackContext context) {
        if (context.performed && jumped) {
            rb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
            jumped = false;
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            jumped = true;
        }
    }

}
