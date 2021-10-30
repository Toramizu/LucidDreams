using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestEvent : ScriptableObject
{
    [SerializeField] string id;
    public string ID { get { return id; } }
}
