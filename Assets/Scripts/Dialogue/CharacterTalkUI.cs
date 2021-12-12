using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterTalkUI : Window
{
    [SerializeField] TMP_Text title;
    [SerializeField] LayoutGroup layoutGroup;
    [SerializeField] Image charaImage;
    [SerializeField] List<Image> images;

    Character chara;

    public void Open(Character chara)
    {
        FadeIn();
        this.chara = chara;
        chara.LastInteraction = GameManager.Instance.DayManager.Day;

        title.text = chara.Data.Name;
        Canvas.ForceUpdateCanvases();
        layoutGroup.enabled = false;
        layoutGroup.enabled = true;

        charaImage.sprite = chara.Data.Image.Image;
        Color color = chara.Data.Color;
        foreach (Image img in images)
            img.color = color;
    }

    public void Talk()
    {
        chara.PlayDialogue(EndTalk);
        FadeOut();
    }

    public void Gift()
    {
        GameManager.Instance.Notify("Gift for " + chara.Data.Name);
    }

    public void Love()
    {
        GameManager.Instance.Notify("Loving " + chara.Data.Name);
    }

    public void Invite()
    {
        GameManager.Instance.Notify("Going out with " + chara.Data.Name);
    }

    public void EndTalk()
    {
        GameManager.Instance.DayManager.AdvanceTime(1);
    }
}
