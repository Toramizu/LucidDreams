using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DieSlot :  IDie//IDropHandler MonoBehaviour,
{
    DieSlotUI SlotUI;
    /*[SerializeField] Image image;
    [SerializeField] List<Sprite> faces;
    [SerializeField] TMP_Text text;
    [SerializeField] GameObject links;*/

    RolledDie slottedDie;
    public RolledDie SlottedDie {
        get { return slottedDie; }
        set
        {
            slottedDie = value;

            if(condition.Link != null)
            {
                if(slottedDie == null)
                {
                    condition.Link.Count--;
                    if(condition.Link.Count <= 0)
                        condition.Link.Value = 0;
                }
                else
                {
                    condition.Link.Value = slottedDie.Value;
                    condition.Link.Count++;
                }
            }
        }
    }
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

    Ability ability;

    DiceCondition condition;
    public DiceCondition Condition {
        get { return condition; }
        set { SetCondition(value); }
    }
    public int[] AcceptedValues { get { return condition.AcceptedValues; } }

    public bool IsActive
    {
        get {
            return SlotUI && SlotUI.isActiveAndEnabled;
        }
    }
    public float X
    {
        get
        {
            if (SlotUI)
                return SlotUI.Pos.x;
            else
                return 0f;
        }
    }
    public float Y
    {
        get
        {
            if (SlotUI)
                return SlotUI.Pos.y;
            else
                return 0f;
        }
    }
    /*public bool IsActive { get { return isActiveAndEnabled; } }
    public float X { get { return transform.position.x; } }
    public float Y { get { return transform.position.y; } }*/

    public LinkedValue Link
    {
        get { return condition.Link; }
        set
        {
            condition.Link = value;
            //links.SetActive(value != null);
            SlotUI.ShowLinks(value != null);
        }
    }

    public DieSlot() { }
    public DieSlot(ConditionData condition, DieSlotUI slotUI, Ability ability)
    {
        this.ability = ability;
        SlotUI = slotUI;
        slotUI.Init(this);

        SetCondition(condition.ToCondition());
        Link = ability.Link;
    }

    public void SetCondition(DiceCondition cond)
    {
        condition = cond;
        SlotUI.SetCondition(cond);

        /*if (cond is EqualsDie)
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
        }*/
    }

    public bool Check(int die)
    {
        return condition.Check(die);
    }

    /*public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null && GameManager.Instance.BattleManager.PlayerTurn)
        {
            RolledDie die = eventData.pointerDrag.GetComponent<RolledDie>();
            if(!die.Locked)
                OnDrop(die);
        }
    }*/

    public void OnDrop(RolledDie die)
    {
        if (die != null && condition.Check(die.Value))
        {
            SlottedDie = die;
            die.CurrentSlot = this;
            //die.transform.position = SlotUI.Pos;
            ability.Check();
        }
    }
}