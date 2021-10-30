using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogue", menuName = "Data/Dialogue/Dialogue")]
public class DialogueData : ScriptableObject
{
    [SerializeField] string id;
    public string ID { get { return id; } }

    [SerializeField] List<DialogueElement> elements;
    public List<DialogueElement> Elements { get { return elements; } }
}