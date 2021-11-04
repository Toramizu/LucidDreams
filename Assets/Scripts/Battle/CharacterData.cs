using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSuccubus", menuName = "Data/Succubus", order = 1)]
public class CharacterData : ScriptableObject
{
    [SerializeField] protected string sName;
    public string SName
    {
        get { return sName; }
        set { sName = value; }
    }
    [SerializeField] protected Sprite image;
    /*public Sprite Image
    {
        get { return image; }
        set { image = value; }
    }*/
    [SerializeField] protected string source;
    /*public string Source
    {
        get { return source; }
        set { source = value; }
    }*/

    [SerializeField] ImageSet images;
    public ImageSet Images { get { return images; } }
    public ImageData Image { get { return images.Default; } }

    [SerializeField] List<AbilityData> abilities;
    public List<AbilityData> Abilities { get { return abilities; } set { abilities = value; } }

    /*[SerializeField] int rolls = 3;
    public int Rolls { get { return rolls; } set { rolls = value; } }*/

    /*[SerializeField] List<CharacterLevel> levels;
    public List<CharacterLevel> Level { get{ return levels; } }*/

    [SerializeField] int dice;
    public int Dice { get { return dice; } }
    [SerializeField] int maxArousal;
    public int MaxArousal { get { return maxArousal; } }

    [SerializeField] int crystals;
    public int Crystals { get { return crystals; } }
}
