using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LockSlot : IDie// MonoBehaviour, IDropHandler
{
    Ability ability;
    DieSlotUI SlotUI;

    public LockSlot(Ability ability)
    {
        this.ability = ability;
        if (ability.AbiUI != null)
        {
            SlotUI = ability.AbiUI.LockSlot;
            SlotUI.Init(this);
        }
    }

    /*public bool IsActive { get { return isActiveAndEnabled; } }
    public float X { get { return transform.position.x; } }
    public float Y { get { return transform.position.y; } }*/
    public bool IsActive
    {
        get
        {
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

    public bool Check(int die) { return true; }

    /*public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null && GameManager.Instance.BattleManager.PlayerTurn)
        {
            RolledDie die = eventData.pointerDrag.GetComponent<RolledDie>();
            if (!die.Locked)
                OnDrop(die);
        }
    }*/

    public void OnDrop(RolledDie die)
    {
        if (die != null)
        {
            ability.Locked = false;
            die.Hide();
                //.gameObject.SetActive(false);
        }
    }
}
