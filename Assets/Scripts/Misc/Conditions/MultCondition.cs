using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using UnityEngine;

public class MultCondition : Condition
{
    [XmlAttribute("Count"), DefaultValue(0)]
    public int Count { get; set; }
    //[XmlElement("Condtion")]
    [XmlElement("IfMult", typeof(MultCondition))]
    [XmlElement("IfFlag", typeof(FlagCondition))]
    [XmlElement("IfChara", typeof(CharaCondition))]
    [XmlElement("IfString", typeof(StringCondition))]
    [XmlElement("IfTime", typeof(TimeCondition))]
    public List<Condition> Conditions { get; set; }

    public override bool Check
    {
        get
        {
            if (Count <= 0)
            {

                foreach (Condition c in Conditions)
                    if (!c.Check)
                        return false;
                return true;
            }
            else
            {
                int i = 0;
                foreach(Condition c in Conditions)
                    if (c.Check)
                    {
                        if (++i >= Count)
                            return true;
                    }
                return false;
            }
        }
    }
}
