using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class InteractionButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] protected Image background;
    [SerializeField] Image icon;

    protected InteractionData data;

    public virtual void Init(InteractionData data)
    {
        this.data = data;

        if (data != null)
        {
            background.color = data.Background;
            icon.sprite = data.Icon;
        }
    }

    public void OnClick()
    {
        data.OnClick();
    }

    public abstract void OnPointerEnter(PointerEventData eventData);

    public abstract void OnPointerExit(PointerEventData eventData);
}
