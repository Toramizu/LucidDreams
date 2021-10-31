using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Notification : MonoBehaviour
{
    [SerializeField] float lifeDuration;
    [SerializeField] float moveLength;

    [SerializeField] float fadeDuration;

    [SerializeField] TMP_Text text;
    public string Text
    {
        get { return text.text; }
        set { text.text = value; }
    }

    [SerializeField] CanvasGroup canvasGroup;

    public void Display()
    {
        gameObject.SetActive(true);
        iTween.MoveBy(gameObject, iTween.Hash(
            "y", -moveLength,
            "time", lifeDuration + fadeDuration,
            "easeType", iTween.EaseType.linear,
            "onComplete", "Delete"
            ));

        StartCoroutine(LifeDuration(lifeDuration));
    }

    IEnumerator LifeDuration(float duration)
    {
        yield return new WaitForSeconds(duration);

        iTween.ValueTo(gameObject,
             iTween.Hash(
             "from", 1f,
             "to", 0f,
             "time", fadeDuration,
             "onupdate", "ChangeAlpha",
             "onComplete", "Destroy"
             ));
    }

    void ChangeAlpha(float alpha)
    {
        canvasGroup.alpha = alpha;
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
}
