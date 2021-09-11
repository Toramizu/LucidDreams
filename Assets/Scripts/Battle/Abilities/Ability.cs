using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ability : MonoBehaviour
{
    [SerializeField] TMP_Text title;
    [SerializeField] TMP_Text description;

    [SerializeField] List<DieSlot> Dice;

    [SerializeField] TMP_Text countText;
    int? count;
    public int? Count {
        get {return count; }
        set { count = value;
            if(count == null)
                countText.text = "";
            else if (count <= 0)
                countText.text = "0";
            else
                countText.text = value.ToString();
        }
    }

    public void Init(AbilityData data)
    {
        Debug.Log("Init ability...");
        title.text = data.Title;
        description.text = data.Description;

        for (int i = 0; i < Dice.Count; i++)
        {
            if (i < data.Conditions.Count)
            {
                Dice[i].gameObject.SetActive(true);
                Dice[i].SetCondition(data.Conditions[i].ToCondition());
            }
            else
                Dice[i].gameObject.SetActive(false);
        }

        if (data.Total <= 0)
            Count = null;
        else
            Count = data.Total;
    }
}
