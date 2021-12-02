using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrayerManager : MonoBehaviour
{
    public Transform prayer1Spawn;
    public Transform prayer2Spawn;
    
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

    public void ShowPrayersOnBoard(List<Prayer> prayers)
    {
        
    }
}