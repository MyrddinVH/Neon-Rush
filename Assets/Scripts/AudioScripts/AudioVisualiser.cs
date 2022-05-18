using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVisualiser : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float updateStep = 0.1f;
    [SerializeField] private int sampleDataLength = 1024;

    private float currentUpdateTime = 0f;

    [SerializeField] private float clipLoudness;
    private float[] clipSampleData;

    [SerializeField] private GameObject sprite;
    [SerializeField] private float sizeVector = 1;
    [SerializeField] private float minSize = 0;
    [SerializeField] private float maxSize = 500;

    void Start()
    {
        clipSampleData = new float[sampleDataLength];
    }

    void Update()
    {
        currentUpdateTime += Time.deltaTime;
        if(currentUpdateTime >= updateStep){
            currentUpdateTime = 0f;
            audioSource.clip.GetData(clipSampleData, audioSource.timeSamples);
            clipLoudness = 0f;
            foreach (var sample in clipSampleData)
            {
                clipLoudness += Mathf.Abs(sample);
            }
            clipLoudness /= sampleDataLength;

            clipLoudness *= sizeVector;
            clipLoudness = Mathf.Clamp(clipLoudness, minSize, maxSize);

            sprite.transform.localScale = new Vector3(clipLoudness, clipLoudness, clipLoudness);
        }
    }
}
