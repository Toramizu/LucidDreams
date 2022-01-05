using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class RandCondition : Condition
{
    [XmlAttribute("Chance")]
    public float Chance { get; set; }

    public override bool Check {
        get
        {
            float r = Random.Range(0f, 100f);

            if (Variables.debugMode)
                GameManager.Instance.Notify(r + "/" + Chance);

            return r <= Chance;
        }
    }
}
