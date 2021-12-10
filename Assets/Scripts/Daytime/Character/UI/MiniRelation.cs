using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MiniRelation : MonoBehaviour
{
    [SerializeField] Image image;
    public Sprite Sprite { set { image.sprite = value; } }
    public Color Color { set { image.color = value; } }

    [SerializeField] Sprite defaultSprite;

    [SerializeField] TMP_Text text;
    public int Stage { set { text.text = value.ToString(); } }

    Relationship relation;

    public void Init(Relationship relation, Color color)
    {
        this.relation = relation;

        gameObject.SetActive(true);

        if (relation.Data.Icon == null)
            Sprite = defaultSprite;
        else
            Sprite = relation.Data.Icon;

        Stage = relation.Stage;
        if (relation.Data.Color == null)
            Color = color;
        else
            Color = relation.Data.Color.Color;
    }

    public void Debug_ShowRelationPoints()
    {
        if (Variables.debugMode)
            GameManager.Instance.Notify(relation.Name + " => " + relation.Points + "/" + relation.Stage * 100);
    }
}
