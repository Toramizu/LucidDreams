using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DieSlotUI : MonoBehaviour, IDropHandler
{
    [SerializeField] Image image;
    [SerializeField] List<Sprite> faces;
    [SerializeField] TMP_Text text;
    [SerializeField] GameObject links;

    IDie slot;

    /*RolledDie slottedDie;
    public RolledDie SlottedDie
    {
        get { return slottedDie; }
        set
        {
            slottedDie = value;

            if (condition.Linked != null)
            {
                if (slottedDie == null)
                {
                    condition.Linked.Count--;
                    if (condition.Linked.Count <= 0)
                        condition.Linked.Value = 0;
                }
                else
                {
                    condition.Linked.Value = slottedDie.Value;
                    condition.Linked.Count++;
                }
            }
        }
    }*/

    public bool IsActive { get { return isActiveAndEnabled; } }
    public Vector3 Pos { get { return transform.position; } }

    public void Init(IDie slot)
    {
        gameObject.SetActive(true);
        this.slot = slot;
        //SetCondition(slot.Condition);
    }

    public void SetCondition(DiceCondition cond)
    {
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

    public void ShowLinks(bool toggle)
    {

    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null && GameManager.Instance.BattleManager.PlayerTurn)
        {
            RolledDieUI die = eventData.pointerDrag.GetComponent<RolledDieUI>();
            if (!die.Locked)
                slot.OnDrop(die.Die);
            GameManager.Instance.BattleManager.CheckBattleStatus();
        }
    }
}
