using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IDie : IDropHandler
{
    void OnDrop(RolledDie die);
    bool Check(int die);
    bool IsActive { get; }

    float X { get; }
    float Y { get; }
}
