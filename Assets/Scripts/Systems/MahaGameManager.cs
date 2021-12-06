using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MahaGameManager : MonoBehaviour
{
    // todo: Temporary fields
    [SerializeField] private HandsManager handsManager;
    [SerializeField] private PrayersManager prayersManager;
    [SerializeField] private List<TextMeshProUGUI> scoreTexts;
    [SerializeField] private TextMeshProUGUI highScoreText;

    private Dictionary<KeyCode, int> _handKeys;

    private Vector2Int _size;
    private bool _gameOver = false;
    
    private float _prayerTime;
    private float _prayerBonus;

    private Vector2Int _points = Vector2Int.zero;
    private Vector2Int _completed = Vector2Int.zero;
    
    [SerializeField] private List<Slider> prayerSliders = new List<Slider>(2);
    private List<Coroutine> _prayerRoutines = new List<Coroutine>(2);
    [SerializeField] private Slider gameTimerSlider;
    private float _timer;
    private float _maxTime;

    private void Awake()
    {
        _size = new Vector2Int( LevelGlobals.Instance.initHands, LevelGlobals.Instance.initHands);
        _timer = LevelGlobals.Instance.levelTime;
        _maxTime = LevelGlobals.Instance.levelTime;
        _prayerBonus = LevelGlobals.Instance.prayerTimeBonus;
        _prayerTime = LevelGlobals.Instance.prayerTime;
        _handKeys = LevelGlobals.Instance.handKeys;
        highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0);
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

        if (_gameOver)
        {
            return;
        }
        // increase game and prayer timers
        _timer -= Time.deltaTime;
        gameTimerSlider.value = _timer / _maxTime;
        prayerSliders[0].value -= (1 / _prayerTime) * Time.deltaTime * 0.5f;
        prayerSliders[1].value -= (1 / _prayerTime) * Time.deltaTime * 0.5f;
        
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
        while (!_gameOver) // stop when game over
        {
            prayerSliders[side].value = 1;
            prayersManager.Generate(side, handsManager.GesturesInSide(side));
            
            yield return new WaitForSeconds(_prayerTime);

            _points[side] -= LevelGlobals.Instance.prayerPenalty;
            prayerSliders[side].value = 1;
        }
    }
    
    private void GameOver()
    {
        _gameOver = true;
        Time.timeScale = 0;
        scoreTexts[0].text += "\nPress 'R' to reset.";
        if (_points[0] > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", _points[0]);
        }
    }

    private void IncreaseSize(int side)
    {
        if (_size[side] >= handsManager.MaxHands() )
        {
            return;
        }
        
        StopCoroutine(_prayerRoutines[0]);
        StopCoroutine(_prayerRoutines[1]);
        _prayerRoutines[0] = StartCoroutine(StartPrayer(0));
        _prayerRoutines[1] = StartCoroutine(StartPrayer(1));
        
        _size[side]++;
        handsManager.IncreaseHandsNumber(side);
        prayersManager.SetSize(side, _size[side], handsManager.GesturesInSide(side));
    }

    private void CompletePrayer(int side)
    {
        StopCoroutine(_prayerRoutines[side]);

        _completed[side]++;

        _points[side] += Mathf.CeilToInt(prayersManager.Score(side) * prayerSliders[side].value);
        scoreTexts[side].text = "Score " + _points[side];

        if (_completed[side] > handsManager.GetHandsNum(side) * 4)
        {
            _completed[side] = 0;
            IncreaseSize(side);
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
