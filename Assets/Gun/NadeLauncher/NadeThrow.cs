using System.Collections;
using UnityEngine;

public class NadeThrow : MonoBehaviour, IFireStrategy
{
    [SerializeField] Transform firePos;                  // 총구 위치
    [SerializeField] ParticleSystem ShootingSystem;      // 발사 이펙트
    [SerializeField] GameObject nadePrefab;


    //Animator anim;
    Vector2 lastDir = Vector2.right;
    Rigidbody2D playerRb;
    void Update()
    {
        Vector2 inputDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (inputDir.sqrMagnitude > 0.01f)
            lastDir = inputDir.normalized;
    }

    public void Fire(WeaponData data)
    {
        //anim.SetTrigger("Shoot");d
        //ShootingSystem?.Play();
        playerRb = GetComponentInParent<Rigidbody2D>();
        Vector2 playerVelocity = playerRb.linearVelocity;

        // 시각적 선 표시
        GameObject obj = Instantiate(nadePrefab, firePos.position, Quaternion.identity);
        obj.GetComponent<Nade>().Init(lastDir, playerVelocity, data.damage);
        WeaponManager.Instance.UseAmmo();
    }
}