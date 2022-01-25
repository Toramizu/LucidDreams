using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSpeakerUI : ImageUI
{
    [SerializeField] Image textBackground;

    public bool Focus
    {
        set
        {
            Color c = textBackground.color;

            if (value)
                c.a = 1f;
            else
                c.a = .5f;

            textBackground.color = c;
            image.color = c;

            c = text.color;

            if (value)
                c.a = 1f;
            else
                c.a = .5f;

            text.color = c;

            if (true)
                image.transform.SetSiblingIndex(0);
        }
    }


    [SerializeField] TMP_Text text;
    public string Text
    {
        get { return text.text; }
        set
        {
            if (value == null || value == "")
            {
                Toggle(false);
            }
            else
            {
                Toggle(true);
                text.text = value;
            }
        }
    }

    ImageHaver cData;
    public ImageHaver Data
    {
        get { return cData; }
        set
        {
            cData = value;
            if (value == null)
            {
                Toggle(false);
            }
            else
            {
                Toggle(true);
                Text = cData.Name;
                Debug.Log(cData.Name + " => " + cData.Images);
                Init(cData.Image);
            }
        }
    }

    public override void Toggle(bool toggle)
    {
        base.Toggle(toggle);
        gameObject.SetActive(toggle);
    }

    public void SetSpeaker(DialogueSpeaker speaker)
    {
        if (speaker == null || (speaker.Speaker == null && speaker.Displayed == null && speaker.ImageID == null))
            Toggle(false);

        if (speaker.Speaker != null && speaker.Speaker != "")
        {
            cData = AssetDB.Instance.CharacterDatas.GetNullableAsset(speaker.Speaker);
            if(cData == null)
                cData = AssetDB.Instance.Succubi.GetNullableAsset(speaker.Speaker);

            if(cData == null)
                Text = GameManager.Instance.Parser.Parse(speaker.Speaker);
            else if (speaker.Displayed == null)
                Text = GameManager.Instance.Parser.Parse(cData.Name);
            else
                Text = GameManager.Instance.Parser.Parse(speaker.Displayed);

            if (cData == null)
                Init(null);
            else
                Init(cData.Image);
        }

        if(speaker.ImageID == "")
            base.Toggle(false);
        else if(cData != null && speaker.ImageID != null)
            Init(cData.Images[speaker.ImageID]);
    }
}
