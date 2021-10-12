using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LockSlot : MonoBehaviour, IDie//IDropHandler
{
    [SerializeField] Ability ability;

    public bool IsActive { get { return isActiveAndEnabled; } }
    public float X { get { return transform.position.x; } }
    public float Y { get { return transform.position.y; } }

    public bool Check(int die) { return true; }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null && GameManager.Instance.BattleManager.PlayerTurn)
        {
            RolledDie die = eventData.pointerDrag.GetComponent<RolledDie>();
            if (!die.Locked)
                OnDrop(die);
        }
    }

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
