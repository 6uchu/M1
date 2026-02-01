using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Barricade : MonoBehaviour, IDamageable
{
    [SerializeField] Image hpBar;
    [SerializeField] int maxHp = 100;

    Canvas canvas;
    int hp;

    void Start()
    {
        hp = maxHp;
        canvas = GetComponent<Canvas>();
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        hp = Mathf.Clamp(hp, 0, maxHp);

        if (hpBar != null)
            hpBar.fillAmount = (float)hp / maxHp;

        if (hp <= 0)
            Die();
    }

    void Die()
    {
        Destroy(gameObject);
    }
}

