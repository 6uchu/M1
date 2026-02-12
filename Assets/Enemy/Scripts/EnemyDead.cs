using System.Collections;
using UnityEngine;

public class EnemyDead : MonoBehaviour
{
    [SerializeField] GameObject Item;
    [SerializeField] GameObject DeadBody;
    Enemy enemy;

    void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
        enemy.OnStateChanged += OnStateChanged;
    }

    void OnDestroy()
    {
        enemy.OnStateChanged -= OnStateChanged;
    }

    void OnStateChanged(Enemy.EnemyState state)
    {
        if (state == Enemy.EnemyState.Dead)
            Die();
    }

    bool isDead = false;

    public void Die()
    {
        if (isDead || DeadBody == null) return;
        isDead = true;

        float random = Random.value;
        if (random <= 1 && Item != null)
            Instantiate(Item, transform.position, Quaternion.identity);
        if(DeadBody != null)
            Instantiate(DeadBody, transform.position, Quaternion.identity);

        ScoreManager.Instance.AddScore(100);

        Destroy(enemy.gameObject);
    }
}
