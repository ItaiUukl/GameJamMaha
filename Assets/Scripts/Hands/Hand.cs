using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField] private GestureSO gesture;
    [SerializeField] private SpriteRenderer gestureRenderer;
    [SerializeField] private TextMeshPro handKey;

    private List<GestureSO> _gestures;
    private int _currGestIndex;

    public GestureSO Gesture { get => gesture; private set => gesture = value; }

    private void Start()
    {
        _gestures = LevelGlobals.Instance.gestures;
        _currGestIndex = _gestures.IndexOf(Gesture);
        gestureRenderer.sprite = gesture.sprite;
        // Debug.Log(gesture.gestureName);
    }
    
    public void SwitchGesture()
    {
        _currGestIndex = (_currGestIndex + 1) % _gestures.Count;
        Gesture = _gestures[_currGestIndex];
        gestureRenderer.sprite = gesture.sprite;
    }
    
    public void SwitchGesture(GestureSO gest)
    {
        _currGestIndex = _gestures.IndexOf(gest);
        Gesture = _gestures[_currGestIndex];
    }

    public void InitHandKey(string key)
    {
        handKey.text = key;
    }
}
