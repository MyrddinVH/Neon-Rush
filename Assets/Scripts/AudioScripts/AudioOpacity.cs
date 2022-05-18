using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SynchronizerData;

public class AudioOpacity : MonoBehaviour
{
    private BeatObserver beatObserver;
    private bool isBeat;
    public float restSmoothTime;
    public float timeToBeat;

    public float beatOpacity;
    public float restOpacity;

    [SerializeField] private LevelColors levelColors;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        beatObserver = GetComponent<BeatObserver>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Debug.Log(beatObserver.beatMask);
        if((beatObserver.beatMask & BeatType.UpBeat) == BeatType.UpBeat){
            // Debug.Log("upbeat");
            Beat();
        }
        if((beatObserver.beatMask & BeatType.OnBeat) == BeatType.OnBeat){
            // Debug.Log("downbeat");
            Beat();
        }
        if((beatObserver.beatMask & BeatType.DownBeat) == BeatType.DownBeat){
            // Debug.Log("onbeat");
            Beat();
        }
        if((beatObserver.beatMask & BeatType.OffBeat) == BeatType.OffBeat){
            // Debug.Log("offbeat");
            Beat();
        }

        ChangeColor();

        if(isBeat){
        return;
        }
        spriteRenderer.color = Color.Lerp(spriteRenderer.color, new Color(spriteRenderer.color.r,spriteRenderer.color.g,spriteRenderer.color.b, restOpacity), restSmoothTime * Time.deltaTime);
    }

    private void Beat(){
        isBeat = true;
        StopCoroutine("MoveToScale");
        StartCoroutine("MoveToScale", beatOpacity);
    }

    private IEnumerator MoveToScale(float target){

        float current = spriteRenderer.color.a;
        float initial = current;

        float timer = 0;

        while(current != target){
            current = Mathf.Lerp(initial, target, timer / timeToBeat);
            timer += Time.deltaTime;

            spriteRenderer.color = new Color(spriteRenderer.color.r,spriteRenderer.color.g,spriteRenderer.color.b, current);

            yield return null;
        }
        isBeat = false;
    }

        private void ChangeColor(){
        Color randomColor = levelColors.colors[Random.Range(0, levelColors.colors.Length)];
        spriteRenderer.color = new Color(randomColor.r, randomColor.g, randomColor.b, spriteRenderer.color.a);
    }
}
