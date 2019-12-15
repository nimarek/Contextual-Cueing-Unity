using System.Collections;
using UnityEngine;

public class StartMeUp : MonoBehaviour
{
    private readonly Vector3 startUpPos = new Vector3(5, 3, -3);
    public int blockPauseDuration = 2;
    private Quaternion centerRot;
    public ChooseTrial chooseTrial;
    public InterTrialInterBlock interTrialInterBlock;
    public Spawner spawner;

    public GameObject startFixation;
    public SubjectInput subjectInput;

    public void Start()
    {
        StartUpText();
    }

    public void StartUpText()
    {
        subjectInput.isInterTrial = true;

        var instanceOfInterTrialFixation = Instantiate(startFixation, startUpPos, centerRot, transform);
        instanceOfInterTrialFixation.transform.localScale = new Vector3(interTrialInterBlock.scaleInterTrialFixation,
            interTrialInterBlock.scaleInterTrialFixation, interTrialInterBlock.scaleInterTrialFixation);
        instanceOfInterTrialFixation.gameObject.tag = "interTrialFixation";

        Invoke("DeleteAllChildren", blockPauseDuration); // Change Start time according to your needs
        StartCoroutine("startPause");
    }

    private IEnumerator startPause()
    {
        yield return new WaitForSeconds(blockPauseDuration);
        chooseTrial.ShuffleTrialOrder(spawner.trialMarkers);
    }

    public void DeleteAllChildren()
    {
        foreach (Transform child in transform) Destroy(child.gameObject);
    }
}