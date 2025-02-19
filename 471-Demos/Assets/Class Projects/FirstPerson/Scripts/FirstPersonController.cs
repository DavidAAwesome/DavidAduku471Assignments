using System;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public class FirstPersonController : MonoBehaviour
{
    [SerializeField] public int health = 5;
    [SerializeField] private float speed = 2.0f;
    [SerializeField] private float jumpHeight = 5.0f;
    [SerializeField] private float JumpPadForce = 6.0f;
    [SerializeField] private GameObject camera;
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private GameObject bulletSpawner;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Texture2D crosshairTexture;
    [SerializeField] private float crosshairScale = 0.2f;
    
    private Vector2 movement;
    private Vector2 mouseMovement;
    private float cameraUpRotation = 0;
    private CharacterController controller;
    private bool jumped = false;
    private bool jumpPadBoosted = false;
    
    private float verticalVelocity = 0;

    private GameStateManager gamestateManager;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
        gamestateManager = GameStateManager.Instance;
        
        // Change the look of the cursor and lock it to the middle of the screen when the game starts
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.SetCursor(crosshairTexture, Vector2.zero, CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        if (gamestateManager.currentState == GameStateManager.GameState.Playing || gamestateManager.currentState == GameStateManager.GameState.Won)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.SetCursor(crosshairTexture, Vector2.zero, CursorMode.Auto);
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }

        if (transform.position.y < -20)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            
        float lookX = mouseMovement.x * Time.deltaTime * mouseSensitivity;
        float lookY = mouseMovement.y * Time.deltaTime * mouseSensitivity;
        
        cameraUpRotation -= lookY;
        cameraUpRotation = Mathf.Clamp(cameraUpRotation, -90, 90);
        
        camera.transform.localRotation = Quaternion.Euler(cameraUpRotation, 0, 0);
        
        transform.Rotate(Vector3.up * lookX);
        
        float moveX = movement.x;
        float moveZ = movement.y;

        Vector3 actual_movement = (transform.forward * moveZ) + (transform.right * moveX);

        if (controller.isGrounded)
        {
            if (jumped)
            {
                verticalVelocity = jumpHeight; 
                jumped = false;
            }
            else if (jumpPadBoosted)
            {
                verticalVelocity = JumpPadForce;
                jumpPadBoosted = false;
            }
            else
                verticalVelocity = -0.5f;
        }
        else
            verticalVelocity += Physics.gravity.y * Time.deltaTime;

        actual_movement.y = verticalVelocity;
        controller.Move(actual_movement * (Time.deltaTime * speed));
    }

    void OnMove(InputValue moveVal)
    {
        movement = moveVal.Get<Vector2>();
    }

    void OnLook(InputValue lookVal)
    {
        mouseMovement = lookVal.Get<Vector2>();
    }

    void OnAttack()
    {
        if(gamestateManager.currentState == GameStateManager.GameState.Playing || gamestateManager.currentState == GameStateManager.GameState.Won)
            Instantiate(bullet, bulletSpawner.transform.position, bulletSpawner.transform.rotation);
    }

    void OnJump()
    {
        jumped = true;
    }
    
    void OnGUI()
    {
        float width = crosshairTexture.width * crosshairScale;
        float height = crosshairTexture.height * crosshairScale;
        float x = (Screen.width - width) / 2;
        float y = (Screen.height - height) / 2;
        GUI.DrawTexture(new Rect(x, y, width, height), crosshairTexture);
    }

    private void OnCollisionEnter(Collision other)
    {
            // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        if (other.gameObject.CompareTag("JumpPad"))
            jumpPadBoosted = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            health--;
            Debug.Log("Collided with Enemy!!");
        }
    }
    
}
