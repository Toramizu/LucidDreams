using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MiniAbility : Window, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    [SerializeField] TMP_Text text;

    [SerializeField] Image background;
    [SerializeField] float alpha;
    [SerializeField] Canvas canvas;
    
    RectTransform rTransform;

    AbilityData ability;
    public AbilityData Ability
    {
        get { return ability; }
        set
        {
            EquipAbility(value);
        }
    }

    Vector3 defPos;
    public Vector3 DefaultPosition
    {
        get { return defPos; }
        set
        {
            defPos = value;
            ResetPos();
        }
    }

    public MiniAbilityUI AbiUI { get; set; }
    public int EquipSlot { get; set; } = -1;

    protected override void Awake()
    {
        base.Awake();
        rTransform = GetComponent<RectTransform>();
    }

    void ResetPos()
    {
        transform.localPosition = defPos;
    }

    void EquipAbility(AbilityData abi)
    {
        ability = abi;
        if(abi == null)
        {
            text.text = "";
            Color c = background.color;
            c.a = 0f;
            background.color = c;
        }
        else
        {
            text.text = abi.Title;
            Color c = background.color;
            c.a = alpha;
            background.color = c;
        }
    }

    #region Drag & Drop

    public void OnPointerClick(PointerEventData eventData)
    {
        AbiUI.DisplayAbility(ability);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(Ability != null)
            rTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        ResetPos();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            MiniAbility abi = eventData.pointerDrag.GetComponent<MiniAbility>();

            if (abi != null && abi.Ability != null && abi != this)
            {
                AbilityData tmp = Ability;
                Ability = abi.Ability;
                abi.Ability = tmp;
            }
        }
    }
    #endregion
}