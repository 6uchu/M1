using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] Transform[] spawnPoints;
    void OnEnable()
    {
        Player.OnPlayerDead += Play;
    }

    void OnDisable()
    {
        Player.OnPlayerDead -= Play;
    }

    public void Play()
    {
        gameObject.SetActive(false);
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            long score = ScoreManager.Instance.GetScore();
            float cooltime = 3f / (Mathf.Log(score + 10f) + 1f);
            cooltime = Mathf.Max(0.2f, cooltime);
            yield return new WaitForSeconds(cooltime);

            int randomIndex = Random.Range(0, spawnPoints.Length);
            Instantiate(enemyPrefab, spawnPoints[randomIndex].position, Quaternion.identity);
        }
    }
}
