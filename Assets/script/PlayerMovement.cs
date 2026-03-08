using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputReader input;

    [Header("Movement")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintSpeed = 8f;

    private CharacterController controller;

    private Vector2 moveInput;
    private bool isSprinting;

    public Vector2 MoveInput => moveInput;
    public bool IsSprinting => isSprinting;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        input.MoveEvent += OnMove;
        input.SprintEvent += OnSprint;
    }

    private void OnDisable()
    {
        input.MoveEvent -= OnMove;
        input.SprintEvent -= OnSprint;
    }

    private void OnMove(Vector2 value)
    {
        moveInput = value;
    }

    private void OnSprint(bool value)
    {
        isSprinting = value;
    }

    private void Update()
    {
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;

        float speed = isSprinting && moveInput.y > 0 ? sprintSpeed : walkSpeed;

        controller.Move(move * speed * Time.deltaTime);
    }
}