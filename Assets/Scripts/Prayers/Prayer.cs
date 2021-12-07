using System.Collections.Generic;
using UnityEngine;

public class Prayer: MonoBehaviour
{
    [SerializeField] private GameObject gestureHolderPref;
    [SerializeField] private List<SpriteRenderer> _spriteHolders = new List<SpriteRenderer>(4);

    private int _size;
    public int PrayerSize
    {
        get => _size;
        set
        {
            _size = value;
            //TODO: Implement
        }
    }

    private int _side;
    
    private List<GestureSO> _prayerGestures;

    private void Awake()
    {
        _size = LevelGlobals.Instance.initHands;
        // _spriteHolders = new List<GameObject>(_size);
        _prayerGestures = new List<GestureSO>(_size);
    }

    /**
     * generate a sequence of gestures in length of initPrayerSize - and from specific list.
     */
    public void Generate(List<GestureSO> avoid)
    {
        List<GestureSO> gestures = LevelGlobals.Instance.gestures;
        int identicalCount = 0;
        List<int> gesturesIndices = new List<int>(_size); 
        _prayerGestures = new List<GestureSO>(_size);

        for (int i = 0; i < _size; i++)
        {
            gesturesIndices.Add(Random.Range(0, gestures.Count));
            _prayerGestures.Add(gestures[gesturesIndices[i]]);
            
            if (_prayerGestures[i].gestureId == avoid[i].gestureId)
            {
                identicalCount++;
            }
        }

        if (identicalCount >= _size)
        {
            int rngI = Random.Range(0, _size);

            _prayerGestures[rngI] = gestures[(gesturesIndices[rngI] + 1) % gestures.Count];
        }
        
        UpdateSprites();
    }

    /**
     * get the wanted sequance of gestures of the prayers
     */
    public List<GestureSO> GetGestures()
    {
        return _prayerGestures;
    }

    /**
     * return true if gestures matches the prayer sequence
     */
    public bool IsAccepted(List<GestureSO> gestures)
    {
        if (gestures.Count == 0)
        {
            return false;
        }

        for (int i = 0; i < gestures.Count; i++)
        {
            if (gestures[i].gestureId != _prayerGestures[i].gestureId)
            {
                return false;
            }
        }

        return true;
    }

    private void UpdateSprites()
    {
        for (int i = 0; i < _size; i++)
        {
            _spriteHolders[i].sprite = _prayerGestures[i].sprite;
        }
    }
}