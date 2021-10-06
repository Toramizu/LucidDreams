using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NodeMaker : DreamNode, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] Canvas canvas;

    bool held;
    RectTransform rTransform;
    CanvasGroup canvasGroup;

    MapMaker maker { get { return (MapMaker)manager; } }

    public List<NodeLink> CellLinks { get; set; } = new List<NodeLink>();

    private void Awake()
    {
        rTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        if (held && Input.GetKeyDown(KeyCode.Delete))
            maker.Delete(this);
    }

    void Delete()
    {

    }

    public void ColorSelect()
    {
        image.color = Color.yellow;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        held = true;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        held = false;
        canvasGroup.blocksRaycasts = true;
        foreach (NodeLink link in CellLinks)
            link.SetAllDirty();
    }
}
