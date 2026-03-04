using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputReader input;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float moveSpeed = 5f;


    private CharacterController controller;

    private Vector2 moveInput; // create Place to save Value
    private Vector2 lookInput;
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
    }
}
