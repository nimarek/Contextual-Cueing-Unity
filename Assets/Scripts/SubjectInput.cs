using UnityEngine;

public class SubjectInput : MonoBehaviour
{
    public bool blockPause;
    public ChooseTrial chooseTrial;
    public TrackBehavioralData TrackBehavioralData;
    public InterTrialInterBlock interBlock;
    public InterTrialInterBlock interTrialInterBlock;
    public bool isFixedTrial;

    public bool isInterTrial;
    public Spawner spawner;

    public bool subClicked;
    public bool subCorrectResponse;
    public bool subInputLeft;
    public bool subInputRight;
    public Timer timer;

    public void Update()
    {
        DetectSubInput();
    }

    public void DetectSubInput()
    {
        if (!blockPause && !isInterTrial)
        {
            if (!subClicked && Input.GetButtonDown("Fire1"))
            {
                if (spawner.flipTarget)
                {
                    timer.StopRecord();
                }
                else
                {
                    timer.StopRecord();
                    subCorrectResponse = false;
                }

                EndTrial();
            }

            if (!subClicked && Input.GetButtonDown("Fire2"))
            {
                if (spawner.flipTarget)
                {
                    timer.StopRecord();
                }
                else
                {
                    timer.StopRecord();
                    subCorrectResponse = false;
                }

                EndTrial();
            }
        }

        if (isInterTrial && Input.GetButtonDown("Fire3")) EndInterTrial();
    }

    private void EndTrial()
    {
        TrackBehavioralData.saveTrial();
        
        subClicked = true;
        isInterTrial = true;
        spawner.DeleteAllChildren();

        interBlock.StartInterTrialFixation();
    }

    private void EndInterTrial()
    {
        interTrialInterBlock.DeleteAllChildren();
        subClicked = false;

        chooseTrial.countTrialGlobal++;
        chooseTrial.countTrialBlock++;
        chooseTrial.chooseDisplay++;
        chooseTrial.ChooseNextTrial();
    }
}