using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelGlobals : Singleton<LevelGlobals>
{
    public const int LEFT = 0, RIGHT = 1;
    
    [SerializeField] public Dictionary<KeyCode, int> handKeys = new Dictionary<KeyCode, int>(){
        {KeyCode.A, 0},
        {KeyCode.S, 1},
        {KeyCode.Z, 2},
        {KeyCode.X, 3}
    };
    public List<GestureSO> gestures;

    public float levelTime = 10f;
    public float prayerTime = 5f;
    
    public int prayerPoints = 1;
    public float prayerTimeBonus = 15f;
    
    public int initHands = 4;
    public int initPrayerSize = 2;


    protected LevelGlobals() {}

    private void Awake()
    {
        
    }
}
