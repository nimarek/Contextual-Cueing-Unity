using System.Collections;
using UnityEngine;

public class StartMeUp : MonoBehaviour
{
    public int blockPauseDuration = 1;
    private Quaternion centerRot;
    public ChooseTrial chooseTrial;
    public Spawner Spawner;
    public float scaleStartFixation;

    public GameObject startFixation;
    public SubjectInput subjectInput;

    public void Start()
    {
        StartFixation(Spawner.centerPosStart);
    }

    public void StartFixation(Vector3 centerPosition)
    {
        GameObject instanceOfStartFixation = Instantiate(startFixation, centerPosition, centerRot, transform);
        instanceOfStartFixation.gameObject.tag = "StartUp";
        instanceOfStartFixation.transform.localScale = new Vector3(scaleStartFixation,
            scaleStartFixation, scaleStartFixation);
        
        Invoke("DeleteAllChildren", blockPauseDuration); // Change Start time according to your needs
        StartCoroutine("StartPause");
    }
    
    private IEnumerator StartPause()
    {
        yield return new WaitForSeconds(blockPauseDuration);
        chooseTrial.ShuffleTrialOrder(Spawner.trialMarkers);
    }

    public void DeleteAllChildren()
    {
        foreach (Transform child in transform) Destroy(child.gameObject);
    }
}