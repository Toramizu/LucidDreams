using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{
    protected CanvasGroup canvasGroup;
    public bool Active { get; set; }

    protected virtual void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        Active = false;
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
    }

    /*public virtual void Open()
    {
        active = true;
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
    }

    public virtual void Close()
    {
        active = false;
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
    }

    public void FadeIn()
    {
        active = true;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;

        iTween.ValueTo(gameObject, 
            iTween.Hash(
            "from", 0f, 
            "to", 1f, 
            "time", .5f,
            "onupdate", "ChangeAlpha"
            ));
    }

    public void FadeOut()
    {
        active = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;

        iTween.ValueTo(gameObject,
            iTween.Hash(
            "from", 1f,
            "to", 0f,
            "time", .5f,
            "onupdate", "ChangeAlpha"
            ));
    }*/

    public virtual void FadeIn()
    {
        Active = true;
        if (canvasGroup == null)
            Debug.Log("?");
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
    }

    public virtual void FadeOut()
    {
        Active = false;
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
    }

    void ChangeAlpha(float alpha)
    {
        canvasGroup.alpha = alpha;
    }

    public void Toggle()
    {
        if (Active)
            FadeOut();
        else
            FadeIn();
    }
}
