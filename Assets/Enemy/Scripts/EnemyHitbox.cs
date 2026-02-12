using UnityEngine;

public class EnemyHitbox : MonoBehaviour
{
    Enemy enemy;
    EnemyAttack enemyAttack;

    private void Start()
    {
        enemy = GetComponentInParent<Enemy>();
        enemyAttack = GetComponentInParent<EnemyAttack>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (enemy.State == Enemy.EnemyState.Attack) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            enemyAttack.currentTarget = collision.gameObject.GetComponent<Player>();
        }
        else if (collision.gameObject.CompareTag("Barricade"))
        {
            enemyAttack.currentTarget = collision.gameObject.GetComponent<Barricade>();
        }
        else
            return;

        enemy.ChangeState(Enemy.EnemyState.Attack);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (enemyAttack.currentTarget == null) return;

        if (collision.gameObject == ((Component)enemyAttack.currentTarget).gameObject)
        {
            enemyAttack.currentTarget = null;
            enemy.ChangeState(Enemy.EnemyState.Move);
        }
    }
}
