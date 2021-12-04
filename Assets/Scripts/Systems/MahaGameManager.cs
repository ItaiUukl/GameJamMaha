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
    [SerializeField] private List<GestureSO> gestures;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text highScoreText;

    private Dictionary<KeyCode, int> _handKeys;

    private int _size;
    private bool _gameOver = false;
    
    private float _prayerTime;
    private float _prayerBonus;

    private int _points = 0;
    private int _completed = 0;
    
    [SerializeField] private List<Slider> prayerSliders = new List<Slider>(2);
    private List<Coroutine> _prayerRoutines = new List<Coroutine>(2);
    [SerializeField] private Slider gameTimerSlider;
    private float _timer;
    private float _maxTime;

    private void Awake()
    {
        _size = LevelGlobals.Instance.initHands;
        _timer = LevelGlobals.Instance.levelTime;
        _maxTime = LevelGlobals.Instance.levelTime;
        _prayerBonus = LevelGlobals.Instance.prayerTimeBonus;
        _prayerTime = LevelGlobals.Instance.prayerTime;
        _handKeys = LevelGlobals.Instance.handKeys;
        LevelGlobals.Instance.gestures ??= gestures;
        highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

    private void Start()
    {
        StartCoroutine(StartLevel());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            IncreaseSize();
        }

        if (_gameOver)
        {
            return;
        }
        // increase game and prayer timers
        _timer -= Time.deltaTime;
        gameTimerSlider.value = _timer / _maxTime;
        prayerSliders[0].value -= (1 / _prayerTime) * Time.deltaTime;
        prayerSliders[1].value -= (1 / _prayerTime) * Time.deltaTime;
        
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

    private IEnumerator StartLevel()
    {
        yield return new WaitForEndOfFrame();
        _prayerRoutines.Add(StartCoroutine(StartPrayer(0)));
        _prayerRoutines.Add(StartCoroutine(StartPrayer(1)));
    }
    
    private IEnumerator StartPrayer(int side)
    {
        while (_timer > 0f) // stop when game over
        {
            prayerSliders[side].value = 1;
            prayersManager.Generate(side, handsManager.GesturesInSide(side));
            
            yield return new WaitForSeconds(_prayerTime);
            
            _timer -= LevelGlobals.Instance.prayerPenalty;
            prayerSliders[side].value = 1;
        }
    }
    
    private void GameOver()
    {
        _gameOver = true;
        Time.timeScale = 0;
        scoreText.text += "\nPress 'R' to reset.";
        if (_points > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", _points);
        }
    }

    private void IncreaseSize()
    {
        if (_size >= handsManager.MaxHands() )
        {
            return;
        }
        
        StopCoroutine(_prayerRoutines[0]);
        StopCoroutine(_prayerRoutines[1]);
        _prayerRoutines[0] = StartCoroutine(StartPrayer(0));
        _prayerRoutines[1] = StartCoroutine(StartPrayer(1));
        
        _size++;
        handsManager.HandsNumber++;
        prayersManager.SetSize(_size, new List<List<GestureSO>>{handsManager.GesturesInSide(0),
                                                                     handsManager.GesturesInSide(1)});
    }

    private void CompletePrayer(int side)
    {
        StopCoroutine(_prayerRoutines[side]);

        _completed++;
        // update timer
        _timer += _prayerBonus;

        _timer = Math.Min(_timer, _maxTime);

        _points += Mathf.CeilToInt(prayersManager.Score(side) * prayerSliders[side].value);
        scoreText.text = "Score " + _points;

        if (_completed > handsManager.HandsNumber * 4)
        {
            _completed = 0;
            IncreaseSize();
        }
        else
        {
            _prayerRoutines[side] = StartCoroutine(StartPrayer(side));
        }
    }
    
    private void ChangeGesture(int hand)
    {
        int side = handsManager.HandSide(hand);
        
        handsManager.ChangeHand(hand);
        
        if (prayersManager.IsAccepted(side, handsManager.GesturesInSide(side)))
        {
            CompletePrayer(side);
        }
    }
}
