using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Variables
{
    public static int friendshipOnInteract = 1;
    public static int friendshipPerLevel = 5;
    public static int friendshipDecayRate = 1;

    public static int lovePerLevel = 5;
    public static int loveDecayRate = 5;
    public static int loveDecayByLoss = 5;

    public static int lossOnDefeat = 10;
    public static int lossPerLevel = 5;
    public static int lossDecayByLove = 5;

    public static float bossLoveLossMod = 2;

    public static bool debugMode = false;
}
