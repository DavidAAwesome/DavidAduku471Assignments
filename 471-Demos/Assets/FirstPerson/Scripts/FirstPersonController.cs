using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public class FirstPersonController : MonoBehaviour
{
    [SerializeField] private float speed = 2.0f;
    [SerializeField] private GameObject camera;
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private GameObject bulletSpawner;
    [SerializeField] private GameObject bullet;
    
    private Vector2 movement;
    private Vector2 mouseMovement;
    private float cameraUpRotation = 0;
    private CharacterController controller;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float lookX = mouseMovement.x * Time.deltaTime * mouseSensitivity;
        float lookY = mouseMovement.y * Time.deltaTime * mouseSensitivity;
        
        cameraUpRotation -= lookY;
        cameraUpRotation = Mathf.Clamp(cameraUpRotation, -90, 90);
        
        camera.transform.localRotation = Quaternion.Euler(cameraUpRotation, 0, 0);
        
        transform.Rotate(Vector3.up * lookX);
        
        float moveX = movement.x;
        float moveZ = movement.y;

        Vector3 actual_movement = (transform.forward * moveZ) + (transform.right * moveX);
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
        Instantiate(bullet, bulletSpawner.transform.position, bulletSpawner.transform.rotation);
    }
}
