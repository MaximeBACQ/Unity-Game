    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class FPSController : MonoBehaviour
    {
        public GameObject door;
        private DoorController doorController;
        public GameObject key;

        [SerializeField] public float walkSpeed = 3.0f;
        [SerializeField] private float sprintMultiplier = 2.0f;

        [SerializeField] private float jumpForce = 5.0f;
        [SerializeField] private float gravity = 5f;

        [SerializeField] private float mouseSensitivity = 2.0f;
        [SerializeField] private float upDownRange = 80.0f; //45 up and down

        private CharacterController characterController;
        private Camera mainCamera;
        private InputController inputController;

        private Vector3 currentMovement;
        private float verticalRotation;

        private bool hasKey = false;
        //private DoorController doorController;

        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;
            characterController = GetComponent<CharacterController>();
            mainCamera = Camera.main;
        }

        private void Start()
        {
            inputController = InputController.Instance;
            /*GameObject doorObject = GameObject.FindGameObjectWithTag("Door");
            if (doorObject != null)
            {
                doorController = doorObject.GetComponent<DoorController>();
            }*/
            doorController = door.GetComponent<DoorController>();

    }

    private void Update()
        {
            HandleMovement();
            HandleRotation();
            HandleInteraction();
        }

    void HandleInteraction()
    {
        if (inputController.InteractionTriggered)
        {
            float maxDistance = 100f; // Maximum distance for raycast
            RaycastHit hit; // Stores information about the object hit by the raycast

            // Ray from the center of the screen
            Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

            // Visualize the raycast in the scene view
            Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.red, 2f); // Last parameter is duration

            // Perform the raycast
            if (Physics.Raycast(ray, out hit, maxDistance))
            {
                // Check if the object hit has a specific component or tag that makes it pickable
                // For example, if pickable objects have a tag named "Pickable"
                if (hit.collider.CompareTag("Pickable"))
                {
                    hasKey = true;
                    Debug.Log("Hit a pickable object: " + hit.collider.name);
                    key.SetActive(false);
                    // Here, you can add logic to "pick up" the object, such as disabling it, attaching it to the player, etc.
                }
                if(hit.collider.CompareTag("Door") && hasKey)
                {
                    Debug.Log("open door bc has key");
                    doorController.UseKey();
                    hasKey = false;

                }
            }
        }
    }


    void HandleMovement()
        {
            //if sprint>0, the default sprintMultiplier of 2.0 is used, if sprintvalue is not superior to 0, we are just walking
            float speed = walkSpeed * (inputController.SprintValue>0 ? sprintMultiplier:1f);

            Vector3 inputDirection = new Vector3(inputController.moveInput.x, 0f, inputController.moveInput.y);
            Vector3 worldDirection = transform.TransformDirection(inputDirection); // transform from local to global space

            worldDirection.Normalize(); // make sure we never go above the speed allowed 

            currentMovement.x = worldDirection.x * speed;
            currentMovement.z = worldDirection.z * speed;

            //Handle Jumping
            HandleJumping();

            characterController.Move(currentMovement * Time.deltaTime);
        }

        void HandleJumping()
        {
            if (characterController.isGrounded)
            {
                currentMovement.y = -0.5f;
                if (inputController.JumpTriggered)
                {
                    currentMovement.y = jumpForce;
                }
            }
            else
            {
                currentMovement.y -= gravity * Time.deltaTime;
            }
        }
        void HandleRotation()
        {
            float mouseXRotation = inputController.lookInput.x * mouseSensitivity;
            transform.Rotate(0, mouseXRotation, 0);

            verticalRotation -= inputController.lookInput.y * mouseSensitivity;
            verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);
            mainCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
        }
    }
