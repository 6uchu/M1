using UnityEngine;
using static Enemy;

public class EBody : MonoBehaviour
{
    Enemy enemy;
    [SerializeField] Canvas cv;
    [SerializeField]int hitOrder = 10;

    void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
        enemy.OnStateChanged += OnStateChanged;
    }

    void OnDestroy()
    {
        enemy.OnStateChanged -= OnStateChanged;
    }


    void Update()
    {
        if (!enemy.CanMove) return;

        Vector2 heading = enemy.Dir;
        if (heading.x > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (heading.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    void OnStateChanged(Enemy.EnemyState state)
    {
        if (state == Enemy.EnemyState.Hit)
            HitOrder();
    }
    public void HitOrder()
    {
        if (cv == null) return;
        cv.sortingOrder = hitOrder;
    }

    public void OnHitAnimationEnd()
    {
        Debug.Log("무브");
        enemy.ChangeState(EnemyState.Move);
    }
}