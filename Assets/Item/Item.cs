using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] Player player;
    float random;
    void Start()
    {
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")){
            player = collision.GetComponentInParent<Player>();
            if (player == null) return;

            random = Random.value;
            Debug.Log(random);
            WhichOne(random);
            Destroy(gameObject);
        }
    }

    void WhichOne(float ran)
    {
        if(ran < 0.5)
        {
            Debug.Log("GetWall");
            player.GetWall();
        }
    }
}
