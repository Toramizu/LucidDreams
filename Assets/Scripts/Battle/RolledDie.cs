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
        value = dieFaces.Count - 1;
        //Add die
    }


    void EmptyDie()
    {
        value = 0;
        image.sprite = dieFaces[0];
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        if(CurrentSlot != null)
        {
            CurrentSlot.SlottedDie = null;
            CurrentSlot = null;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        rTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
    }

}
