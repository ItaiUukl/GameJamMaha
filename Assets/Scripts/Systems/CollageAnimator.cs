using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollageAnimator : MonoBehaviour
{

    [SerializeField] private List<SpriteRenderer> sprites;

    private List<Transform> _originalPos;
    
    // Start is called before the first frame update
    private void Start()
    {
        _originalPos = new List<Transform>(sprites.Count);

        foreach (SpriteRenderer sprite in sprites)
        {
            _originalPos.Add(sprite.transform);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void MoveSprite(SpriteRenderer sprite)
    {
        
    }
}
