using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    [SerializeField] TMP_Text scoreText;
    long score = 0;
    void Start()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(Instance);
            return;
        }
        Instance = this;

        scoreText.text = score.ToString("N0");
    }

    public void AddScore(int add)
    {
        score += add;
        scoreText.text = score.ToString("N0");
    }
}
