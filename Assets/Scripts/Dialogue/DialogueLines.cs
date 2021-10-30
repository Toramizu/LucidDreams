using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLines", menuName = "Data/Dialogue/Lines")]
public class DialogueLines : DialogueElement
{
    [SerializeField] List<DialogueLine> lines;
    public List<DialogueLine> Lines { get { return lines; } }

    public override void Play(DialogueUI dialUI)
    {
        dialUI.Play(lines);
    }
}

[System.Serializable]
public class DialogueLine
{
    [SerializeField] string line;
    public string Line { get { return line; } }
    [SerializeField] string speaker;
    public string Speaker { get { return speaker; } }
    [SerializeField] SpeakerPos speakerPosition;
    public SpeakerPos SpeakerPosition { get { return speakerPosition; } }
    [SerializeField] bool removeSpeaker;
    public bool RemoveSpeaker { get { return removeSpeaker; } }
}

public enum SpeakerPos
{
    None,
    Left,
    Right
}