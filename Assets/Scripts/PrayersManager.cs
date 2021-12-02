using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrayersManager : MonoBehaviour
{
    [SerializeField] private Transform _prayer1Spawn;
    [SerializeField] private Transform _prayer2Spawn;
    [SerializeField] private GameObject gestureHolderPref;
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

    /**
     * Spawn the Gesture sprite in a circle (??) around spawnPoint
     */
    public void ShowPrayersOnBoard(Prayer prayer, int side)
    {
        List<GestureSO> prayerGestures = prayer.GetGestures();

        for (int i = 0; i < prayerGestures.Count; i++)
        {
            float angle = i * Mathf.PI*2f / prayerGestures.Count;
            Vector3 spawnDir = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));
            Vector3 spawnPos = GetSpawnPositionBySide(side).position + spawnDir * _spawnRadius;
            GameObject holder = Instantiate(gestureHolderPref, spawnPos, Quaternion.identity);

            holder.name = holder.name + i.ToString() + "Side" + side.ToString();
            holder.transform.SetParent(GetSpawnPositionBySide(side));
            holder.GetComponent<SpriteRenderer>().sprite = prayerGestures[i].sprite;
        }
        Debug.Log("Show Prayer");
    }

    public void DestroyPrayer(int side)
    {
        Transform spawnPoint = GetSpawnPositionBySide(side);
        for(int i = 0; i < spawnPoint.childCount; i++)
        {
            Transform child = spawnPoint.transform.GetChild(i);
            Destroy(child.gameObject);
        }
        Debug.Log("Destroy Prayer");
    }

    private Transform GetSpawnPositionBySide(int side)
    {
        return side == 0 ? _prayer1Spawn : _prayer2Spawn;
    }
}