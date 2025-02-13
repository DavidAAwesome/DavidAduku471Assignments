using UnityEngine;

public class Testing : MonoBehaviour
{

    public GameObject cube;
    private Transform cubeTransform;
    float speed = 0.001f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cubeTransform = cube.transform;//cube.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(cubeTransform.position.y > 10)
        {
            speed *= -1;
        }
        else if(cubeTransform.position.y < -10)
        {
            speed *= -1;
        }
        cubeTransform.Translate(0, speed, 0);
            
    }
}
