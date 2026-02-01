using System.Collections;
using UnityEngine;

public class EnemyDead : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, 2f);
    }
}
