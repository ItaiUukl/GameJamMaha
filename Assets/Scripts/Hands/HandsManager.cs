using System.Collections.Generic;
using UnityEngine;

public class HandsManager : MonoBehaviour
{
    [SerializeField] private List<Hand> hands; // 0 to (_handsNum/2-1) are left side and (_handsNum/2) to _handsNum are right side

    private int _midHands;
    
    private int _handsNum;
    public int HandsNumber
    {
        get => _handsNum;
        set
        {
            _handsNum = value;
            hands[_midHands - _handsNum].gameObject.SetActive(true);
            hands[_midHands + _handsNum - 1].gameObject.SetActive(true);
        }
    }
    
    private void Start()
    {
        _midHands = hands.Count / 2;
        HandsNumber = LevelGlobals.Instance.initHands;
    }

    public int HandSide(int hand)
    {
        return hand < _midHands ? LevelGlobals.LEFT : LevelGlobals.RIGHT;
    }
    
    public int MaxHands()
    {
        return _midHands;
    }

    public void ChangeHand(int handIndex)
    {
        if ( _midHands - _handsNum <= handIndex && handIndex < _midHands + _handsNum)
        {
            hands[handIndex].SwitchGesture();
        }
    }

    /**
     * returns a list of the current gestures in the side
     */
    public List<GestureSO> GesturesInSide(int side)
    {
        List<GestureSO> sideGestures = new List<GestureSO>();
        
        if (side == LevelGlobals.LEFT)
        {
            for (int i = _midHands - 1; i >= _midHands - _handsNum; i--)
            {
                sideGestures.Add(hands[i].Gesture);
            }
        }
        else if (side == LevelGlobals.RIGHT)
        {
            for (int i = _midHands; i < _midHands + _handsNum; i++)
            {
                sideGestures.Add(hands[i].Gesture);
            }
        }
        
        return sideGestures;
    }
}
