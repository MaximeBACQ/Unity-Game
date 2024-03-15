using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InputController : MonoBehaviour
{
    //Singleton pattern
    [SerializeField] private InputActionAsset inputs;

    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction jumpAction;
    private InputAction sprintAction;
    private InputAction interactAction;

    public Vector2 moveInput { get; private set; }
    public Vector2 lookInput { get; private set; }
    public bool JumpTriggered { get; private set; }
    public float SprintValue { get; private set; }
    public bool InteractionTriggered { get; private set; }

    public static InputController Instance { get; private set; }


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // don't destroy this object if it already exists
        }
        else
        {
            Destroy(gameObject);
        }

        moveAction = inputs.FindActionMap("Player").FindAction("Movement");
        lookAction = inputs.FindActionMap("Player").FindAction("Look");
        jumpAction = inputs.FindActionMap("Player").FindAction("Jump");
        sprintAction = inputs.FindActionMap("Player").FindAction("Sprint");
        interactAction = inputs.FindActionMap("Player").FindAction("Interaction");
        RegisterInputActions();

        CheckDeviceList();
    }

    public void RemoveFromDontDestroyOnLoad()
    {
        SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());
    }

    //useful for later
    void CheckDeviceList()
    {
        foreach(var device in InputSystem.devices)
        {
            if (device.enabled)
            {
                Debug.Log("Device :" + device.name);
            }
        }
    }
    void RegisterInputActions()
    {
        moveAction.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        moveAction.canceled += ctx => moveInput = Vector2.zero;

        lookAction.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        lookAction.canceled += ctx => lookInput = Vector2.zero;

        jumpAction.performed += ctx => JumpTriggered = true;
        jumpAction.canceled += ctx => JumpTriggered = false;

        sprintAction.performed += ctx => SprintValue = ctx.ReadValue<float>();
        sprintAction.canceled += ctx => SprintValue = 0f;

        interactAction.performed += ctx => InteractionTriggered = true;
        interactAction.canceled += ctx => InteractionTriggered = false;
    } 

    private void OnEnable()
    {
        moveAction.Enable();
        lookAction.Enable(); 
        jumpAction.Enable();
        sprintAction.Enable();
        interactAction.Enable();

        InputSystem.onDeviceChange += OnDeviceChange;
    }

    //useful for later
    private void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        switch (change)
        {
            case InputDeviceChange.Disconnected:
                Debug.Log("Device :" + device.name + " disconnected");
                //HANDLE DISCONNECT
                break;
            case InputDeviceChange.Reconnected:
                Debug.Log("Device :" + device.name + " reconnected");
                break;
        }
    }

    private void OnDisable()
    {
        moveAction.Disable();
        lookAction.Disable();
        jumpAction.Disable();
        sprintAction.Disable();
        interactAction.Disable();

        // don't need that event anymore, unsubscribe
        InputSystem.onDeviceChange -= OnDeviceChange;
    }
}
