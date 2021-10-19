using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TraitTooltip : Window
{
    [SerializeField] Image icon;
    [SerializeField] TMP_Text title;
    [SerializeField] TMP_Text description;

    public void Open(Trait trait)
    {
        Open();
        if (trait == null)
        {
            Close();
        }
        else
        {
            gameObject.SetActive(true);
            icon.sprite = trait.Icon;
            title.text = trait.ID;
            description.text = trait.Description;
        }
    }
}
