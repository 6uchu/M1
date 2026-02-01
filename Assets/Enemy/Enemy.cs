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
    Player player;
    Barricade barricade;

    [SerializeField] int atk = 10;
    [SerializeField] Image hpBar;
    [SerializeField] int maxHp = 100;
    [SerializeField] float speed = 3f;
    int hp;

    Rigidbody2D rb;
    public Vector2 Dir;

    Canvas cv;
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
        if (isTouchingPlayer || isTouchingBarricade) return;

        Dir = (player.transform.position - transform.position).normalized;
        rb.MovePosition(rb.position + Dir * speed * Time.fixedDeltaTime);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            isTouchingPlayer = true;

        if (collision.gameObject.CompareTag("Barricade")){
            barricade = collision.gameObject.GetComponent<Barricade>();
            isTouchingBarricade = true;
        }
        StartCoroutine(AttackLoop());
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            isTouchingPlayer = false;

        if (collision.gameObject.CompareTag("Barricade"))
            isTouchingBarricade = false;
    }



    public void TakeDamage(int damage)
    {
        hp -= damage;
        hp = Mathf.Clamp(hp, 0, maxHp);

        if (hpBar != null)
            hpBar.fillAmount = (float)hp / maxHp;

        StopCoroutine(nameof(HitOrder));
        StartCoroutine(HitOrder());

        if (hp <= 0)
            Die();
    }

    IEnumerator HitOrder()
    {
        cv.sortingOrder = hitOrder;
        yield return new WaitForSeconds(hitOrderTime);
        cv.sortingOrder = defaultOrder;
    }

    IEnumerator AttackLoop()
    {
        while (isTouchingPlayer || isTouchingBarricade)
        {
            if (isTouchingPlayer)
                player.TakeDamage(atk);
            if (isTouchingBarricade)
                barricade.TakeDamage(atk);

            yield return new WaitForSeconds(cooldown);
        }
    }

    void Die()
    {
        float random = Random.value;
        if (random <= 1)
            Instantiate(Item, transform.position, Quaternion.identity);
        Instantiate(DeadBody, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
