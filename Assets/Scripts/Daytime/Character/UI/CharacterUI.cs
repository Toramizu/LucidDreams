using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : Window, CharacterDisplayer
{
    [SerializeField] CharacterToken tokenPrefab;
    [SerializeField] LayoutGroup tokenList;

    Character lastChara;

    [SerializeField] CharacterListUI charaList;
    [SerializeField] CharacterDisplayUI charaDisplay;
    [SerializeField] RelationshipUI relationUI;

    public override void FadeIn()
    {
        base.FadeIn();
        charaList.Clear();

        List<Character> charas = AssetDB.Instance.Characters.ToList().Where(c => c.IsImportant && c.Check).ToList();
        charaList.Open(charas, this);

        if (lastChara == null || !lastChara.Check)
        {
            if(charas.Count == 0)
                FadeOut();
            else
                lastChara = charas[0];
        }

        DisplayCharacter(lastChara);
    }

    public void DenbugOpen(List<Character> debugCharacters)
    {
        FadeIn();
        charaList.Clear();

        charaList.Open(debugCharacters, this);

        if (lastChara == null || !lastChara.Check)
            DisplayCharacter(null);
        else
            DisplayCharacter(lastChara);
    }

    public void DisplayCharacter(Character chara)
    {
        if (chara == null || !chara.Check)
            return;

        lastChara = chara;
        charaDisplay.Display(chara);
        relationUI.Display(chara);
    }

    public void DisplayNext()
    {
        /*tokenPos++;
        ShowTokens();*/
        charaList.DisplayNext();
    }

    public void DisplayPrevious()
    {
        /*tokenPos--;
        ShowTokens();*/
        charaList.DisplayPrevious();
    }

    public void Debug_ShowRelationPoints()
    {
        if(Variables.debugMode)
            relationUI.Debug_ShowRelationPoints();
    }
}

public interface CharacterDisplayer
{
    void DisplayCharacter(Character chara);
    void DisplayNext();
    void DisplayPrevious();
    void FadeOut();
}

[System.Serializable]
public class CharacterListUI
{
    [SerializeField] CharacterToken tokenPrefab;
    [SerializeField] LayoutGroup tokenList;

    List<CharacterToken> tokens = new List<CharacterToken>();
    List<CharacterToken> displayedTokens = new List<CharacterToken>();

    [SerializeField] int tokenDisplayed;
    int tokenPos;
    [SerializeField] Button next;
    [SerializeField] Button previous;

    public void Open(List<Character> charas, CharacterDisplayer ui)
    {
        foreach (Character chara in charas)
        {
            CharacterToken token = MonoBehaviour.Instantiate(tokenPrefab, tokenList.transform, false);
            tokens.Add(token);
            token.Init(chara, ui);
        }
        ShowTokens();
    }

    public void ShowTokens()
    {
        foreach (CharacterToken displayed in displayedTokens)
            displayed.Toggle(false);
        displayedTokens.Clear();

        if (tokens.Count > tokenDisplayed)
        {
            next.gameObject.SetActive(true);
            previous.gameObject.SetActive(true);
        }
        else
        {
            next.gameObject.SetActive(false);
            previous.gameObject.SetActive(false);
        }

        int i = tokenPos * tokenDisplayed;
        if (i < 0)
        {
            tokenPos = (tokens.Count - 1) / tokenDisplayed;
            i = tokenPos * tokenDisplayed;
        }
        if (i > tokens.Count)
        {
            tokenPos = 0;
            i = 0;
        }

        for (int j = 0; j < tokenDisplayed && i < tokens.Count; i++, j++)
        {
            tokens[i].Toggle(true);
            displayedTokens.Add(tokens[i]);
        }
    }

    public void DisplayNext()
    {
        tokenPos++;
        ShowTokens();
    }

    public void DisplayPrevious()
    {
        tokenPos--;
        ShowTokens();
    }

    public void Clear()
    {
        foreach (CharacterToken token in tokens)
            MonoBehaviour.Destroy(token.gameObject);

        tokens.Clear();
    }
}

[System.Serializable]
public class CharacterDisplayUI
{
    [SerializeField] TMP_Text charaName;
    [SerializeField] TMP_Text charaDescr;
    [SerializeField] Image charaImage;
    [SerializeField] Image charaBackground;
    [SerializeField] float backgroundColorFading;

    public void Display(Character chara)
    {
        charaImage.sprite = chara.Data.Image.Image;
        charaName.text = chara.Data.Name;
        charaDescr.text = chara.Data.Background;

        if (chara.Data.Color != null)
        {
            Color color = chara.Data.Color;
            Color faded = new Color();
            faded.r = color.r + (1f - color.r) * backgroundColorFading;
            faded.g = color.g + (1f - color.g) * backgroundColorFading;
            faded.b = color.b + (1f - color.b) * backgroundColorFading;
            faded.a = 1;

            charaBackground.color = faded;
        }
        else
        {
            charaBackground.color = Color.white;
        }
    }
}

