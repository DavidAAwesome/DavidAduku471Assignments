using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float shootSpeed = 50f;
    
    private Rigidbody rb;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(transform.forward * shootSpeed);
    }
}
