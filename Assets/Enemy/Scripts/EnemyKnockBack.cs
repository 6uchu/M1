using UnityEngine;
using static Enemy;

public class EnemyKnockBack : MonoBehaviour, IKnockbackable
{
    Enemy enemy;

    [Header("넉백")]
    [SerializeField] float knockbackTime = 0.3f;
    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }
    public void Knockback(Vector2 dir, float knockbackPower)
    {
        enemy.ChangeState(EnemyState.Knockback);
        enemy.RB.AddForce(dir.normalized * knockbackPower, ForceMode2D.Impulse);
        Invoke(nameof(EndKnockback), knockbackTime);
    }

    public void EndKnockback()
    {
        enemy.ChangeState(EnemyState.Move);
    }
}
