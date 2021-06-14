using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    [SerializeField] private float _xSensitivity = 1000f;
    [SerializeField] private float _movementSpeed = 10f;

    private Rigidbody _rigidBody;
    private Animator _animator;
    private float _mouseMovement = 0f;

    void Awake() {
        _rigidBody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    void Update() {
        _mouseMovement += Input.GetAxis("Mouse X");
    }

    void FixedUpdate() {

        transform.Rotate(0, _mouseMovement * Time.fixedDeltaTime * _xSensitivity, 0);
        _mouseMovement = 0f;

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        if (Input.GetKey(KeyCode.LeftShift)) {
            verticalInput *= 2.0f;
        }

        Vector3 targetVelocity = new Vector3(horizontalInput, 0, verticalInput).normalized * _movementSpeed * Time.fixedDeltaTime;
        Vector3 offset = transform.rotation * targetVelocity;

        _animator.SetFloat("Horizontal", horizontalInput, 0.1f, Time.fixedDeltaTime);
        _animator.SetFloat("Vertical", verticalInput, 0.1f, Time.fixedDeltaTime);
        _rigidBody.MovePosition(transform.position + offset);
    }
}