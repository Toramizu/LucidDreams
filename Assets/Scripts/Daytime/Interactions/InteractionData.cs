using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public /*abstract*/ class InteractionData// : ScriptableObject
{
    [SerializeField] string id;
    public string ID { get { return id; }  set { id = value; } }
    [SerializeField] Vector2 position;
    public Vector2 Position { get { return position; } }
    [SerializeField] string text;
    public string Text { get { return text; } set { text = value; } }

    [SerializeField] Color background;
    public Color Background { get { return background; } }
    [SerializeField] Sprite icon;
    public Sprite Icon { get { return icon; } set { icon = value; } }

    public void OnClick()
    {
        Debug.Log(id);
    }
}
