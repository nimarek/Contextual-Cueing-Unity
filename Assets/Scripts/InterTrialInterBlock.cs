using System.Collections;
using UnityEngine;

public class InterTrialInterBlock : MonoBehaviour
{
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
    
    public void StartInterTrialFixation(Vector3 centerPosition, int numCircles)
    {
        centerPosition[1] = numCircles / 2;
        
        var instanceOfInterTrialFixation = Instantiate(interTrialFixation, centerPosition, centerRot, transform);
        instanceOfInterTrialFixation.gameObject.tag = "InterTrialFixation";
        instanceOfInterTrialFixation.transform.localScale = new Vector3(scaleInterTrialFixation,
            scaleInterTrialFixation, scaleInterTrialFixation);
    }

    public void StartInterBlockFixation(Vector3 centerPosition, int numCircles)
    {
        centerPosition[1] = numCircles / 2;

        SubjectInput.blockPause = true;

        var instanceOfInterBlockFixation = Instantiate(interBlockText, centerPos, centerRot, transform);
        instanceOfInterBlockFixation.transform.localScale = new Vector3(scaleInterBlockFixation,
            scaleInterBlockFixation, scaleInterBlockFixation);

        Invoke("DeleteAllChildren", interBlockTime);
        StartCoroutine("interBlockPause");
    }

    private IEnumerator interBlockPause()
    {
        yield return new WaitForSeconds(interBlockTime);
        ChooseTrial.ShuffleTrialOrder(Spawner.trialMarkers);
    }

    public void DeleteAllChildren()
    {
        foreach (Transform child in transform) Destroy(child.gameObject);
    }
}