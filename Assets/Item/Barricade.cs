using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Barricade : MonoBehaviour, IDamageable
{
    [SerializeField] Image hpBar;
    [SerializeField] int maxHp = 100;
    [SerializeField] int hitOrder = 10;
    [SerializeField] float hitOrderTime = 1f;
    int defaultOrder = 1;
    Canvas cv;
    int hp;

    void Start()
    {
        hp = maxHp;
        cv = GetComponentInChildren<Canvas>();
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

        if (hp <= 0)
            Die();
    }
    void HitOrder()
    {
        cv.sortingOrder = hitOrder;
        //yield return new WaitForSeconds(hitOrderTime);
        //cv.sortingOrder = defaultOrder;
    }

    void Die()
    {
        Destroy(gameObject);
    }
}

