using UnityEngine;

public class ColorLerper : MonoBehaviour
{
    public Color endColor;
    public float speed = 1.0f;
    public Color startColor;
    public float startTime;

    private void Start()
    {
        startTime = Time.time;
    }

    private void Update()
    {
        var t = (Time.time - startTime) * speed;
        GetComponent<Renderer>().material.color = Color.Lerp(startColor, endColor, t);
    }
}