using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DayManager : Window
{
    [SerializeField] Clock clock;
    public int Time { get { return clock.Time; } }
    [SerializeField] TMP_Text dayText;

    [SerializeField] Image backgroundImage;
    [SerializeField] List<Sprite> backgrounds;

    [SerializeField] Transform tokenParent;
    [SerializeField] MainInteractionButton interactPrefab;
    [SerializeField] MainInteractionButton mainInteractPrefab;
    [SerializeField] SubInteractionButton subInteractPrefab;
    
    Dictionary<string, MainInteractionButton> interactions = new Dictionary<string, MainInteractionButton>();

    public void Open()
    {
        FillLocations();
        backgroundImage.sprite = backgrounds[0];
        FadeIn();
        DailyChecks();
    }

    public void FillLocations()
    {
        Clear();
        AddLocations();
        AddCharacters();
    }

    void AddLocations()
    {
        Dictionary<string, List<LocationData>> notPlaced = new Dictionary<string, List<LocationData>>();
        foreach (LocationData location in AssetDB.Instance.Locations.ToList())
            if (location.Check)
            {
                if (location.Parent == null)
                {
                    AddLocation(null, location);
                    if (notPlaced.ContainsKey(location.ID))
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

    void AddCharacters()
    {
        List<Character> charas = AssetDB.Instance.Characters.ToList().Where(c=> c.Check).ToList();
        int time = clock.Time;

        foreach (Character chara in charas)
        {
            string location = null;
            foreach (CharacterLocation loc in chara.Locations)
                if (loc.Time == time)
                    location = loc.Locations[Random.Range(0, loc.Locations.Count)];

            if (location != null && interactions.ContainsKey(location))
                AddCharacter(location, chara);
        }
    }

    void AddCharacter(string location, Character chara)
    {
        CharacterEvent cEvent = new CharacterEvent(chara);
        interactions[location].AddSub(cEvent);
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
                inter = Instantiate(interactPrefab, tokenParent, false);
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
        bool newDay = clock.AdvanceTime(amount);
        int t = clock.Time;
        if (t < clock.NightTime)
        {
            backgroundImage.sprite = backgrounds[t];
            FillLocations();
        }
        else
        {
            GameManager.Instance.StartNightTime();
        }

        if (newDay) DailyChecks();
    }

    public void NewDay()
    {
        int t = clock.NewDay();
        backgroundImage.sprite = backgrounds[t];
        FillLocations();
        DailyChecks();
    }

    void DailyChecks()
    {
        //Do relationship dacays
        Debug.Log("It's a new day!");
        dayText.text = clock.Date;
    }
}