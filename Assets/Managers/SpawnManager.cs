using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] int quantity;

    private void Awake()
    {
        if (instance == null && instance != this)
            instance = this;
        else
            Destroy(gameObject);
    }
    void Start()
    {
        quantity = 5;

        spawnPoints = GetComponentsInChildren<Transform>();
        StartCoroutine(SpawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator SpawnEnemy()
    {
        while (quantity > 0)
        {
            yield return new WaitForSeconds(2f);

            int randomIndex = Random.Range(1, spawnPoints.Length);
            Instantiate(enemyPrefab, spawnPoints[randomIndex].position, Quaternion.identity);
            quantity--;
        }
    }
}
