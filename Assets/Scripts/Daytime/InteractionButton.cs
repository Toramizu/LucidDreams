using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class InteractionButton : MonoBehaviour
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
            icon.color = data.IconColor;
        }
    }

    public void OnClick()
    {
        data.OnClick();
    }
}
