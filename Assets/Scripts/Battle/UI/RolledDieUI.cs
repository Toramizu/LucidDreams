using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RolledDieUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public RolledDie Die { get; set; }

    [SerializeField] Image image;
    [SerializeField] List<Sprite> dieFaces;
    [SerializeField] Sprite lockSprite;

    [SerializeField] Canvas canvas;
    RectTransform rTransform;
    CanvasGroup canvasGroup;

    public DieSlot CurrentSlot {
        get { return Die.CurrentSlot; }
        set { Die.CurrentSlot = value; }
    }

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

    public bool Locked { get { return Die.Locked; } }

    private void Awake()
    {
        rTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Init(RolledDie die)
    {
        this.Die = die;
        ShowFace(die.Value);
    }

    public void Lock()
    {
        image.sprite = lockSprite;
    }
    
    public void ShowFace(int value)
    {
        if (value < dieFaces.Count)
            image.sprite = dieFaces[value];
        else
            image.sprite = dieFaces[0];
    }

    public void ResetPosition()
    {
        transform.localPosition = position;
    }

    #region Drag & Drop
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (GameManager.Instance.BattleManager.PlayerTurn && !Locked)
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
        if (GameManager.Instance.BattleManager.PlayerTurn && !Locked)
            rTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (GameManager.Instance.BattleManager.PlayerTurn && !Locked)
            canvasGroup.blocksRaycasts = true;
    }
    #endregion
}
