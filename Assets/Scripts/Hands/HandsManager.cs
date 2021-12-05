using System.Collections.Generic;
using UnityEngine;

public class HandsManager : MonoBehaviour
{
    [SerializeField] private List<Hand> hands; // 0 to (_handsNum/2-1) are left side and (_handsNum/2) to _handsNum are right side

    private int _midHands;
    
    private List<int> _handsNum;

    private void Start()
    {
        _midHands = hands.Count / 2;
        _handsNum = new List<int> {0, 0};
        SetHandsNumber(0, LevelGlobals.Instance.initHands);
        SetHandsNumber(1, LevelGlobals.Instance.initHands);
    }

    public int GetHandsNum(int side)
    {
        return _handsNum[side];
    }
    
    public void IncreaseHandsNumber(int side)
    {
        SetHandsNumber(side, _handsNum[side] + 1);
    }
    
    private void SetHandsNumber(int side, int num)
    {
        _handsNum[side] = num;
        int sign = -1 + 2 * side;
        hands[_midHands + sign*_handsNum[side] - side].InitHandKey(GetKeyStr(_midHands + sign*_handsNum[side]));
        hands[_midHands + sign*_handsNum[side] - side].gameObject.SetActive(true);
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
        if ( _midHands - _handsNum[LevelGlobals.LEFT] <= handIndex && handIndex < _midHands + _handsNum[LevelGlobals.RIGHT])
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
            for (int i = _midHands - 1; i >= _midHands - _handsNum[side]; i--)
            {
                sideGestures.Add(hands[i].Gesture);
            }
        }
        else if (side == LevelGlobals.RIGHT)
        {
            for (int i = _midHands; i < _midHands + _handsNum[side]; i++)
            {
                sideGestures.Add(hands[i].Gesture);
            }
        }
        
        return sideGestures;
    }
    
    private string GetKeyStr(int handIndex)
    {
        Dictionary<KeyCode, int> handKeys = LevelGlobals.Instance.handKeys;
        foreach (KeyValuePair<KeyCode, int> entry in handKeys)
        {
            if (entry.Value == handIndex)
            {
                return entry.Key.ToString();
            }
        }
        Debug.Log("NO for " + handIndex.ToString());
        return null;
    }
}
