using System.Collections;
using UnityEngine;

public class InterTrialInterBlock : MonoBehaviour
{
    public ColorLerper ColorLerper;
    public float interBlockTime;
    private Vector3 centerPos;
    private Quaternion centerRot;
    public ChooseTrial ChooseTrial;
    public GameObject interBlockText;
    public GameObject interTrialFixation;
    public float scaleInterBlockFixation;
    public float scaleInterTrialFixation;

    public Spawner Spawner;
    public SubjectInput SubjectInput;
    
    public void StartInterTrialFixation(Vector3 centerPositionInterTrial)
    {
        GameObject instanceOfInterTrialFixation = Instantiate(interTrialFixation, centerPositionInterTrial, centerRot, transform);
        instanceOfInterTrialFixation.gameObject.tag = "InterTrialFixation";
        instanceOfInterTrialFixation.transform.localScale = new Vector3(scaleInterTrialFixation,
            scaleInterTrialFixation, scaleInterTrialFixation);
    }

    public void StartInterBlockFixation(Vector3 centerPositionInterTrial)
    {
        SubjectInput.blockPause = true;

        GameObject instanceOfInterBlockFixation = Instantiate(interBlockText, centerPositionInterTrial, centerRot, transform);
        instanceOfInterBlockFixation.transform.localScale = new Vector3(scaleInterBlockFixation,
            scaleInterBlockFixation, scaleInterBlockFixation);

        Invoke("DeleteAllChildren", interBlockTime);
        StartCoroutine("InterBlockPause");
    }

    private IEnumerator InterBlockPause()
    {
        yield return new WaitForSeconds(interBlockTime);
        ChooseTrial.ShuffleTrialOrder(Spawner.trialMarkers);
    }
    
    public void DeleteAllChildren()
    {
        foreach (Transform child in transform) Destroy(child.gameObject);
    }
}