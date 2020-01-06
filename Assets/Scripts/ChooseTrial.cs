using System.Collections.Generic;
using UnityEngine;

public class ChooseTrial : MonoBehaviour
{
    private int _tmp = -1;
    public int chooseDisplay;
    public int countBlock = 1;
    public int countTrialBlock = 1;
    public int countTrialGlobal = 1;
    public int maxBlock = 10;
    private int maxTrials;
    public int numFixedTrials = 8;

    public Spawner Spawner;
    public InterTrialInterBlock InterTrialInterBlock;
    public SubjectInput subjectInput;
    public Timer timer;

    public void ShuffleDisplayConfiguration(List<int> displayConfiguration)
    {
        for (var i = displayConfiguration.Count - 1; i > 0; i--)
        {
            var rnd = Random.Range(0, i);
            var temp = displayConfiguration[i];

            displayConfiguration[i] = displayConfiguration[rnd];
            displayConfiguration[rnd] = temp;
        }
    }

    public void ShuffleTrialOrder(List<int> trialOrderList)
    {
        Debug.Log("<color=green>Shuffling list ...</color>");
        subjectInput.blockPause = false;

        var _random = new System.Random();

        var n = trialOrderList.Count;
        for (var i = 0; i < n; i++)
        {
            var r = i + (int) (_random.NextDouble() * (n - i));
            var tmp = trialOrderList[r];

            trialOrderList[r] = trialOrderList[i];
            trialOrderList[i] = tmp;
        }

        CheckForRepetitions();
    }

    public void CheckForRepetitions()
    {
        Debug.Log("<color=green>Checking for repetitions ...</color>");

        for (var i = 0; i < Spawner.trialMarkers.Count; i++)
        {
            if (_tmp == Spawner.trialMarkers[i])
            {
                Debug.Log("<color=red>Repetition encountered. New shuffle ...</color>");
                ShuffleTrialOrder(Spawner.trialMarkers);
                return;
            }

            _tmp = Spawner.trialMarkers[i];
        }

        ChooseNextTrial();
    }

    // Execute methods in shuffled order & check for trials and blocks
    public void ChooseNextTrial()
    {
        maxTrials = Spawner.trialMarkers.Count;

        if (countBlock <= maxBlock)
        {
            if (countTrialBlock <= maxTrials)
            {
                if (Spawner.trialMarkers[chooseDisplay] == 1)
                {
                    Spawner.StartTrial(Spawner.fixedTrials[0]);
                    Debug.Log("Trial: " + countTrialBlock + " Of: " + maxTrials);
                    return;
                }

                if (Spawner.trialMarkers[chooseDisplay] == 2)
                {
                    Spawner.StartTrial(Spawner.fixedTrials[1]);
                    Debug.Log("Trial: " + countTrialBlock + " Of: " + maxTrials);
                    return;
                }

                if (Spawner.trialMarkers[chooseDisplay] == 3)
                {
                    Spawner.StartTrial(Spawner.fixedTrials[2]);
                    Debug.Log("Trial: " + countTrialBlock + " Of: " + maxTrials);
                    return;
                }

                if (Spawner.trialMarkers[chooseDisplay] == 4)
                {
                    Spawner.StartTrial(Spawner.fixedTrials[3]);
                    Debug.Log("Trial: " + countTrialBlock + " Of: " + maxTrials);
                    return;
                }

                if (Spawner.trialMarkers[chooseDisplay] == 5)
                {
                    Spawner.StartTrial(Spawner.fixedTrials[4]);
                    Debug.Log("Trial: " + countTrialBlock + " Of: " + maxTrials);
                    return;
                }

                if (Spawner.trialMarkers[chooseDisplay] == 6)
                {
                    Spawner.StartTrial(Spawner.fixedTrials[5]);
                    Debug.Log("Trial: " + countTrialBlock + " Of: " + maxTrials);
                    return;
                }

                if (Spawner.trialMarkers[chooseDisplay] == 7)
                {
                    Spawner.StartTrial(Spawner.fixedTrials[6]);
                    Debug.Log("Trial: " + countTrialBlock + " Of: " + maxTrials);
                    return;
                }

                if (Spawner.trialMarkers[chooseDisplay] == 8)
                {
                    Spawner.StartTrial(Spawner.fixedTrials[7]);
                    Debug.Log("Trial: " + countTrialBlock + " Of: " + maxTrials);
                    return;
                }

                if (Spawner.trialMarkers[chooseDisplay] == 9)
                {
                    Spawner.GenerateRandomDisplay(4, Random.Range(1, Spawner.numObjects / 2));
                    Debug.Log("Trial: " + countTrialBlock + " Of: " + maxTrials);
                    return;
                }

                if (Spawner.trialMarkers[chooseDisplay] == 10)
                {
                    Spawner.GenerateRandomDisplay(2,
                        Random.Range(Spawner.numObjects / 2, Spawner.numObjects - 1));
                    Debug.Log("Trial: " + countTrialBlock + " Of: " + maxTrials);
                    return;
                }

                if (Spawner.trialMarkers[chooseDisplay] == 11)
                {
                    Spawner.GenerateRandomDisplay(2, Random.Range(1, Spawner.numObjects / 2));
                    Debug.Log("Trial: " + countTrialBlock + " Of: " + maxTrials);
                    return;
                }

                if (Spawner.trialMarkers[chooseDisplay] == 12)
                {
                    Spawner.GenerateRandomDisplay(4,
                        Random.Range(Spawner.numObjects / 2, Spawner.numObjects - 1));
                    Debug.Log("Trial: " + countTrialBlock + " Of: " + maxTrials);
                }
            }
            else
            {
                InterTrialInterBlock.StartInterBlockFixation(Spawner.centerPos, Spawner.numCircles);
                Debug.Log("<color=green>Block Number: </color>" + countBlock + "<color=green> finished </color>.");

                countBlock++;
                countTrialBlock = 1;
                chooseDisplay = 0;
            }
        }
    }
}