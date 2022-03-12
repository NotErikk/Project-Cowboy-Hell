using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeAndThenDestroyOnTimer : MonoBehaviour
{
    [SerializeField] float secondsToWaitBeforeFadeStart;
    [SerializeField] float secondsFadeToLastBeforeDestroy;
    WaitForSeconds waitForFadeSeconds;

    SpriteRenderer spriteRenderer;

    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        waitForFadeSeconds = new WaitForSeconds(secondsToWaitBeforeFadeStart);
    }

    public void Start()
    {
        StartCoroutine(FadeAndDestroy());
    }

    IEnumerator FadeAndDestroy()
    {

        yield return waitForFadeSeconds;

        float elapsedTime = 0;
        float startAlpha = spriteRenderer.color.a;

        while (elapsedTime < secondsFadeToLastBeforeDestroy)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, 0, elapsedTime / secondsFadeToLastBeforeDestroy);
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, newAlpha);
            yield return null;
        }

        Destroy(gameObject);

    }
}
