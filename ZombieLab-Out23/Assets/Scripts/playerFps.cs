using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]

public class playerFps : MonoBehaviour
{
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public Camera fpsCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;
    public bool hasKey;
    public GameObject key;
    public TextMeshPro nameText;
    //public Collider colPlayer;
    //public bool scriptEnable = true;//para que se quite el update

    CharacterController characterController;
    public Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    //[HideInInspector]
    public bool canMove = true;
    public bool blockMove =  false;

    public float curSpeedX;
    public float curSpeedY;

    public GameObject[] modelsAvailable;

    public RaycastManager raycastManager;

    private void Awake()
    {
        var selectedHeroe = PlayerPrefs.GetInt("Mycharacter", 0);
        var name = PlayerPrefs.GetString("Mynombre", "");

        nameText.text = name;
        modelsAvailable[selectedHeroe].SetActive(true);

        raycastManager = GetComponent<RaycastManager>();

    }
    void Start()
    {
        characterController = GetComponent<CharacterController>();

        // Lock cursor
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    public void deReversa()
    {
        this.gameObject.transform.position += Vector3.back * 1.5f;
    }

    void Update()
    {
        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);


        if (Input.GetButtonDown("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Move the controller
        if (curSpeedX != 0 || curSpeedY != 0)
            Spawner.Instance.SendPos(transform);
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
            if (Input.GetAxis("Mouse X")!=0)
                Spawner.Instance.SendRot(transform.rotation.eulerAngles.y);

            //if (Input.GetAxis("Mouse X") != 0)
            //{
            //    Spawner.Instance.SendRot(transform.localRotation.y);
            //}
            //apr que el cursor no se vea y este "bloqueado"
            //Cursor.lockState = CursorLockMode.Locked;
            //Cursor.visible = false;
            //colPlayer.enabled = true; //activa colision
        }

        //pax
        if (!canMove)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            //colPlayer.enabled = false; //quita colison
        }
        if (key != null)
            key.SetActive(hasKey);

        if (raycastManager.isAlreadyEnter)
            canMove = false;
        else if(!blockMove)
            canMove = true;

    }
    public void ChangeCameraToFPS()
    {
        fpsCamera.enabled = true;
        playerCamera.enabled = false;
    }

    public void ChangeCameraToShoulder()
    {
        playerCamera.enabled = true;
        fpsCamera.enabled = false;
    }
}