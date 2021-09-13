using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DieSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] Image image;
    [SerializeField] List<Sprite> faces;
    [SerializeField] TMP_Text text;

    public RolledDie SlottedDie { get; set; }
    public bool Slotted { get { return SlottedDie != null; } }
    public int Value {
        get
        {
            if (SlottedDie == null)
                return 0;
            else
                return SlottedDie.Value;
        }
    }

    public Ability Ability { get; set; }

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

    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null )
        {
            RolledDie die = eventData.pointerDrag.GetComponent<RolledDie>();

            if(die != null && condition.Check(die.Value))
            {
                SlottedDie = die;
                die.CurrentSlot = this;
                die.transform.position = transform.position;
                Ability.Check();
            }
        }
    }
}
