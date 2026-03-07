using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputReader input;

    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintSpeed = 8f;
    [SerializeField] private float moveSpeed;

    [SerializeField] private Transform cameraTransform; // Drag PlayerCamera and drop
    [SerializeField] private float mouseSensitivity = 0.08f;
    [SerializeField] private float pitchClamp = 85f;

    [SerializeField] private GameObject extraCross;
    [SerializeField] private AudioSource gunFire;
    [SerializeField] private AudioSource emtyGunSound;
    [SerializeField] private GameObject handGun;
    [SerializeField] bool canFire = true;

    public Vector2 MoveInput => moveInput;
    public bool IsSprinting => isSprinting;


    private CharacterController controller;

    private Vector2 moveInput; // create Place to save Value
    private bool isSprinting;

   

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
        input.SprintEvent += OnSprint;
        input.LookEvent += OnLook;
        input.FireEvent += OnFire;

    }
    private void OnDisable()
    {
        if (input == null) return;
        input.MoveEvent -= OnMove;
        input.SprintEvent -= OnSprint;
        input.LookEvent -= OnLook;
        input.FireEvent -= OnFire;

    }


    private void OnMove(Vector2 v)
    {
        moveInput = v;
    }
    private void OnSprint(bool value)
    {
        isSprinting = value;
    }    
    private void OnLook(Vector2 v)
    {
        lookInput = v;
    }    
    private void OnFire()
    {
        if(canFire == true)
        {
            if (GlobalAmmo.handgunAmmoCount == 0)
            {
                canFire = false;
                StartCoroutine(EmptyGun());

            }
            else
            {
                canFire = false;
                StartCoroutine(FiringGun());
            }
                
        }
    }

    private void Update()
    {
        // MOVE

        // Vector3 move = new Vector3(moveInput.x, 0f, moveInput.y); // V2 -> V3 . y -> z
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y; // Move = Mouse
        moveSpeed = isSprinting && moveInput.y > 0 ? sprintSpeed : walkSpeed;
        controller.Move(move * moveSpeed * Time.deltaTime);

        // Mouse Look 

        float yaw = lookInput.x * mouseSensitivity; // Body look around
        transform.Rotate(Vector3.up * yaw);

        pitch -= lookInput.y * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, -15f, pitchClamp); 

        if (cameraTransform != null) 
        cameraTransform.localRotation = Quaternion.Euler(pitch, 0f, 0f);

        
    }

    IEnumerator FiringGun()
    {
        gunFire.Play();
        extraCross.SetActive(true);
        GlobalAmmo.handgunAmmoCount -= 1;
        handGun.GetComponent<Animator>().Play("HandgunFire");
        yield return new WaitForSeconds(0.5f);
        handGun.GetComponent<Animator>().Play("gunfire");
        extraCross.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        canFire = true;
    }

    IEnumerator EmptyGun()
    {
        emtyGunSound.Play();
        yield return new WaitForSeconds(0.6f);
        canFire = true;
    }
}
