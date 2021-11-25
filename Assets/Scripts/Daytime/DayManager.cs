using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayManager : Window
{
    [SerializeField] Clock clock;
    public int Time { get { return clock.Time; } }

    [SerializeField] Image backgroundImage;
    [SerializeField] List<Sprite> backgrounds;

    [SerializeField] MainInteractionButton interactPrefab;
    [SerializeField] MainInteractionButton mainInteractPrefab;
    [SerializeField] SubInteractionButton subInteractPrefab;

    [SerializeField] List<DayTimeSlot> mainInteractions;

    Dictionary<string, MainInteractionButton> interactions = new Dictionary<string, MainInteractionButton>();

    public void Open(int time)
    {
        clock.Time = time;
        AddLocation();
        backgroundImage.sprite = backgrounds[0];
        FadeIn();
    }

    void AddLocation()
    {
        Clear();

        Dictionary<string, List<LocationData>> notPlaced = new Dictionary<string, List<LocationData>>();
        foreach (LocationData location in AssetDB.Instance.Locations.ToList())
            if (location.Check)
            {
                if (location.Parent == null)
                {
                    AddLocation(null, location);
                    foreach (LocationData loc in notPlaced[location.ID])
                        AddLocation(location.ID, loc);
                }
                else if (interactions.ContainsKey(location.Parent))
                {
                    AddLocation(location.Parent, location);
                }
                else
                {
                    if (!notPlaced.ContainsKey(location.Parent))
                        notPlaced.Add(location.Parent, new List<LocationData>());

                    notPlaced[location.Parent].Add(location);
                }

            }
    }

    public void AddLocation(string main, LocationData data)
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

    public void Clear()
    {
        foreach (MainInteractionButton inter in interactions.Values)
            Destroy(inter.gameObject);

        interactions.Clear();
    }

    public void AdvanceTime(int amount)
    {
        int t = clock.AdvanceTime(amount);
        backgroundImage.sprite = backgrounds[t];
        AddLocation();
    }
}

[System.Serializable]
public class DayTimeSlot
{
    [SerializeField] List<InteractionData> interactions;
    public List<InteractionData> Interactions { get { return interactions; } }
}