using System.Collections;
using UnityEngine;

public class SubjectInput : MonoBehaviour
{
    public bool blockPause;
    public ChooseTrial chooseTrial;
    public InterTrialInterBlock interBlock;
    public InterTrialInterBlock interTrialInterBlock;
    public bool isFixedTrial;
    public bool isInterTrial;
    public Spawner Spawner;
    private float InterTrialPause = 1.0f;
    public bool subClicked;
    public bool subCorrectResponse;
    public bool subInputLeft;
    public bool subInputRight;
    public Timer timer;
    public TrackBehavioralData TrackBehavioralData;
    public TrackDisplayConfiguration TrackDisplayConfiguration;

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
                if (Spawner.flipTarget)
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
                if (Spawner.flipTarget)
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
        TrackBehavioralData.FindResponseVariables();
        TrackDisplayConfiguration.SaveTrialConfiguration();

        subClicked = true;
        isInterTrial = true;
        Spawner.DeleteAllChildren();
        
        Invoke("CallTrialStart", InterTrialPause);
    }

    public void CallTrialStart()
    {
        interBlock.StartInterTrialFixation(Spawner.centerPosInterTrial);
    }


    public void EndInterTrial()
    {
        interTrialInterBlock.DeleteAllChildren();
        subClicked = false;

        chooseTrial.countTrialGlobal++;
        chooseTrial.countTrialBlock++;
        chooseTrial.chooseDisplay++;
        chooseTrial.ChooseNextTrial();
    }
}