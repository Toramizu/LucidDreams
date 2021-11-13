using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SubInteractionButton : InteractionButton, IPointerEnterHandler, IPointerExitHandler
{
    MainInteractionButton mainInteraction;

    public void Init(InteractionData data, MainInteractionButton main)
    {
        mainInteraction = main;
        Init(data);
    }

    public void Rotate(Vector3 angle)
    {
        transform.localEulerAngles = angle;
        background.transform.eulerAngles = new Vector3();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(mainInteraction != null)
            mainInteraction.Text = data.Text;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (mainInteraction != null)
            mainInteraction.Text = null;
    }
}
