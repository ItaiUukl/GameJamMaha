using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField] private GestureSO gesture;
    
    private List<GestureSO> _gestures;
    private int _currGestIndex;

    private void Start()
    {
        _gestures = LevelGlobals.Instance.gestures;
        _currGestIndex = _gestures.IndexOf(gesture);
    }

    public GestureSO GetGesture()
    {
        return gesture;
    }
    
    public void SwitchGesture()
    {
        _currGestIndex = (_currGestIndex + 1) % _gestures.Count;
        gesture = _gestures[_currGestIndex];
    }
    
    public void SwitchGesture(GestureSO gest)
    {
        _currGestIndex = _gestures.IndexOf(gest);
        gesture = _gestures[_currGestIndex];
    }
}