[System.Serializable]
public class RelationshipUI
{
    [SerializeField] Image relationImagePrefab;
    [SerializeField] Sprite emptySprite;
    [SerializeField] Sprite lostSprite;
    [SerializeField] Sprite defaultSprite;
    [SerializeField] Color defaultColor;
    [SerializeField] LayoutGroup spriteParent;
    [SerializeField] TMP_Text relationName;
    [SerializeField] TMP_Text relationDescr;

    List<Image> images = new List<Image>();

    [SerializeField] MiniRelation miniRelationPrefab;
    [SerializeField] LayoutGroup miniParent;

    Relationship mainRelation;
    List<MiniRelation> minis = new List<MiniRelation>();

    Color color;

    public void Display(Character chara)
    {
        if (chara.Relationships.Count == 0)
        {
            Clear();
            return;
        }

        if (chara.Data.Color == null)
            color = Color.white;
        else
            color = chara.Data.Color;
        Relationship main = null;
        List<Relationship> others = new List<Relationship>();


        foreach(Relationship rela in chara.Relationships)
        {
            if (rela.Stage > 0 || Variables.debugMode)
            {
                others.Add(rela);
                if (main == null || rela.Stage >= main.Stage)
                    main = rela;
            }
        }

        if (main == null)
            main = chara.Relationships[0];

        others.Remove(main);

        Init(main);
        InitMinis(others);
    }

    void Init(Relationship relation)
    {
        mainRelation = relation;
        if (images.Count < relation.MaxStage)
            CreateImages(relation.MaxStage);

        int i = 0;
        Sprite s = relation.Data.Icon;
        for (; i < relation.PointsInStage; i++)
        {
            Image img = images[i];
            images[i].gameObject.SetActive(true);
            if (s != null)
                img.sprite = s;
            else
                img.sprite = defaultSprite;

            if (relation.Data.Color == null)
                img.color = color;
            else
                img.color = relation.Data.Color.Color;
        }

        for (; i < relation.Stage; i++)
        {
            Image img = images[i];
            images[i].gameObject.SetActive(true);

            img.sprite = lostSprite;

            if (relation.Data.Color == null)
                img.color = color;
            else
                img.color = relation.Data.Color.Color;
        }

        for (; i < relation.MaxStage; i++)
        {
            Image img = images[i];
            img.sprite = emptySprite;

            if (relation.Data.Color == null)
                img.color = color;
            else
                img.color = relation.Data.Color.Color;

            images[i].gameObject.SetActive(true);
        }

        for (; i < images.Count; i++)
            images[i].gameObject.SetActive(false);

        relationName.text = relation.Data.RelationName;
        relationDescr.text = relation.Data.Description;
    }

    void CreateImages(int amount)
    {
        while (images.Count < amount)
        {
            Image img = MonoBehaviour.Instantiate(relationImagePrefab, spriteParent.transform, false);
            images.Add(img);
        }
    }

    void InitMinis(List<Relationship> relations)
    {
        if (minis.Count < relations.Count)
            CreateMinis(relations.Count);

        int i = 0;
        for(; i < relations.Count; i++)
        {
            minis[i].Init(relations[i], color);
            /*minis[i].gameObject.SetActive(true);

            if (relations[i].Data.Icon == null)
                minis[i].Sprite = defaultSprite;
            else
                minis[i].Sprite = relations[i].Data.Icon;

            minis[i].Stage = relations[i].Stage;
            if (relations[i].Data.AutoColor)
                minis[i].Color = color;
            else
                minis[i].Color = Color.white;*/
        }

        for (; i < minis.Count; i++)
            minis[i].gameObject.SetActive(false);
    }

    void CreateMinis(int amount)
    {
        while (minis.Count < amount)
        {
            MiniRelation mini = MonoBehaviour.Instantiate(miniRelationPrefab, miniParent.transform, false);
            minis.Add(mini);
        }
    }

    void Clear()
    {
        relationName.text = "";
        relationDescr.text = "";

        foreach (Image img in images)
            img.gameObject.SetActive(false);
        foreach (MiniRelation mini in minis)
            mini.gameObject.SetActive(false);
    }

    public void Debug_ShowRelationPoints()
    {
        GameManager.Instance.Notify(mainRelation.Name + " => " + mainRelation.Points + "/" + mainRelation.Stage * 100);
    }
}
