using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class Character : MonoBehaviour
{
    [SerializeField] Image characterImage;
    [SerializeField] Sprite defaultImage;
    [SerializeField] TMP_Text characterName;
    [SerializeField] SimpleGauge gauge;
    [SerializeField] Button borrowed;

    protected int Arousal { get; set; }
    public bool Finished { get { return Arousal >= Data.MaxArousal; } }

    protected abstract CharacterData Data { get; }

    public void LoadCharacter(CharacterData data)
    {
        if (data.Image == null)
            characterImage.sprite = defaultImage;
        else
            characterImage.sprite = data.Image;

        characterName.text = data.Name;
        Arousal = 0;
        gauge.Fill(Arousal, data.MaxArousal);

        borrowed.gameObject.SetActive(data.Source != null && data.Source != "");
    }

    public virtual bool InflictDamage(int amount)
    {
        Arousal += amount;
        if (Arousal < 0)
            Arousal = 0;
        else if (Arousal > Data.MaxArousal)
            Arousal = Data.MaxArousal;

        gauge.Fill(Arousal, Data.MaxArousal);

        return Arousal >= Data.MaxArousal;
    }

    public void OpenBorrowed()
    {
        if (Data.Source != null && Data.Source != "")
            Application.OpenURL(Data.Source);
    }
}
