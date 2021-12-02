using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MahaGameManager : MonoBehaviour
{
    // todo: Temporary fields
    [SerializeField] private HandsManager handsManager;
    [SerializeField] private PrayersManager prayersManager;
    [SerializeField] private Text scoreText;

    private Dictionary<KeyCode, int> _handKeys;
    private List<GestureSO> _gestures;

    private float _prayerTime;
    private int _prayerPoints;
    private float _prayerBonus;


    private int _points = 0;
    
    private List<Prayer> _prayers;
    private List<Coroutine> _prayerRoutines = new List<Coroutine>(2);
    private float _timer;

    private void Awake()
    {
        _timer = LevelGlobals.Instance.levelTime;
        _prayerPoints = LevelGlobals.Instance.prayerPoints;
        _prayerBonus = LevelGlobals.Instance.prayerTimeBonus;
        _prayerTime = LevelGlobals.Instance.prayerTime;
        _handKeys = LevelGlobals.Instance.handKeys;
        _gestures = LevelGlobals.Instance.gestures;
        
        int initPrayerSize = LevelGlobals.Instance.initPrayerSize;
        _prayers = new List<Prayer>{new Prayer(initPrayerSize), new Prayer(initPrayerSize)};
    }

    private void Start()
    {
        //_prayerRoutines[0] = StartCoroutine(StartPrayer(0));
        //_prayerRoutines[1] = StartCoroutine(StartPrayer(1));
        _prayerRoutines.Add(StartCoroutine(StartPrayer(0)));
        _prayerRoutines.Add(StartCoroutine(StartPrayer(1)));
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
            prayersManager.ShowPrayersOnBoard(_prayers[side], side);
            yield return new WaitForSeconds(_prayerTime);
            
            prayersManager.DestroyPrayer(side);
            Debug.Log("Prayer Failed");
        }
    }
    
    private void GameOver()
    {
        Debug.Log("You lost the round, point: " + _points);
    }

    private int HandSide(int hand)
    {
        // todo: won't work with NOT 2 hands
        return hand < (handsManager.GetHandsNum() / 2) ? LevelGlobals.LEFT : LevelGlobals.RIGHT;
    }

    private void CompletePrayer(int side)
    {
        StopCoroutine(_prayerRoutines[side]);
        
        _timer += _prayerBonus;
        _points += _prayers[side].PrayerSize * _prayerPoints;
        scoreText.text = "Score " + _points.ToString();

        prayersManager.DestroyPrayer(side);
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
