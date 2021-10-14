using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RolledDie// : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public RolledDieUI DieUI { get; set; }
    const int D_MAX = 6;

    /*[SerializeField] Image image;
    [SerializeField] List<Sprite> dieFaces;
    [SerializeField] Sprite lockSprite;

    [SerializeField] Canvas canvas;

    RectTransform rTransform;
    CanvasGroup canvasGroup;*/

    public GameObject GameObject { get { return DieUI.gameObject; } }

    public DieSlot CurrentSlot { get; set; }
    /*Vector3 position;
    public Vector3 Position
    {
        get { return position; }
        set
        {
            position = value;
            transform.localPosition = value;
        }
    }*/

    [SerializeField] int value;
    public int Value
    {
        get { return value; }
        set
        {
            this.value = value;
            ParseValue();
        }
    }

    bool locked;
    public bool Locked
    {
        get { return locked; }
        set {
            locked = value;
            ParseValue();
        }
    }

    public int ID { get; set; }

    public RolledDie(int value, int id)
    {
        this.value = value;
        ID = id;
    }

    public void Add(int amount)
    {
        value += amount;
        ParseValue();
    }

    void ParseValue()
    {
        if (locked)
            DieUI.Lock();
        //image.sprite = lockSprite;
        else if (value > D_MAX)
            SplitDie();
        else if (value <= 0)
            EmptyDie();
        else
            DieUI.ShowFace(value);
            //image.sprite = dieFaces[value];
    }

    void SplitDie()
    {
        GameManager.Instance.BattleManager.Give(value - D_MAX + 1);
        Value = D_MAX - 1;
    }


    void EmptyDie()
    {
        value = 0;
        DieUI.ShowFace(0);
    }

    public void SlotDie(DieSlotUI slot)
    {
        DieUI.transform.position = slot.Pos;
    }

    public void ResetPosition()
    {
        DieUI.ResetPosition();
        //transform.localPosition = position;
    }

    public void Hide()
    {
        DieUI.gameObject.SetActive(false);
    }

    public RolledDie Clone()
    {
        return new RolledDie(value, ID);
    }
}
