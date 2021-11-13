using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayManager : Window
{
    [SerializeField] Clock clock;
    [SerializeField] MainInteractionButton interactPrefab;

    [SerializeField] List<InteractionData> mainInteractions;

    Dictionary<string, MainInteractionButton> interactions = new Dictionary<string, MainInteractionButton>();

    public override void Open()
    {
        base.Open();

        foreach (InteractionData inter in mainInteractions)
            if (inter.Check)
                AddInteraction(null, inter);
    }

    public void AddInteraction(string main, InteractionData data)
    {
        if (!data.Check)
            return;

        if(main == null || !interactions.ContainsKey(main))
        {
            MainInteractionButton inter;
            if (interactions.ContainsKey(data.ID))
            {
                inter = interactions[data.ID];
            }
            else
            {
                inter = Instantiate(interactPrefab, transform, false);
                interactions.Add(data.ID, inter);
            }
            inter.Init(data);
        }
        else
        {
            interactions[main].AddSub(data);
        }
    }
}
