using System;
using System.Collections.Generic;
using UnityEngine;

public class HandsManager : MonoBehaviour
{
    private int _handsNum;
    [SerializeField] private List<Hand> _hands; // 0 to (_handsNum/2-1) are left side and (_handsNum/2) to _handsNum are right side

    private void Awake()
    {
        _handsNum = LevelGlobals.Instance.initHands;
    }

    /**
     * change the number of availible hands - half on each side;
     */
    public void SetHandsNumber(int num)
    {
        _handsNum = num;
    }

    public void ChangeHand(int handIndex)
    {
        _hands[handIndex].SwitchGesture();
    }

    /**
     * returns a list of the current gestures in the side
     */
    public List<GestureSO> GesturesInSide(int side)
    {
        List<GestureSO> sideGestures = new List<GestureSO>();
        int halfOfTheHands = _handsNum / 2;
        if (side == LevelGlobals.LEFT)
        {
            for (int i = 0; i < halfOfTheHands; i++)
            {
                sideGestures.Add(_hands[i].Gesture);
            }
        }
        else if (side == LevelGlobals.RIGHT)
        {
            for (int i = halfOfTheHands; i < _handsNum; i++)
            {
                sideGestures.Add(_hands[i].Gesture);
            }
        }
        return sideGestures;
    }
}
