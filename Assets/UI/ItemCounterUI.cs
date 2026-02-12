using TMPro;
using UnityEngine;

public class ItemCounterUI : MonoBehaviour
{
    [SerializeField] TMP_Text Walltxt;
    void Start()
    {
    }

    // Update is called once per frame
    public void Redate()
    {
        if (ItemManager.Instance == null) { return; }
        Walltxt.text = ItemManager.Instance.WallCnt.ToString();
    }
}
