using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField] private GestureSO gesture;
    
    private List<GestureSO> _gestures;
    private int _currGest;

    private void Start()
    {
        _gestures = LevelGlobals.Instance.gestures;
        _currGest = _gestures.IndexOf(gesture);
    }

    public GestureSO GetGesture()
    {
        return gesture;
    }
    
    public void SwitchGesture()
    {
        _currGest = (_currGest + 1) % _gestures.Count;
        gesture = _gestures[_currGest];
    }
    
    public void SwitchGesture(GestureSO gest)
    {
        _currGest = _gestures.IndexOf(gest);
        gesture = _gestures[_currGest];
    }
}
