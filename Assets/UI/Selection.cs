using UnityEngine;
using UnityEngine.UI;

public class Selection : MonoBehaviour
{
    public Image img;
    public static Selection current;

    public float selectedAlpha = 1f;
    public float unselectedAlpha = 0.3f;

    void Awake()
    {
        img = GetComponent<Image>();
        SetAlpha(unselectedAlpha);
    }

    public void OnClick()
    {
        if (current != null)
            current.SetAlpha(unselectedAlpha);

        current = this;
        SetAlpha(selectedAlpha);
    }

    void SetAlpha(float a)
    {
        Color c = img.color;
        c.a = a;
        img.color = c;
    }
}
