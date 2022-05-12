using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineManager : MonoBehaviour
{
    [SerializeField] private GameObject outlineOBJ;

    private SpriteRenderer outlineSpriteRenderer;

    void Start()
    {
        outlineOBJ.transform.localScale += new Vector3(0.3f,0.3f,0.3f);
        outlineSpriteRenderer = outlineOBJ.GetComponent<SpriteRenderer>();
        outlineSpriteRenderer.color = Color.blue;
    }

    void Update()
    {
        
    }
}
