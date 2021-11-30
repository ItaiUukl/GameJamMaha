using System.Collections.Generic;
using UnityEngine;

public class HandsManager : MonoBehaviour
{
    private int _handsNum = LevelGlobals.Instance.initHands;

    public void SetHandsNumber(int num)
    {
        _handsNum = num;
    }

    public void ChangeHand(int hand)
    {
        //TODO: Implement
    }

    public List<GestureSO> GesturesInSide(int side)
    {
        //TODO: Implement
        return null;
    }
}
