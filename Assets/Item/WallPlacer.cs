using UnityEngine;

public class WallPlacer : MonoBehaviour
{
    [SerializeField] GameObject wallPrefab;
    [SerializeField] Player player;
    [SerializeField] float placeDistance = 1f;
    [SerializeField] LayerMask wallMask;
    [SerializeField] ItemCounterUI itemCounterUI;

    private void Start()
    {
        player = GetComponentInParent<Player>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryPlace();
        }
    }

    void TryPlace()
    {
        Vector2 dir = player.LookDir;
        Vector2 pos = (Vector2)player.transform.position + dir * placeDistance;
        pos = Snap(pos);

        //bool canPlace = !Physics2D.OverlapBox(
        //    pos,
        //    Vector2.one,
        //    0f,
        //    wallMask
        //);

        //if (canPlace)
        if(ItemManager.Instance.WallCnt > 0)
        {
            Instantiate(wallPrefab, pos, Quaternion.identity);
            ItemManager.Instance.UseWall();
            itemCounterUI.Redate();
        }
    }
    

    Vector2 Snap(Vector2 pos)
    {
        return new Vector2(
            Mathf.Round(pos.x),
            Mathf.Round(pos.y)
        );
    }
}
