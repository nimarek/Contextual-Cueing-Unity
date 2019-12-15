using UnityEngine;

public class Timer : MonoBehaviour
{
    public static Timer instance;
    public float currentTime;
    public float globalTime;
    public float interTrialTime;

    //string write2File = "Assets/Data/ReactionTime.txt";
    private bool record;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Timer found!");
            return;
        }

        instance = this;
    }


    // Update is called once per frame
    private void Update()
    {
        if (record)
        {
            globalTime = Time.time;
            currentTime += 1 * Time.deltaTime;
        }
    }

    public void StartRecord()
    {
        globalTime = 0.0f;
        currentTime = 0.0f;
        record = true;
    }

    public void StopRecord()
    {
        record = false;
        Debug.Log("<color=red>GlobalTime</color>: " + globalTime);
        Debug.Log("<color=red>TrialTime</color>: " + currentTime);
    }

    public void interTrialTimer()
    {
        interTrialTime += 1 * Time.deltaTime;
    }
}