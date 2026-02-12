using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    EBody ebody;
    Enemy enemy;

    [Header("체력바")]
    int hp;
    [SerializeField] Image hpBar;
    [SerializeField] int maxHp = 100;
    private void Start()
    {
        enemy = GetComponent<Enemy>();
        ebody = GetComponentInChildren<EBody>();
        hp = maxHp;

        if (hpBar)
            hpBar.fillAmount = 1f;
    }
    public void TakeDamage(int damage)
    {
        hp -= damage;
        hp = Mathf.Clamp(hp, 0, maxHp);

        if (hpBar != null)
            hpBar.fillAmount = (float)hp / maxHp;

        if (ebody != null)
            ebody.HitOrder();

        if (hp <= 0){
            enemy.ChangeState(Enemy.EnemyState.Dead);
        }
    }
}
