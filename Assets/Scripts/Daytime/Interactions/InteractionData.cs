using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class InteractionData : ScriptableObject
{
    [SerializeField] string id;
    public string ID { get { return id; } }
    [SerializeField] Vector2 position;
    public Vector2 Position { get { return position; } }
    [SerializeField] string text;
    public string Text { get { return text; } }

    [SerializeField] Condition condition;

    public bool Check { get { return condition == null || condition.Check; } }

    [SerializeField] Color background;
    public Color Background { get { return background; } }
    [SerializeField] protected Sprite icon;
    public virtual Sprite Icon { get { return icon; } }

    public abstract void OnClick();
}
