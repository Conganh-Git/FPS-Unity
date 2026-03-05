using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputReader input;

    [SerializeField] private float moveSpeed = 5f; 

    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float mouseSensitivity = 0.08f;
    [SerializeField] private float pitchClamp = 85f;
    


    private CharacterController controller;

    private Vector2 moveInput; // create Place to save Value

    private Vector2 lookInput;
    private float pitch;


    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }
    //subcribe/un Event
    private void OnEnable()
    {
        if (input == null) return;
        input.MoveEvent += OnMove;
        input.LookEvent += OnLook;
        input.FireEvent += OnFire;

    }
    private void OnDisable()
    {
        if (input == null) return;
        input.MoveEvent -= OnMove;
        input.LookEvent -= OnLook;
        input.FireEvent -= OnFire;

    }


    private void OnMove(Vector2 v)
    {
        moveInput = v;
    }
    private void OnLook(Vector2 v)
    {
        lookInput = v;
    }    
    private void OnFire()
    {
        Debug.Log("Fire");
    }

    private void Update()
    {
        // MOVE
        // Vector3 move = new Vector3(moveInput.x, 0f, moveInput.y); // V2 -> V3 . y -> z
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y; // Move = Mouse
        controller.Move(move * moveSpeed * Time.deltaTime);

        float yaw = lookInput.x * mouseSensitivity; // Body look around
        transform.Rotate(Vector3.up * yaw);

        pitch -= lookInput.y * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, -15f, pitchClamp); 

        if (cameraTransform != null) 
        cameraTransform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }
}
