using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayManager : Window
{
    [SerializeField] Clock clock;
    [SerializeField] MainInteractionButton interactPrefab;

    Dictionary<string, MainInteractionButton> interactions = new Dictionary<string, MainInteractionButton>();

    public void AddInteraction(string main, InteractionData data)
    {
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
