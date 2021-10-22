using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSoruce;
    [SerializeField] List<AudioClip> diceRolls;
    [SerializeField] AudioClip placedDie;

    public float Volume
    {
        get { return audioSoruce.volume * 100f; }
        set { audioSoruce.volume = value / 100f; }
    }

    public void TestSound()
    {
        audioSoruce.clip = diceRolls[0];
        audioSoruce.Play();
    }

    public void DiceRoll()
    {
        audioSoruce.clip = diceRolls[Random.Range(0, diceRolls.Count)];
        audioSoruce.Play();
    }

    public void PlaceDice()
    {
        audioSoruce.clip = placedDie;
        audioSoruce.Play();
    }
}
