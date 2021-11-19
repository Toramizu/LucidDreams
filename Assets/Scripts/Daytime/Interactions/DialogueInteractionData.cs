using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static DialogueUI;

[CreateAssetMenu(fileName = "NewDialogueInteraction", menuName = "Data/Interaction/Dialogue")]
public class DialogueInteractionData : InteractionData
{
    [SerializeField] List<InteractionDialogue> events;

    public override void OnClick()
    {
        List<InteractionDialogue> evnts = events.Where(e => e.Check).ToList();

        evnts[Random.Range(0, evnts.Count)].Play(AdvanceTime);
    }
}

[System.Serializable]
public class InteractionDialogue
{
    [SerializeField] Condition condition;
    public Condition Condition { get { return condition; } }

    public bool Check { get { return condition == null || condition.Check; } }

    [SerializeField] DialogueData dialogue;
    public DialogueData Dialogue { get { return dialogue; } }

    public void Play(DialogueAction action)
    {
        GameManager.Instance.StartDialogue(dialogue, action);
    }
}
