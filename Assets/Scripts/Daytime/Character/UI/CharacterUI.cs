using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : Window
{
    [SerializeField] CharacterToken tokenPrefab;
    [SerializeField] LayoutGroup tokenList;

    List<CharacterToken> tokens = new List<CharacterToken>();
    List<CharacterToken> displayedTokens = new List<CharacterToken>();

    Character lastChara;

    [SerializeField] int tokenDisplayed;
    int tokenPos;
    [SerializeField] Button next;
    [SerializeField] Button previous;

    [SerializeField] CharacterDisplayUI charaDisplay;
    [SerializeField] RelationshipUI relationUI;

    public override void Open()
    {
        FadeIn();
        Clear();
        
        foreach (Character chara in AssetDB.Instance.Characters.ToList())
        {
            if (chara.Check)
            {
                CharacterToken token = Instantiate(tokenPrefab, tokenList.transform, false);
                tokens.Add(token);
                token.Init(chara, this);
            }
        }

        if (lastChara == null || !lastChara.Check)
            ShowCharacter(null);
        else
            ShowCharacter(lastChara);

        ShowTokens();
    }

    public void DenbugOpen(List<Character> debugCharacters)
    {
        FadeIn();
        Clear();

        foreach (Character chara in debugCharacters)
        {
            if (chara.Check)
            {
                CharacterToken token = Instantiate(tokenPrefab, tokenList.transform, false);
                tokens.Add(token);
                token.Init(chara, this);
            }
        }

        if (lastChara == null || !lastChara.Check)
            ShowCharacter(null);
        else
            ShowCharacter(lastChara);

        ShowTokens();
    }

    void Clear()
    {
        foreach (CharacterToken token in tokens)
            Destroy(token.gameObject);

        tokens.Clear();
    }

    public void ShowCharacter(Character chara)
    {
        if(chara == null || !chara.Check)
        {
            if (tokens.Count == 0)
                return;
            else
                chara = tokens[0].Character;
        }

        lastChara = chara;
        charaDisplay.Display(chara);
        relationUI.Display(chara);
    }

    void ShowTokens()
    {
        foreach (CharacterToken displayed in displayedTokens)
            displayed.Toggle(false);
        displayedTokens.Clear();

        if(tokens.Count > tokenDisplayed)
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
        if(i < 0)
        {
            tokenPos = (tokens.Count - 1) / tokenDisplayed;
            i = tokenPos * tokenDisplayed;
        }
        if(i > tokens.Count)
        {
            tokenPos = 0;
            i = 0;
        }

        for(int j = 0; j < tokenDisplayed && i < tokens.Count; i++, j++)
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
}

[System.Serializable]
public class CharacterDisplayUI
{
    [SerializeField] TMP_Text charaName;
    [SerializeField] Image charaImage;
    [SerializeField] TMP_Text charaDescr;

    public void Display(Character chara)
    {
        Debug.Log(chara.Data.Name);

        charaImage.sprite = chara.Data.Image.Image;
        charaName.text = chara.Data.Name;
        charaDescr.text = chara.Data.Background;
    }
}

[System.Serializable]
public class RelationshipUI
{
    [SerializeField] Image relationImagePrefab;
    [SerializeField] Sprite emptySprite;
    [SerializeField] Sprite defaultSprite;
    [SerializeField] Color defaultColor;
    [SerializeField] LayoutGroup spriteParent;
    [SerializeField] TMP_Text relationName;
    [SerializeField] TMP_Text relationDescr;

    List<Image> images = new List<Image>();

    [SerializeField] MiniRelation miniRelationPrefab;
    [SerializeField] LayoutGroup miniParent;

    List<MiniRelation> minis = new List<MiniRelation>();

    Color color;

    public void Display(Character chara)
    {
        if (chara.Data.Color == null)
            color = Color.white;
        else
            color = chara.Data.Color.Color;
        Relationship main = null;
        List<Relationship> others = new List<Relationship>();

        foreach(Relationship rela in chara.Relationships)
        {
            if (rela.Stage > 0)
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
        if (images.Count < relation.MaxStage)
            CreateImages(relation.MaxStage);

        int i = 0;
        Sprite s = relation.Data.Icon;
        for (; i < relation.Stage; i++)
        {
            Image img = images[i];
            images[i].gameObject.SetActive(true);
            if (s != null)
                img.sprite = s;
            else
                img.sprite = defaultSprite;

            if (relation.Data.AutoColor)
                img.color = color;
            else
                img.color = Color.white;
        }

        for (; i < relation.MaxStage; i++)
        {
            Image img = images[i];
            img.sprite = emptySprite;

            if (relation.Data.AutoColor)
                img.color = color;
            else
                img.color = Color.white;

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
            minis[i].gameObject.SetActive(true);

            if (relations[i].Data.Icon == null)
                minis[i].Sprite = defaultSprite;
            else
                minis[i].Sprite = relations[i].Data.Icon;

            minis[i].Stage = relations[i].Stage;
            if (relations[i].Data.AutoColor)
                minis[i].Color = color;
            else
                minis[i].Color = Color.white;
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
}
