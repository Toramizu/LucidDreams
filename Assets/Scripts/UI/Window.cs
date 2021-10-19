using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{
    protected CanvasGroup canvasGroup;
    bool active;

    protected virtual void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        active = false;
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
    }

    public virtual void Open()
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

    public void Toggle()
    {
        if (active)
            Close();
        else
            Open();
    }
}
