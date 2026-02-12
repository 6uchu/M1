using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.CullingGroup;

public class EnemyAttack : MonoBehaviour
{
    Enemy enemy;
    public IDamageable currentTarget;

    [Header("공격")]

    [SerializeField] float cooldown = 3f;
    [SerializeField] int atk = 10;

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
        if (state == Enemy.EnemyState.Attack)
            StartAttack();
        else
            StopCoroutine(nameof(AttackLoop));
    }

    void StartAttack()
    {
        if (currentTarget == null) return;

        StopCoroutine(nameof(AttackLoop));
        StartCoroutine(AttackLoop());
    }


    IEnumerator AttackLoop()
    {
        while (currentTarget != null)
        {
            currentTarget.TakeDamage(atk);

            yield return new WaitForSeconds(cooldown);
        }
    }
}
