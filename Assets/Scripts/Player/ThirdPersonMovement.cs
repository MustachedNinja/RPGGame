using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    [SerializeField] private float _xSensitivity = 1000f;
    [SerializeField] private float _movementSpeed = 10f;
    private Rigidbody _rigidBody;

    void Awake() {
        _rigidBody = GetComponent<Rigidbody>();
    }

    void Update() {
        float mouseMovement = Input.GetAxis("Mouse X");
        transform.Rotate(0, mouseMovement * Time.deltaTime * _xSensitivity, 0);
    }

    void FixedUpdate() {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 targetVelocity = new Vector3(horizontalInput, 0, verticalInput).normalized * _movementSpeed * Time.fixedDeltaTime;
        Vector3 offset = transform.rotation * targetVelocity;

        _rigidBody.MovePosition(transform.position + offset);
    }
}