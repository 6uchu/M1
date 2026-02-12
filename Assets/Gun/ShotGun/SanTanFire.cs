using System.Collections;
using UnityEngine;

public class ShotgunFire : MonoBehaviour, IFireStrategy
{
    [SerializeField] Transform firePos;
    [SerializeField] LayerMask hitMask;

    [Header("총 로직")]
    [SerializeField] GameObject tracerPrefab;
    [SerializeField] float tracerSpeed = 80f;
    [SerializeField] float range = 10f;
    [SerializeField] int pelletCount = 8;
    [SerializeField] float spreadAngle = 15f;

    [Header("넉백")]
    [SerializeField] float knockbackPower;
    Vector2 knockDir;

    Vector2 lastDir = Vector2.right;

    private void Start()
    {
    }

    void Update()
    {
        Vector2 inputDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (inputDir.sqrMagnitude > 0.01f)
            lastDir = inputDir.normalized;
    }

    public void Fire(WeaponData data)
    {
        for (int i = 0; i < pelletCount; i++)
        {
            float angle = Random.Range(-spreadAngle, spreadAngle);
            Vector2 dir = Quaternion.Euler(0, 0, angle) * lastDir;

            RaycastHit2D hit = Physics2D.Raycast(firePos.position, dir, range, hitMask);
            Vector2 endPos = hit ? hit.point : (Vector2)firePos.position + dir * range;

            CreateWeaponTracer(endPos);

            if (!hit) continue;

            if (hit.collider.GetComponentInParent<IDamageable>() is IDamageable d)
            {
                d.TakeDamage(data.damage);

                if (hit.collider.GetComponentInParent<IKnockbackable>() is IKnockbackable k)
                {
                    Enemy enemy = hit.collider.GetComponentInParent<Enemy>();
                    enemy.ChangeState(Enemy.EnemyState.Hit);
                    Animator ani = enemy.GetComponentInChildren<Animator>();
                    ani.SetTrigger("Hit");
                    //knockDir = (hit.transform.position - firePos.position).normalized;
                    //k.Knockback(knockDir, knockbackPower);
                }
            }
        }
    }

    void CreateWeaponTracer(Vector2 endPos)
    {
        GameObject tracer = Instantiate(tracerPrefab, firePos.position, Quaternion.identity);
        StartCoroutine(MoveTracer(tracer, endPos));
    }

    IEnumerator MoveTracer(GameObject tracer, Vector2 endPos)
    {
        Vector2 start = tracer.transform.position;
        float dist = Vector2.Distance(start, endPos);
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * tracerSpeed / dist;
            tracer.transform.position = Vector2.Lerp(start, endPos, t);
            yield return null;
        }

        Destroy(tracer, 0.1f);
    }
}
