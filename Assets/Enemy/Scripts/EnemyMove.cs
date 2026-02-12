using UnityEngine;
using static Enemy;

public class EnemyMove : MonoBehaviour
{
    Enemy enemy;
    Player player;

    [Header("이동")]
    [SerializeField] float speed = 3f;
    void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
        enemy.OnStateChanged += OnStateChanged;
    }

    void Start()
    {
        player = FindAnyObjectByType<Player>();
    }

    void OnDestroy()
    {
        enemy.OnStateChanged -= OnStateChanged;
    }

    void OnStateChanged(Enemy.EnemyState state)
    {
        if (state != Enemy.EnemyState.Move)
            enemy.RB.linearVelocity = Vector2.zero;
    }
    void FixedUpdate()
    {
        if (!enemy.CanMove) { return; }
        Vector2 dir;
        if (player == null) return;
        dir = (player.transform.position - transform.position).normalized;
        enemy.Dir = dir;
        
        enemy.RB.linearVelocity = dir * speed;
    }

    void Update()
    {
        
    }
}
