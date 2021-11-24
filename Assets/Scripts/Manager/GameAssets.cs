using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    [SerializeField] List<Trait> traits;
    public List<Trait> Traits { get { return traits; } }
}
