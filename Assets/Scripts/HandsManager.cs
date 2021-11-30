using System;
using System.Collections.Generic;
using UnityEngine;

public class HandsManager : MonoBehaviour
{
    private int _handsNum = LevelGlobals.Instance.initHands;

    /**
     * change the number of availible hands - half on each side;
     */
    public void SetHandsNumber(int num)
    {
        _handsNum = num;
    }

    public void ChangeHand(int hand)
    {
        //TODO: Implement
    }

    /**
     * returns a list of the current gestures in the side
     */
    public List<GestureSO> GesturesInSide(int side)
    {
        // TODO: implement
        return null;
    }
}
