using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RevealAbilites : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        GameManager.Instance.BattleManager.ToggleOpponentAbilities(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GameManager.Instance.BattleManager.ToggleOpponentAbilities(false);
    }
}
