using System.Collections.Generic;
using UnityEngine;

public class LevelGlobals : Singleton<LevelGlobals>
{
    public Dictionary<KeyCode, int> handKeys;
    public List<GestureSO> gestures;

    public int LEFT = 0, RIGHT = 1;
    
    public float levelTime;
    public float prayerTime;
    
    public int prayerPoints;
    
    public int initHands;
    public int initPrayerSize;


    protected LevelGlobals() {}
}
