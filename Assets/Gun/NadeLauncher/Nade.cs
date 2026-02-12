using UnityEngine;

public class Nade : MonoBehaviour
{
    [SerializeField] LayerMask targetMask;
    [SerializeField] float radius = 3f;
    [SerializeField] int maxDamage = 100;

    Rigidbody2D rb;
    Animator ani;
    
    [SerializeField] float speed = 4f;
    
    [Header("넉백")]
    [SerializeField] float knockbackPower;
    Vector2 knockDir;
    
    void Start()
    {
        ani = GetComponent<Animator>();
    }

    public void Init(Vector2 dir, Vector2 playerVelocity, int damage)
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = dir * speed + playerVelocity;

        maxDamage = damage;

        Invoke(nameof(Ani), .25f);
    }

    void Ani()
    {
        ani.SetTrigger("explode");

        rb.linearVelocity = Vector2.zero;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }
    void Explode()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius, targetMask);

        foreach (var hit in hits)
        {
            if (hit.GetComponentInParent<IDamageable>() is IDamageable d)
            {
                float dist = Vector2.Distance(transform.position, hit.transform.position);
                float ratio = 1f - Mathf.Clamp01(dist / radius);
                int damage = Mathf.RoundToInt(maxDamage * ratio);

                if (damage <= 0) continue;
                d.TakeDamage(damage);
                if (hit.GetComponentInParent<IKnockbackable>() is IKnockbackable k)
                {
                    Debug.Log("넉백");
                    knockDir = hit.transform.position - transform.position;
                    k.Knockback(knockDir, knockbackPower);
                }
            }
        }

        Destroy(gameObject, ani.GetCurrentAnimatorStateInfo(0).length - 0.16f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
