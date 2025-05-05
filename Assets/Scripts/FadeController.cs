using System.Collections;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    public static FadeController Instance { get; private set; }

    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float fadeDuration;

    private void Awake()
    {
        Instance = this;
    }

    public void FadeIn()
    {
        StartCoroutine(SetAlpha(1f));
    }

    public void FadeOut()
    {
        StartCoroutine(SetAlpha(0f));
    }

    private IEnumerator SetAlpha(float alphaTarget)
    {
        float canvasAlpha = canvasGroup.alpha;
        float t = 0;

        while (t < 1f)
        {
            t += Time.deltaTime/fadeDuration;

            canvasGroup.alpha = Mathf.Lerp(canvasAlpha, alphaTarget, t);
            yield return null;
        }

        canvasGroup.alpha = alphaTarget;
    }

}
