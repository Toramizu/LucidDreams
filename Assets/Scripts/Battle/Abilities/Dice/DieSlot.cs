using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DieSlot : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] List<Sprite> faces;
    [SerializeField] TMP_Text text;

    DiceCondition condition;
    public DiceCondition Condition {
        get { return condition; }
        set { SetCondition(value); }
    }

    public void SetCondition(DiceCondition cond)
    {
        condition = cond;

        if (cond is EqualsDie)
        {
            int val = ((EqualsDie)cond).Value;
            if (val <= faces.Count)
            {
                image.sprite = faces[val];
                text.text = "";
            }
            else
            {
                image.sprite = faces[0];
                text.text = cond.ConditionText();
            }

        }
        else
        {
            image.sprite = faces[0];
            text.text = cond.ConditionText();
        }
    }
}
