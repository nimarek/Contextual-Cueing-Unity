using UnityEngine;

/*
 * Simple script to gradually change the inter trial fixation object from one color to another.
 */
public class ColorLerper : MonoBehaviour
{
    public Color endColor;
    public float interTrialTime = 1.0f;
    public Color startColor;
    public float startTime;

    private void Start()
    {
        startTime = Time.time;
    }

    private void Update()
    {
        var t = (Time.time - startTime) * interTrialTime;
        GetComponent<Renderer>().material.color = Color.Lerp(startColor, endColor, t);
    }
}