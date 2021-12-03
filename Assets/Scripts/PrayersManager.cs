using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrayersManager : MonoBehaviour
{
    [SerializeField] private List<Prayer> prayers;
    
    private int _prayerPoints;
    private float _spawnRadius = 2f;
    
    // private float _prayerTime;
    //
    // private List<Prayer> _prayers = new List<Prayer>{new Prayer(), new Prayer()};
    // private List<Coroutine> _prayerRoutines = new List<Coroutine>(2);
    //
    // private void Awake()
    // {
    //     _prayerTime = LevelGlobals.Instance.prayerTime;
    // }
    //
    //
    // public IEnumerator FollowPrayerCoroutine(int side, List<GestureSO> gestures, List<GestureSO> avoid)
    // {
    //     _prayers[side].Generate(gestures, avoid);
    //     yield return new WaitForSeconds(_prayerTime);
    //         
    //     Debug.Log("Prayer Failed");
    // }


    private void Awake()
    {
        _prayerPoints = LevelGlobals.Instance.prayerPoints;
    }

    private void Start()
    {
        prayers[0].SpawnPrayer(0, _spawnRadius);
        prayers[1].SpawnPrayer(1, _spawnRadius);
    }

    public int Score(int side)
    {
        return prayers[side].PrayerSize * _prayerPoints;
    }

    public void Generate(int side, List<GestureSO> avoid)
    {
        prayers[side].Generate(avoid);
    }

    public bool IsAccepted(int side, List<GestureSO> gestures)
    {
        return prayers[side].IsAccepted(gestures);
    }
}