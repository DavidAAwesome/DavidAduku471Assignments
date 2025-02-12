using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int health = 5;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float rotationSpeed = 1000f;
    
    private Transform target;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = FindObjectOfType<FirstPersonController>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            GameManager.Instance.enemyCount--;
            Destroy(gameObject);
        }
            
    }
    
    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);;
        Vector3 direction = target.position - transform.position;
        // Rotate player to face movement direction if moving
        if (direction != Vector3.zero)
        {
            Quaternion toRotate = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotate, rotationSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PlayerBullet"))
            health--;
    }
    
    
}
