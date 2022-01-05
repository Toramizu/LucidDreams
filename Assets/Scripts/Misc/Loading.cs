using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loading : Window
{
    [SerializeField] Image succubus;

    public override void FadeIn()
    {
        if (AssetDB.IsLoaded)
        {
            List<Sprite> s = AssetDB.Instance.Images.ToList();
            succubus.sprite = s[Random.Range(0, s.Count)];
        }
        base.FadeIn();
    }
}
