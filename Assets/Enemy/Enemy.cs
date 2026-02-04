using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] GameObject DeadBody;
    [SerializeField] GameObject Item;
    bool isTouchingPlayer = false;
    bool isTouchingBarricade = false;
    bool isKnockback = false;
    Player player;
    Barricade barricade;

    [SerializeField] int atk = 10;
    [SerializeField] Image hpBar;
    [SerializeField] int maxHp = 100;
    [SerializeField] float speed = 3f;
    int hp;

    Rigidbody2D rb;
    public Vector2 Dir;

    [SerializeField] float knockbackPower = 1f;

    Canvas cv;
    IDamageable currentTarget;
    [SerializeField] int hitOrder = 10;
    [SerializeField] float hitOrderTime = 1f;
    int defaultOrder;

    [SerializeField] float cooldown = 3f;



    void Start()
    {
        hp = maxHp;
        player = FindAnyObjectByType<Player>();

        cv = GetComponentInChildren<Canvas>();
        defaultOrder = cv.sortingOrder;

        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 move;
        if (isTouchingPlayer || isKnockback)
        {
            move = Vector2.zero;
        }
        else
        {
            Dir = (player.transform.position - transform.position).normalized;
            move = Dir * speed;
        }
        rb.linearVelocity = new Vector2(move.x, move.y);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (currentTarget != null) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            currentTarget = collision.gameObject.GetComponent<Player>();
            isTouchingPlayer = true;
        }
        else if (collision.gameObject.CompareTag("Barricade"))
        {
            currentTarget = collision.gameObject.GetComponent<Barricade>();
        }
        else
            return;
        Debug.Log("ATk" + collision);
        StartCoroutine(AttackLoop());
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            isTouchingPlayer = false;

        currentTarget = null;
    }



    public void TakeDamage(int damage)
    {
        hp -= damage;
        hp = Mathf.Clamp(hp, 0, maxHp);

        if (hpBar != null)
            hpBar.fillAmount = (float)hp / maxHp;

        //StopCoroutine(nameof(HitOrder));
        //StartCoroutine(HitOrder());
        HitOrder();
        Knockback(-Dir);

        if (hp <= 0)
            Die();
    }

    public void Knockback(Vector2 dir)
    {
        isKnockback = true;
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(dir.normalized * knockbackPower, ForceMode2D.Impulse);
        Invoke(nameof(EndKnockback), 0.15f);
    }

    void EndKnockback()
    {
        isKnockback = false;
    }

    void HitOrder()
    {
        cv.sortingOrder = hitOrder;
        //yield return new WaitForSeconds(hitOrderTime);
        //cv.sortingOrder = defaultOrder;
    }

    IEnumerator AttackLoop()
    {
        while (currentTarget != null)
        {
            currentTarget.TakeDamage(atk);

            yield return new WaitForSeconds(cooldown);
        }
    }

    void Die()
    {
        float random = Random.value;
        if (random <= 1)
            Instantiate(Item, transform.position, Quaternion.identity);
        Instantiate(DeadBody, transform.position, Quaternion.identity);

        ScoreManager.Instance.AddScore(100);

        Destroy(gameObject);
    }
}
