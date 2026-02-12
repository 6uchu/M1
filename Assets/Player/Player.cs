using UnityEngine;
using UnityEngine.UI;
using System;

public class Player : MonoBehaviour, IDamageable
{
    public static event Action OnPlayerDead;

    [SerializeField] float maxHp;
    [SerializeField] float hp;
    [SerializeField] Image hpBar;
    [SerializeField] float speed = 5f;

    Rigidbody2D rb;
    Vector2 input;
    public Vector2 LookDir;

    // 이동 제한 범위
    [SerializeField] float maxX = 17f;
    [SerializeField] float minX;
    [SerializeField] float maxY = 17f;
    [SerializeField] float minY;

    [SerializeField] ItemCounterUI itemCounterUI;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        rb.gravityScale = 0;
        maxHp = 100f;
        hp = maxHp;
    }

    private void Start()
    {
        minX = -maxX;
        minY = -maxY;
    }

    void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        input = input.normalized;

        if (input != Vector2.zero)
            LookDir = input;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = input * speed;
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        hp = Mathf.Clamp(hp, 0, maxHp);

        if (hpBar != null)
            hpBar.fillAmount = hp / maxHp;

        if (hp <= 0)
            Die();
    }

    void Die()
    {
        OnPlayerDead?.Invoke();

        gameObject.SetActive(false);
    }

    public void GetWall()
    {
        ItemManager.Instance.AddWall(5);
        itemCounterUI.Redate();
    }
}
