    using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float normalDistance = 5.0f;
    public float rotationSpeed = 3.0f;
    public float transitionSpeed = 5.0f;
    
    private Transform player;
    private float currentAngleY;
    private float currentAngleX;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindObjectOfType<RollaBallPlayer>().transform;
        // Hide and lock the cursor when the game starts
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    void LateUpdate()
    {
        // Get input from the mouse or joystick
        currentAngleY += Input.GetAxis("Mouse X") * rotationSpeed;
        currentAngleX -= Input.GetAxis("Mouse Y") * rotationSpeed;
        currentAngleX = Mathf.Clamp(currentAngleX, -45, 45);

        // Calculate the target position and smoothly move the camera
        Quaternion rotation = Quaternion.Euler(currentAngleX, currentAngleY, 0);
        Vector3 targetPosition = player.position - (rotation * Vector3.forward * normalDistance);
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * transitionSpeed);
        
        transform.LookAt(player.position);
    }
}
