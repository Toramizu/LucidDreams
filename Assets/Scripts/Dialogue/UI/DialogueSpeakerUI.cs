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
        }
    }


    [SerializeField] TMP_Text text;
    public string Text
    {
        get { return text.text; }
        set
        {
            if (value == null)
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

    CharacterData cData;
    public CharacterData Data
    {
        get { return cData; }
        set
        {
            cData = value;
            Text = cData.SName;
            Init(cData.Image);
        }
    }

    public override void Toggle(bool toggle)
    {
        base.Toggle(toggle);
        gameObject.SetActive(toggle);
    }
}
