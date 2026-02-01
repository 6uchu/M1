using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] float maxHp;
    [SerializeField] float hp;
    [SerializeField] Image hpBar;
    [SerializeField] float speed = 5f;

    Rigidbody2D rb;
    Vector2 input;
    public Vector2 LookDir;

    // 이동 제한 범위
    [SerializeField] float minX = -4.5f;
    [SerializeField] float maxX = 4.5f;
    [SerializeField] float minY = -4f;
    [SerializeField] float maxY = 4.5f;

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
        Vector2 newPos = rb.position + input * speed * Time.fixedDeltaTime;

        // Clamp로 제한
        newPos.x = Mathf.Clamp(newPos.x, minX, maxX);
        newPos.y = Mathf.Clamp(newPos.y, minY, maxY);

        rb.MovePosition(newPos);
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        hp = Mathf.Clamp(hp, 0, maxHp);

        if (hpBar != null)
            hpBar.fillAmount = hp / maxHp;

        //if (hp <= 0)
        //    Die();
    }

    public void GetWall()
    {
        Debug.Log(ItemManager.Instance.WallCnt);
        ItemManager.Instance.AddWall(5);
        itemCounterUI.Redate();
    }
}
