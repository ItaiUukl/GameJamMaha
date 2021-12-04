using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollageAnimator : MonoBehaviour
{

    [SerializeField] private List<SpriteRenderer> sprites;
    [SerializeField, Min(0)] private float animationRange = 0.25f;
    [SerializeField, Min(0)] private float animationSpeed = 0.5f;
    

    private List<Vector3> _originalPos;
    private List<Vector4> _perlinPos;

    // Start is called before the first frame update
    private void Start()
    {
        _originalPos = new List<Vector3>(sprites.Count);
        _perlinPos = new List<Vector4>(sprites.Count);

        foreach (SpriteRenderer sprite in sprites)
        {
            _originalPos.Add(sprite.transform.position);
            _perlinPos.Add(new Vector4(Random.Range(0,10000), Random.Range(0,10000), 
                                           Random.Range(0,10000), Random.Range(0,10000)));
        }
    }

    // Update is called once per frame
    private void Update()
    {
        MoveSprites();
    }

    private float StretchedPerlin(float x, float y)
    {
        return -1 + 2 * Mathf.PerlinNoise(x, y);
    }
    
    private void MoveSprites()
    {
        for (int i = 0; i < sprites.Count; i++)
        {
            _perlinPos[i] += Vector4.one * Time.deltaTime * animationSpeed;

            Vector3 posAdd = new Vector3(StretchedPerlin(_perlinPos[i].x, _perlinPos[i].y) * sprites[i].bounds.size.x,
                                         StretchedPerlin(_perlinPos[i].z, _perlinPos[i].w) * sprites[i].bounds.size.y,
                                         0) * animationRange;

            sprites[i].transform.position = _originalPos[i] + posAdd;
        }
    }
}
