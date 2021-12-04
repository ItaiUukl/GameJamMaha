using System.Collections.Generic;
using UnityEngine;

public class LevelGlobals : Singleton<LevelGlobals>
{
    public const int LEFT = 0, RIGHT = 1;
    
    public Dictionary<KeyCode, int> handKeys = new Dictionary<KeyCode, int>{
        {KeyCode.A, 0},
        {KeyCode.W, 1},
        {KeyCode.E, 2},
        {KeyCode.F, 3},
        {KeyCode.H, 4},
        {KeyCode.U, 5},
        {KeyCode.I, 6},
        {KeyCode.L, 7}
    };
    public List<GestureSO> gestures;

    public float levelTime = 20f;
    public float prayerTime = 8f;

    public float prayerRadius = 1.3f;
    public int prayerPoints = 5;
    public float prayerTimeBonus = 3f;
    public float prayerPenalty = 2f;
    
    public int initHands = 1;


    protected LevelGlobals() {}
}
