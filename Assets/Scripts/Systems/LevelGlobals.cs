using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelGlobals : Singleton<LevelGlobals>
{
    public const int LEFT = 0, RIGHT = 1;
    
    public Dictionary<KeyCode, int> handKeys = new Dictionary<KeyCode, int>{
        {KeyCode.Z, 0},
        {KeyCode.A, 1},
        {KeyCode.M, 2},
        {KeyCode.K, 3}
    };
    public List<GestureSO> gestures;

    public float levelTime = 10f;
    public float prayerTime = 5f;
    
    public int prayerPoints = 1;
    public float prayerTimeBonus = 15f;
    
    public int initHands = 4;
    public int initPrayerSize = 2;


    protected LevelGlobals() {}
}
