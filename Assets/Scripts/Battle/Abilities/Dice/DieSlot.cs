using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DieSlot : MonoBehaviour, IDie//IDropHandler
{
    [SerializeField] Image image;
    [SerializeField] List<Sprite> faces;
    [SerializeField] TMP_Text text;
    [SerializeField] GameObject links;

    RolledDie slottedDie;
    public RolledDie SlottedDie {
        get { return slottedDie; }
        set
        {
            slottedDie = value;

            if(condition.Linked != null)
            {
                if(slottedDie == null)
                {
                    condition.Linked.Count--;
                    if(condition.Linked.Count <= 0)
                        condition.Linked.Value = 0;
                }
                else
                {
                    condition.Linked.Value = slottedDie.Value;
                    condition.Linked.Count++;
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

    public Ability Ability { get; set; }

    DiceCondition condition;
    public DiceCondition Condition {
        get { return condition; }
        set { SetCondition(value); }
    }
    public bool IsActive { get { return isActiveAndEnabled; } }
    public float X { get { return transform.position.x; } }
    public float Y { get { return transform.position.y; } }

    public LinkedValue Linked
    {
        get { return condition.Linked; }
        set
        {
            condition.Linked = value;
            links.SetActive(value != null);
        }
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

    public bool Check(int die)
    {
        return condition.Check(die);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null && GameManager.Instance.BattleManager.PlayerTurn)
        {
            RolledDie die = eventData.pointerDrag.GetComponent<RolledDie>();
            if(!die.Locked)
                OnDrop(die);
        }
    }

    public void OnDrop(RolledDie die)
    {
        if (die != null && condition.Check(die.Value))
        {
            SlottedDie = die;
            die.CurrentSlot = this;
            die.transform.position = transform.position;
            Ability.Check();
        }
    }
}