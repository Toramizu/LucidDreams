using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flags
{
    Dictionary<string, int> flags = new Dictionary<string, int>();
    public Dictionary<string, string> Strings { get; set; } = new Dictionary<string, string>();

    public bool HasFlag(string flag)
    {
        return flags.ContainsKey(flag);
    }

    public int GetFlag(string flag)
    {
        if (flag.Contains(flag))
            return flags[flag];
        else return 0;
    }

    public void SetFlag(string flag, int value)
    {
        flags[flag] = value;
    }

    public void FlagAdd(string flag, int value)
    {
        if (flag.Contains(flag))
            flags[flag] += value;
        else
            flags.Add(flag, value);
    }

    public string GetString(string id)
    {
        if (Strings.ContainsKey(id))
            return Strings[id];
        else
            return id;
    }
}
