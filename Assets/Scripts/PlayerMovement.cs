using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float sprintSpeed = 8f;
    public float crouchSpeed = 3f;
    public float jumpForce = 5f;
    public float crouchHeight = 1f;
    public float standHeight = 2f;
    public float mouseSensitivity = 100f;
    public Camera playerCamera;

    private float xRotation = 0f;
    private Rigidbody rb;
    private bool isCrouching = false;
    private bool isSprinting = false;
    private float currentSpeed;
    private Vector3 originalScale;
    private int jumpCount = 0; // Compteur de sauts

    //Player related variable
    int maxJumps = 0; // Nombre maximal de sauts
    bool canCrouch = false;
    bool canJump = false;
    [SerializeField] private GameObject HeadGameObject;

    [SerializeField] private GameObject Player1Mesh;
    [SerializeField] private GameObject Player2Mesh;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;

        if (playerCamera == null)
        {
            playerCamera = GetComponentInChildren<Camera>();
            if (playerCamera == null)
            {
                Debug.LogError("Aucune cam�ra trouv�e dans les enfants du Player !");
            }
        }

        currentSpeed = walkSpeed;
        originalScale = transform.localScale;
    }

    void Update()
    {
        // Sprint avec Maj
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            isSprinting = true;
            currentSpeed = sprintSpeed;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            isSprinting = false;
            currentSpeed = walkSpeed;
        }

        // Accroupissement avec Ctrl
        if (Input.GetKeyDown(KeyCode.LeftControl) && canCrouch)
        {
            isCrouching = true;
            transform.localScale = new Vector3(originalScale.x, crouchHeight, originalScale.z);
            currentSpeed = crouchSpeed;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            isCrouching = false;
            transform.localScale = originalScale;
            currentSpeed = isSprinting ? sprintSpeed : walkSpeed;
        }

        // Saut avec Espace
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (IsGrounded() && canJump)
            {
                // Premier saut
                jumpCount = 1;
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
            else if (jumpCount < maxJumps && canJump)
            {
                // Double saut
                jumpCount++;
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z); // Reset vitesse verticale
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }

        // Récupération des inputs de déplacement
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Calcul de la direction du mouvement par rapport à la caméra
        Vector3 moveDirection = playerCamera.transform.forward * moveZ + playerCamera.transform.right * moveX;
        moveDirection.y = 0; // On évite de modifier la hauteur

        // Déplacement
        transform.position += moveDirection.normalized * currentSpeed * Time.deltaTime;

        // Rotation de la caméra avec la souris
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotation en X de la caméra (pitch)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 20f);
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 90f, 0f);

        // Rotation en Y du personnage (yaw)
        transform.Rotate(Vector3.up * mouseX);
    }


    bool IsGrounded()
    {
        bool grounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);
        if (grounded)
        {
            jumpCount = 0; // R�initialise le compteur de sauts
        }
        return grounded;
    }

    public void SetPlayerBehaviorVariable(int playerIndex)
    {
        if (playerIndex == 1)
        {
            maxJumps = 2;
            canJump = true;
            Player2Mesh.SetActive(false);
            // HeadGameObject.transform.localPosition = new Vector3(0, 0, 0);
        }
        else if (playerIndex == 2)
        {
            canCrouch = true;
            HeadGameObject.transform.localPosition = new Vector3(0, -1.3f, 0);
            Player1Mesh.SetActive(false);
        }
        else
        {
            Debug.LogError("Wrong player index inputed");
        }
    }
}