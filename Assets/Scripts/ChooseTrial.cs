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

    public Spawner spawner;
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

        for (var i = 0; i < spawner.trialMarkers.Count; i++)
        {
            if (_tmp == spawner.trialMarkers[i])
            {
                Debug.Log("<color=red>Repetition encountered. New shuffle ...</color>");
                ShuffleTrialOrder(spawner.trialMarkers);
                return;
            }

            _tmp = spawner.trialMarkers[i];
        }

        ChooseNextTrial();
    }

    // Execute methods in shuffled order & check for trials and blocks
    public void ChooseNextTrial()
    {
        maxTrials = spawner.trialMarkers.Count;

        if (countBlock <= maxBlock)
        {
            if (countTrialBlock <= maxTrials)
            {
                if (spawner.trialMarkers[chooseDisplay] == 1)
                {
                    spawner.StartTrial(spawner.fixedTrials[0]);
                    Debug.Log("Trial: " + countTrialBlock + " Of: " + maxTrials);
                    Debug.Log("Fixed Trial 1.");
                    return;
                }

                if (spawner.trialMarkers[chooseDisplay] == 2)
                {
                    spawner.StartTrial(spawner.fixedTrials[1]);
                    Debug.Log("Trial: " + countTrialBlock + " Of: " + maxTrials);
                    return;
                }

                if (spawner.trialMarkers[chooseDisplay] == 3)
                {
                    spawner.StartTrial(spawner.fixedTrials[2]);
                    Debug.Log("Trial: " + countTrialBlock + " Of: " + maxTrials);
                    return;
                }

                if (spawner.trialMarkers[chooseDisplay] == 4)
                {
                    spawner.StartTrial(spawner.fixedTrials[3]);
                    Debug.Log("Trial: " + countTrialBlock + " Of: " + maxTrials);
                    return;
                }

                if (spawner.trialMarkers[chooseDisplay] == 5)
                {
                    spawner.StartTrial(spawner.fixedTrials[4]);
                    Debug.Log("Trial: " + countTrialBlock + " Of: " + maxTrials);
                    return;
                }

                if (spawner.trialMarkers[chooseDisplay] == 6)
                {
                    spawner.StartTrial(spawner.fixedTrials[5]);
                    Debug.Log("Trial: " + countTrialBlock + " Of: " + maxTrials);
                    return;
                }

                if (spawner.trialMarkers[chooseDisplay] == 7)
                {
                    spawner.StartTrial(spawner.fixedTrials[6]);
                    Debug.Log("Trial: " + countTrialBlock + " Of: " + maxTrials);
                    return;
                }

                if (spawner.trialMarkers[chooseDisplay] == 8)
                {
                    spawner.StartTrial(spawner.fixedTrials[7]);
                    Debug.Log("Trial: " + countTrialBlock + " Of: " + maxTrials);
                    return;
                }

                if (spawner.trialMarkers[chooseDisplay] == 9)
                {
                    spawner.GenerateRandomDisplay(4, Random.Range(1, spawner.numObjects / 2));
                    Debug.Log("Trial: " + countTrialBlock + " Of: " + maxTrials);
                    return;
                }

                if (spawner.trialMarkers[chooseDisplay] == 10)
                {
                    spawner.GenerateRandomDisplay(2,
                        Random.Range(spawner.numObjects / 2, spawner.numObjects - 1));
                    Debug.Log("Trial: " + countTrialBlock + " Of: " + maxTrials);
                    return;
                }

                if (spawner.trialMarkers[chooseDisplay] == 11)
                {
                    spawner.GenerateRandomDisplay(2, Random.Range(1, spawner.numObjects / 2));
                    Debug.Log("Trial: " + countTrialBlock + " Of: " + maxTrials);
                    return;
                }

                if (spawner.trialMarkers[chooseDisplay] == 12)
                {
                    spawner.GenerateRandomDisplay(4,
                        Random.Range(spawner.numObjects / 2, spawner.numObjects - 1));
                    Debug.Log("Trial: " + countTrialBlock + " Of: " + maxTrials);
                    return;
                }
            }
            else
            {
                Debug.Log("<color=green>Block Number: </color>" + countBlock + "<color=green> finished </color>.");
                //Spawner.startInterBlockFixation();

                countBlock++;
                countTrialBlock = 1;
                chooseDisplay = 0;
            }
        }
    }
}