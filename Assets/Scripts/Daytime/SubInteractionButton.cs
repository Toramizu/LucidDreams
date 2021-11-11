using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SubInteractionButton : InteractionButton
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
        background.transform.localEulerAngles = new Vector3();
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        if(mainInteraction != null)
            mainInteraction.Text = data.Text;
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        if (mainInteraction != null)
            mainInteraction.Text = null;
    }
}
