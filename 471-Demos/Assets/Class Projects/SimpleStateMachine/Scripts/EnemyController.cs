using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletPosition;
    [SerializeField] private float shootSpeed = 8f;
    [SerializeField] private float bulletAirTime = 3f;
    [SerializeField] private float shotCooldown = 2f;

    private float timer = 0f;
    [SerializeField] private int speed = 1;
    public int health = 5;
    
    [SerializeField] private GameObject[] route;
    private GameObject target;
    private int routeIndex = 0;
    
    private enum State
    {
        Pace,
        Follow
    }

    private State currentState = State.Pace;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            GameStateManager.Instance.enemyCount--;
            Destroy(gameObject);
        }
        switch (currentState)
        {
            case State.Pace:
                OnPace();
                break;
            case State.Follow:
                OnFollow();
                break;
        }
    }

    void OnPace()
    {
        //What do we do when we're pacing?
        // print("I'm pacing!");
        target = route[routeIndex];
        
        MoveTo(target);
        if (Vector3.Distance(transform.position, target.transform.position) <= 0.1)
        {
            routeIndex += 1;
            if (routeIndex >= route.Length)
                routeIndex = 0;
        }

        //On what condition do we switch states?
        
        GameObject obstacle = CheckForward();
        if (obstacle != null)
        {
            target = obstacle;
            currentState = State.Follow;
        }


        
    }

    void OnFollow()
    {
        //What do we do when we are following?
        // print("I'm following!");
        MoveTo(target);
        
        
        //On what condition do we stop following?
        GameObject obstacle = CheckForward();

        if (obstacle == null)
        {
            currentState = State.Pace;
        }
        
        if (timer <= 0f)
        {
            Vector3 direction = (target.transform.position - transform.position).normalized;

            GameObject creation = Instantiate(bullet, bulletPosition.position + bulletPosition.forward * 0.5f, Quaternion.LookRotation(direction));
            creation.transform.localScale = transform.localScale;
    
            Rigidbody objectRb = creation.GetComponent<Rigidbody>();
            objectRb.mass = 0.1f; 
            objectRb.AddForce(direction * shootSpeed, ForceMode.Impulse);
    
            Destroy(creation, bulletAirTime);
            timer = shotCooldown;
        }

        timer -= Time.deltaTime;
    }

    void MoveTo(GameObject targetObject)
    {
        transform.position = Vector3.MoveTowards(transform.position, targetObject.transform.position, speed * Time.deltaTime);
        transform.LookAt(targetObject.transform, Vector3.up);
    }

    GameObject CheckForward()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward * 10, Color.green);

        if (Physics.Raycast(transform.position, transform.forward, out hit, 10))
        {
            PlayerStateManager player = hit.transform.gameObject.GetComponent<PlayerStateManager>();
            
            if (player != null)
            {
                
                if(player.currentState != player.sneakState) 
                    return hit.transform.gameObject;
                // print(hit.transform.gameObject.name);
                
            }
        }
        return null;
    }

    private void OnTriggerEnter(Collider  other)
    {
        health--;
    }
}
