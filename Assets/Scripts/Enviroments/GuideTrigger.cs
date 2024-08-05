using System.Collections;
using TMPro;
using UnityEngine;

public class GuideTrigger : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI guideText;
    [SerializeField] float existDuration;
    [SerializeField] float alphaChangeSpeed;

    void Start()
    {
        guideText.gameObject.SetActive(false);
        guideText.alpha = 0f;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Tags.Player))
        {
            guideText.gameObject.SetActive(true);
            StartCoroutine(CR_Appear());
        }
    }

    IEnumerator CR_Appear()
    {
        float tick = 0f;

        while (guideText.alpha < 1f)
        {
            tick += Time.deltaTime;
            guideText.alpha += alphaChangeSpeed * tick;
            yield return null;
        }

        yield return new WaitForSeconds(existDuration);

        tick = 0f;
        while (guideText.alpha > 0f)
        {
            tick += Time.deltaTime;
            guideText.alpha -= alphaChangeSpeed * tick;
            yield return null;
        }
        
        Destroy(guideText.gameObject);
        Destroy(gameObject);
    }
}
