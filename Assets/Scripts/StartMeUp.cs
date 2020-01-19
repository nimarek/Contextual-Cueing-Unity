using System.Collections;
using UnityEngine;
using PupilLabs;

public class StartMeUp : MonoBehaviour
{
    // This boolean needs to be channged via the Unity Editor Interface
    public bool enableEyeTracking = false;
    public int blockPauseDuration = 1;
    public int StartPauseDuration;
    private Quaternion centerRot;
    public ChooseTrial chooseTrial;
    public Spawner Spawner;
    public DisableDuringCalibration DisableDuringCalibration;
    public float scaleStartFixation;

    public GameObject startFixation;
    public SubjectInput subjectInput;

    public void Start()
    {
        if (enableEyeTracking)
        {
            Debug.Log("<color=Green>Starting Eye-Tracker mode. Waiting for calibration ... </color>");
        }
        
        if (!enableEyeTracking)
        {
            Debug.Log("<color=Green>No Eye-Tracking enabled.</color>");
            StartFixation(Spawner.centerPosStart);
        }
    }

    public void Update() 
    {
        // Enable if not in eye tracking mode
        if (DisableDuringCalibration.EyeTrackingSucc && enableEyeTracking)
        {
            StartFixation(Spawner.centerPosStart);
            DisableDuringCalibration.EyeTrackingSucc = false;
        }    
    }

    public void StartFixation(Vector3 centerPosition)
    {
        GameObject instanceOfStartFixation = Instantiate(startFixation, centerPosition, centerRot, transform);
        instanceOfStartFixation.gameObject.tag = "StartUp";
        instanceOfStartFixation.transform.localScale = new Vector3(scaleStartFixation,
            scaleStartFixation, scaleStartFixation);
        
        Invoke("DeleteAllChildren", StartPauseDuration); // Change Start time according to your needs
        StartCoroutine("StartPause");
    }
    
    private IEnumerator StartPause()
    {
        yield return new WaitForSeconds(StartPauseDuration);
        chooseTrial.ShuffleTrialOrder(Spawner.trialMarkers);
    }

    public void DeleteAllChildren()
    {
        foreach (Transform child in transform) Destroy(child.gameObject);
    }
}