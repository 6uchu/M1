using UnityEngine;

public class Gun : MonoBehaviour
{
    public WeaponData data;
    IFireStrategy fire;

    void Awake()
    {
        fire = GetComponent<IFireStrategy>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
    }

    public void Fire()
    {
        if (fire == null) return;
        fire.Fire(data);
    }
}
