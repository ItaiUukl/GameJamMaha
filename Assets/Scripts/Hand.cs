using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField] private GestureSO gesture;
    
    private List<GestureSO> _gestures;
    private int _currGestIndex;

    public GestureSO Gesture { get => gesture; private set => gesture = value; }

    private void Start()
    {
        _gestures = LevelGlobals.Instance.gestures;
        _currGestIndex = _gestures.IndexOf(Gesture);
    }
    
    public void SwitchGesture()
    {
        _currGestIndex = (_currGestIndex + 1) % _gestures.Count;
        Gesture = _gestures[_currGestIndex];
    }
    
    public void SwitchGesture(GestureSO gest)
    {
        _currGestIndex = _gestures.IndexOf(gest);
        Gesture = _gestures[_currGestIndex];
    }
}
