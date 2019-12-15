using System.Collections.Generic;
using UnityEngine;

public class FisherYates : MonoBehaviour
{
    // Shuffle any given list with fisher-yates
    public void shuffleTrialConfiguration(List<int> trialConfiguration)
    {
        for (var i = trialConfiguration.Count - 1; i > 0; i--)
        {
            var rnd = Random.Range(0, i);
            var temp = trialConfiguration[i];

            trialConfiguration[i] = trialConfiguration[rnd];
            trialConfiguration[rnd] = temp;
        }
    }
}