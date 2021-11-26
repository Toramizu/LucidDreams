using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class InteractionButton : MonoBehaviour
{
    [SerializeField] protected Image background;
    [SerializeField] Image icon;

    [SerializeField] Image timeImage;

    protected InteractionEvent data;

    public virtual void Init(InteractionEvent data)
    {
        if (data != null)
        {
            this.data = data;

            background.color = data.BackgroundColor;
            icon.sprite = data.Icon;
            icon.color = data.IconColor;

            timeImage.gameObject.SetActive(data.TimeSpent > 0);
        }
        else
            Destroy(gameObject);
    }

    public void OnClick()
    {
        data.Play();
    }
}
delegate void GameAction();