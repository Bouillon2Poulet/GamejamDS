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
    public int maxJumps = 2; // Nombre maximal de sauts

    private float xRotation = 0f;
    private Rigidbody rb;
    private bool isCrouching = false;
    private bool isSprinting = false;
    private float currentSpeed;
    private Vector3 originalScale;
    private int jumpCount = 0; // Compteur de sauts

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
        if (Input.GetKeyDown(KeyCode.LeftControl))
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
            if (IsGrounded())
            {
                // Premier saut
                jumpCount = 1;
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
            else if (jumpCount < maxJumps)
            {
                // Double saut
                jumpCount++;
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z); // R�initialise la vitesse verticale
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }

        // Mouvement ZQSD
        float x = Input.GetAxis("Horizontal") * currentSpeed * Time.deltaTime;
        float z = Input.GetAxis("Vertical") * currentSpeed * Time.deltaTime;

        transform.Translate(x, 0, z);

        // Rotation de la cam�ra avec la souris
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
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
}