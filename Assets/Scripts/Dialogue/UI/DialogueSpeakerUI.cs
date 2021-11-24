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
            cData = AssetDB.Instance.CharacterDatas[speaker.Speaker];

            if (speaker.Displayed == null)
                Text = cData.Name;
            else
                Text = speaker.Displayed;

            Init(cData.Image);
        }

        if(speaker.ImageID == "")
            base.Toggle(false);
        else if(speaker.ImageID != null)
            Init(cData.Images[speaker.ImageID]);
    }
}
