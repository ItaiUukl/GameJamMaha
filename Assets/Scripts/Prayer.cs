using System.Collections.Generic;
using UnityEngine;

public class Prayer
{
    private int _size = LevelGlobals.Instance.initPrayerSize;

    private List<GestureSO> _prayerGestures;
    private int _gesturesNumToFinish;

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
        _gesturesNumToFinish = _prayerGestures.Count;
    }

    public List<GestureSO> GetGestures()
    {
        return _prayerGestures;
    }

    public bool IsAccepted(List<GestureSO> gestures)
    {
        return _gesturesNumToFinish == 0;
    }

}
