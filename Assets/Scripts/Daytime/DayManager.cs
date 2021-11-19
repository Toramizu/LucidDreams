using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayManager : Window
{
    [SerializeField] Clock clock;

    [SerializeField] Image backgroundImage;
    [SerializeField] List<Sprite> backgrounds;

    [SerializeField] MainInteractionButton interactPrefab;

    [SerializeField] List<DayTimeSlot> mainInteractions;

    Dictionary<string, MainInteractionButton> interactions = new Dictionary<string, MainInteractionButton>();

    public void Open(int time)
    {
        clock.Time = time;
        AddInteractions();
        backgroundImage.sprite = backgrounds[0];
        FadeIn();
    }

    void AddInteractions()
    {
        foreach(MainInteractionButton inter in interactions.Values)
            Destroy(inter.gameObject);

        interactions.Clear();

        foreach (InteractionData inter in mainInteractions[clock.Time].Interactions)
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

    public void AdvanceTime()
    {
        int t = clock.AdvanceTime();
        backgroundImage.sprite = backgrounds[t];
        AddInteractions();
    }
}

[System.Serializable]
public class DayTimeSlot
{
    [SerializeField] List<InteractionData> interactions;
    public List<InteractionData> Interactions { get { return interactions; } }
}