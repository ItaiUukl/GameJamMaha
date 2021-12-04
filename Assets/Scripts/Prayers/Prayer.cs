using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Prayer: MonoBehaviour
{
    [SerializeField] private GameObject gestureHolderPref;
    
    private List<GameObject> _spriteHolders;

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
        _spriteHolders = new List<GameObject>(_size);
        _prayerGestures = new List<GestureSO>(_size);
    }
    
    /**
     * Spawn the Gesture sprite in a circle (??) around spawnPoint
     */
    public void SpawnPrayer(int side)
    {
        _side = side;
        
        foreach (GameObject sprite in _spriteHolders)
        {
            Destroy(sprite);
        }
        
        _spriteHolders = new List<GameObject>(_size);
        
        for (int i = 0; i < _size; i++)
        {
            float angle = Mathf.PI * ((i + 0.5f) / _size) - Mathf.PI * 0.5f;
            Vector3 spawnDir = new Vector3(Mathf.Cos(angle) * (2 * _side - 1), Mathf.Sin(-angle), 0);
            Vector3 spawnPos = transform.position + spawnDir * LevelGlobals.Instance.prayerRadius;
            
            _spriteHolders.Add(Instantiate(gestureHolderPref, spawnPos, Quaternion.identity));
            _spriteHolders[i].name = "Holder" + i + "Side" + side;
            _spriteHolders[i].transform.SetParent(transform);
            _spriteHolders[i].GetComponent<SpriteRenderer>().flipX = _side == LevelGlobals.RIGHT;
        }
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
            _spriteHolders[i].GetComponent<SpriteRenderer>().sprite = _prayerGestures[i].sprite;
        }
    }
}