using System.Collections.Generic;
using UnityEngine;

public class Prayer
{
    private int _size = LevelGlobals.Instance.initPrayerSize;

    public int PrayerSize
    {
        get => _size;
        set
        {
            _size = value;
            //TODO: Implement
        }
    }

    public void Generate(List<GestureSO> gestures, List<GestureSO> avoid)
    {
        //TODO: Implement
    }

    public List<GestureSO> GetGestures()
    {
        //TODO: Implement
        return null;
    }

    public bool IsAccepted(List<GestureSO> gestures)
    {
        //TODO: Implement
        return false;
    }

}
