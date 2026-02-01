using UnityEngine;

public class EBody : MonoBehaviour
{
    float prevX;
    Enemy enemy;
    void Awake()
    {
        prevX = transform.position.x;
    }

    private void Start()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    void Update()
    {
        if (enemy == null) return;
        Vector2 heading = enemy.Dir;
        if (heading.x > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (heading.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }
}