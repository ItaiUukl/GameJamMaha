using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MahaGameManager : MonoBehaviour
{
    [SerializeField] private HandsManager handsManager;

    private Dictionary<KeyCode, int> _handKeys;
    private List<GestureSO> _gestures;
    
    private float _prayerTime = LevelGlobals.Instance.prayerTime;
    
    private int _prayerPoints = LevelGlobals.Instance.prayerPoints;
    private float _prayerBonus = LevelGlobals.Instance.prayerTimeBonus;

    private List<Prayer> _prayers = new List<Prayer>{new Prayer(), new Prayer()};

    private int _points = 0;
    
    private List<Coroutine> _prayerRoutines = new List<Coroutine>(2);
    private float _timer = LevelGlobals.Instance.levelTime;


    private void Start()
    {
        _handKeys = LevelGlobals.Instance.handKeys;
        _gestures = LevelGlobals.Instance.gestures;
        
        _prayerRoutines[0] = StartCoroutine(StartPrayer(0));
        _prayerRoutines[1] = StartCoroutine(StartPrayer(1));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        _timer -= Time.deltaTime;
        if (_timer <= 0f)
        {
            GameOver();
        }
        
        foreach (KeyCode key in _handKeys.Keys)
        {
            if (Input.GetKeyDown(key))
            {
                ChangeGesture(_handKeys[key]);
            }
        }
    }

    private IEnumerator StartPrayer(int side)
    {
        while (_timer > 0f) // stop when game over
        {
            _prayers[side].Generate(_gestures, handsManager.GesturesInSide(side));

            yield return new WaitForSeconds(_prayerTime);
            
            Debug.Log("Prayer Failed");
        }
    }
    
    private void GameOver()
    {
        Debug.Log("You lost the round, point: " + _points);
    }

    private int HandSide(int hand)
    {
        return hand < 5 ? LevelGlobals.LEFT : LevelGlobals.RIGHT;
    }

    private void CompletePrayer(int side)
    {
        StopCoroutine(_prayerRoutines[side]);
        
        _timer += _prayerBonus;
        _points += _prayers[side].PrayerSize * _prayerPoints;

        _prayerRoutines[side] = StartCoroutine(StartPrayer(side));
    }
    
    private void ChangeGesture(int hand)
    {
        int side = HandSide(hand);
        
        handsManager.ChangeHand(hand);
        
        if (_prayers[side].IsAccepted(handsManager.GesturesInSide(side)))
        {
            CompletePrayer(side);
        }
    }
}
