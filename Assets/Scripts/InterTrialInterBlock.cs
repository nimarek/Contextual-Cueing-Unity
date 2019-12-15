using System.Collections;
using UnityEngine;

public class InterTrialInterBlock : MonoBehaviour
{
    public float blockPauseDuration;
    private Vector3 centerPos;
    private Quaternion centerRot;
    public ChooseTrial ChooseTrial;
    public GameObject interBlockText;
    public GameObject interTrialFixation;
    public float scaleInterBlockFixation;
    public float scaleInterTrialFixation;

    public Spawner Spawner;
    public SubjectInput SubjectInput;
    public TrackBehavioralData TrackBehavioralData;

    public void StartInterTrialFixation()
    {
        centerPos[1] = 3;

        var instanceOfInterTrialFixation = Instantiate(interTrialFixation, centerPos, centerRot, transform);
        instanceOfInterTrialFixation.transform.localScale = new Vector3(scaleInterTrialFixation,
            scaleInterTrialFixation, scaleInterTrialFixation);
    }

    public void StartInterBlockFixation()
    {
        centerPos[1] = 3;

        SubjectInput.blockPause = true;

        var instanceOfInterBlockFixation = Instantiate(interBlockText, centerPos, centerRot, transform);
        instanceOfInterBlockFixation.transform.localScale = new Vector3(scaleInterBlockFixation,
            scaleInterBlockFixation, scaleInterBlockFixation);

        Invoke("DeleteAllChildren", blockPauseDuration);
        StartCoroutine("interBlockPause");
    }

    private IEnumerator interBlockPause()
    {
        yield return new WaitForSeconds(blockPauseDuration);
        ChooseTrial.ShuffleTrialOrder(Spawner.trialMarkers);
    }

    public void DeleteAllChildren()
    {
        foreach (Transform child in transform) Destroy(child.gameObject);
    }
}