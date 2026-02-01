using System.Collections;
using UnityEngine;

public class SingleFire : MonoBehaviour, IFireStrategy
{
    [SerializeField] Transform firePos;                  // 총구 위치
    [SerializeField] ParticleSystem ShootingSystem;      // 발사 이펙트
    [SerializeField] ParticleSystem ImpactParticleSystem;// 맞았을 때 이펙트
    [SerializeField] LayerMask hitMask;                 // 충돌할 레이어
    [SerializeField] float range = 10f;                 // 사거리
    [SerializeField] GameObject tracerPrefab;
    [SerializeField] float tracerSpeed = 80f;

    //Animator anim;
    Vector2 lastDir = Vector2.right;

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

        Vector2 dir = lastDir;

        // 2D 히트스캔 Raycast
        RaycastHit2D hit = Physics2D.Raycast(firePos.position, dir, range, hitMask);
        Vector2 endPos = hit ? hit.point : (Vector2)firePos.position + dir * range;

        // 시각적 선 표시
        CreateWeaponTracer(endPos);

        // 충돌 처리
        if (hit)
        {
            //ImpactParticleSystem?.Play();
            if (hit.collider.TryGetComponent<IDamageable>(out var d))
            {
                d.TakeDamage(data.damage);
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

        Destroy(tracer, 0.1f); // 트레일 남기고 제거
    }
}