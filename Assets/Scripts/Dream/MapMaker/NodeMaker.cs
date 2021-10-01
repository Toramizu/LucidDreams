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

    private void Awake()
    {
        rTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        if (held && Input.GetKeyDown(KeyCode.Delete))
            Delete();
    }

    void Delete()
    {

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
    }
}
