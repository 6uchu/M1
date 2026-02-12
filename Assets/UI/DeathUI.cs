using TMPro;
using UnityEngine;
using System.Collections;

public class DeathUI : MonoBehaviour
{
    [SerializeField] RectTransform youDied;
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] TMP_Text text;

    void OnEnable()
    {
        Player.OnPlayerDead += Play;
    }

    void OnDisable()
    {
        Player.OnPlayerDead -= Play;
    }

    void Start()
    {
        canvasGroup.alpha = 0f;
    }

    public void Play()
    {
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        text.text = "SCORE: " + ScoreManager.Instance.GetScore();
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float duration = 0.3f;
        float time = 0f;

        while (time < duration)
        {
            time += Time.unscaledDeltaTime;
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, time / duration);
            yield return null;
        }

        canvasGroup.alpha = 1f;
    }
}