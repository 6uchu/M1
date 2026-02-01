using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance;
    public int WallCnt { get; private set; }
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        WallCnt = 0;
    }

    public void AddWall(int amount)
    {
        WallCnt += amount;
    }

    public void UseWall(int amount = 1)
    {
        WallCnt = Mathf.Max(0, WallCnt - amount);
    }
}
