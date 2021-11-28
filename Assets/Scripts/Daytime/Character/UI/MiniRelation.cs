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

    [SerializeField] TMP_Text text;
    public int Stage { set { text.text = value.ToString(); } }
}
