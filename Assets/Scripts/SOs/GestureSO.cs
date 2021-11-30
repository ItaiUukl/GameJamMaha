using UnityEngine;

[CreateAssetMenu(fileName = "New Gesture", menuName = "GestureSO")]
public class GestureSO : ScriptableObject
{
    public int gestureId;
    public string gestureName;
    public Sprite sprite;
}
