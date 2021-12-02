using System.Collections.Generic;
using UnityEngine;

public class Prayer
{
    //private int _size = LevelGlobals.Instance.initPrayerSize;
    private int _size = 2;

    private List<GestureSO> _prayerGestures = new List<GestureSO>();

    public int PrayerSize
    {
        get => _size;
        set
        {
            _size = value;
            //TODO: Implement
        }
    }

    /**
     * generate a sequence of gestures in length of initPrayerSize - and from specific list.
     */
    public void Generate(List<GestureSO> gestures, List<GestureSO> avoid)
    {
        for (int i = 0; i < gestures.Count; i++)
        {
            while (true) 
            {
                GestureSO newGesture = gestures[Random.Range(0, gestures.Count)];
                if (newGesture.gestureId != avoid[i].gestureId)
                {
                    _prayerGestures[i] = newGesture;
                    break;
                }
            }
        }
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
}
