using UnityEngine;

public class DeadBody : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DestroyIn2();
    }

    private void DestroyIn2()
    {
        Destroy(gameObject, 2f);
    }
}
