using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainInteractionButton : InteractionButton
{
    [SerializeField] TMP_Text text;
    public string Text
    {
        get { return text.text; }
        set
        {
            if(value == null)
                text.text = data.Name;
            else
                text.text = value;

            Canvas.ForceUpdateCanvases();
            layoutGroup.enabled = false;
            layoutGroup.enabled = true;
        }
    }

    [SerializeField] LayoutGroup layoutGroup;
    [SerializeField] SubInteractionButton subPrefab;
    List<SubInteractionButton> subs = new List<SubInteractionButton>();
    [SerializeField] float iconGap;

    public override void Init(InteractionEvent data)
    {
        base.Init(data);

        if (data != null)
            transform.localPosition = data.Position;

        Text = null;
    }

    public void AddSub(InteractionEvent data)
    {
        SubInteractionButton s = Instantiate(subPrefab, transform, false);
        subs.Add(s);
        s.Init(data, this);

        float current = iconGap * (subs.Count - 1) / 2;
        /*float current;
        if (subs.Count % 2 == 0)
            current = iconGap * (subs.Count - .5f);
        else
            current = iconGap * (subs.Count - 1f);*/

        foreach(SubInteractionButton sub in subs)
        {
            sub.Rotate(new Vector3(0, 0, current));
            current -= iconGap;
        }
    }
}
