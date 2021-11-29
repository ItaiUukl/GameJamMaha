using System.Collections.Generic;
using UnityEngine;

public class Prayer : MonoBehaviour
{
    private int _size = LevelGlobals.Instance.initPrayerSize;
    
    public void SetPrayerSize(int size)
    {
        _size = size;
        //TODO: Implement
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
