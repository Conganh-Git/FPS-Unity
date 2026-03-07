using UnityEngine; // use MonoBehaviour, Debug,...
using System; // use Action
using UnityEngine.InputSystem;
using UnityEngine.XR;


public class InputReader : MonoBehaviour
{
    public event Action<Vector2> MoveEvent; // create event name send ADSW
    public event Action<bool> SprintEvent;

    public event Action<Vector2> LookEvent; // who want hear me about move or look come here.

    public event Action FireEvent;

    private PlayerInputActions actions; // creat from InputAction

    private void Awake()
    {
        actions = new PlayerInputActions(); // create Input
    }
    // turn on Input
    private void OnEnable()
    {
        actions.Enable();
        //hear Input
        actions.PlayerM.Move.performed += CallMove;
        actions.PlayerM.Move.canceled += CallMove;

        actions.PlayerM.Sprint.performed += CallSprint;
        actions.PlayerM.Sprint.canceled += CallSprint;

        actions.PlayerM.Look.performed += CallLook;
        actions.PlayerM.Look.canceled += CallLook;

        actions.PlayerM.Fire.performed += CallFire;


    }
    
    private void OnDisable()
    {
        actions.PlayerM.Move.performed -= CallMove;
        actions.PlayerM.Move.canceled -= CallMove;

        actions.PlayerM.Sprint.performed -= CallSprint;
        actions.PlayerM.Sprint.canceled -= CallSprint;

        actions.PlayerM.Look.performed -= CallLook;
        actions.PlayerM.Look.canceled -= CallLook;

        actions.PlayerM.Fire.performed -= CallFire;

        actions.Disable();
    }
    // send Signal
    private void CallMove(InputAction.CallbackContext call) // Value V2. When Press. Status click or not
    {
        MoveEvent?.Invoke(call.ReadValue<Vector2>());       // ?  = null-safe
    }

    private void CallSprint(InputAction.CallbackContext call)
    {
        SprintEvent?.Invoke(call.ReadValueAsButton());
    }    
    private void CallLook(InputAction.CallbackContext call) // call = take Value of Callback and read it under V2
    {
        LookEvent?.Invoke(call.ReadValue<Vector2>());       // Invoke = call all people want hear
    }

    private void CallFire(InputAction.CallbackContext call)
    {
        FireEvent?.Invoke();
    } 


}
