using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class RollaBallPlayer : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float JumpPadForce = 800f;
    private Vector2 m;
    private Rigidbody rb;
    private Camera main;
    private bool isGrounded;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m = new Vector2(0, 0);
        rb = GetComponent<Rigidbody>();
        main = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -250)
            KillPlayer();
        
        float x_dir = m.x;
        float z_dir = m.y;
        Vector3 cameraForward = main.transform.forward;
        Vector3 cameraRight = main.transform.right;

        // Flatten the camera's forward and right vectors to keep movement grounded
        cameraForward.y = 0;
        cameraRight.y = 0;
        cameraForward.Normalize();
        cameraRight.Normalize();

        // Combine the input with camera direction
        rb.AddForce(cameraForward * (z_dir * moveSpeed) + cameraRight * (x_dir * moveSpeed));
    }

    void OnMove(InputValue movement)
    {
        m = movement.Get<Vector2>();
    }

    void OnJump()
    {
        if (isGrounded)
            rb.AddForce(0,300,0);
    }

    public void KillPlayer()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
            isGrounded = true;
        if (other.gameObject.CompareTag("Killer"))
            KillPlayer();
        if (other.gameObject.CompareTag("JumpPad"))
            rb.AddForce(Vector3.up * JumpPadForce);
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
            isGrounded = false;
    }
}
