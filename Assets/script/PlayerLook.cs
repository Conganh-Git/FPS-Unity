using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private InputReader input;
    [SerializeField] private Transform cameraTransform;

    [SerializeField] private float mouseSensitivity = 0.08f;
    [SerializeField] private float pitchClamp = 85f;

    private Vector2 lookInput;
    private float pitch;

    //Lock Mouse when play.
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void OnEnable()
    {
        input.LookEvent += OnLook;
    }

    private void OnDisable()
    {
        input.LookEvent -= OnLook;
    }

    private void OnLook(Vector2 value)
    {
        lookInput = value;
    }

    void Update()
    {
        float yaw = lookInput.x * mouseSensitivity;
        transform.Rotate(Vector3.up * yaw);

        pitch -= lookInput.y * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, -15f, pitchClamp);

        cameraTransform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }
}