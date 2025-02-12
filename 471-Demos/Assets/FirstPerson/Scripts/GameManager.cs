using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform[] spawnPoints;

    [SerializeField] private float waveWaitTime = 3f;
    
    public int enemyCount;
    public int progression = 1;
    private float spawnCooldown = 0.5f;
    private bool isSpawning = false;
    
    public static GameManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        StartCoroutine(WaitBetweenWaves());
        StartCoroutine(SpawnWave());
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyCount <= 0)
        {
            progression++;
            spawnCooldown -= 0.06f;
            StartCoroutine(WaitBetweenWaves());
            StartCoroutine(SpawnWave());
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
    }

    private IEnumerator WaitBetweenWaves()
    {
        yield return new WaitForSeconds(waveWaitTime);
    }

    private IEnumerator SpawnWave()
    {
        isSpawning = true;
        int enemiesToSpawn = progression;

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnCooldown);
        }

        isSpawning = false;
    }
    
    void SpawnEnemy()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        enemyCount++;
    }
    
    
}
