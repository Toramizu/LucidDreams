using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RolledDie : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] Image image;
    [SerializeField] List<Sprite> dieFaces;

    [SerializeField] Canvas canvas;

    RectTransform rTransform;
    CanvasGroup canvasGroup;

    public DieSlot CurrentSlot { get; set; }
    Vector3 position;
    public Vector3 Position
    {
        get { return position; }
        set
        {
            position = value;
            transform.localPosition = value;
        }
    }

    int value;
    public int Value
    {
        get { return value; }
        set
        {
            this.value = value;
            ParseValue();
        }
    }

    private void Awake()
    {
        rTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Add(int amount)
    {
        value += amount;
        ParseValue();
    }

    void ParseValue()
    {
        if (value >= dieFaces.Count)
            SplitDie();
        else if (value <= 0)
            EmptyDie();
        else
            image.sprite = dieFaces[value];
    }

    void SplitDie()
    {
        GameManager.Instance.BattleManager.Give(value - dieFaces.Count + 1);
        Value = dieFaces.Count - 1;
    }


    void EmptyDie()
    {
        value = 0;
        image.sprite = dieFaces[0];
    }

    public void ResetPosition()
    {
        transform.localPosition = position;
    }

    #region Drag & Drop
    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (GameManager.Instance.BattleManager.PlayerTurn)
        {
            canvasGroup.blocksRaycasts = false;
            if (CurrentSlot != null)
            {
                CurrentSlot.SlottedDie = null;
                CurrentSlot = null;
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (GameManager.Instance.BattleManager.PlayerTurn)
            rTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (GameManager.Instance.BattleManager.PlayerTurn)
            canvasGroup.blocksRaycasts = true;
    }
    #endregion
}
