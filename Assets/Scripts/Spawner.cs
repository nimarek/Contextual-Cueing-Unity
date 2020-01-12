using System.Collections.Generic;
using UnityEngine;

/* Author: Nico Marek (2019). Sepcial thanks to Sophie Anteboth (https://github.com/S-Anteboth) for her patience.
 * This script is used to generate contextual cueing such displays. Starting point for this implementation are the works
 * of Chun, M. M., & Jiang, Y. (1998). Both the old (i.e. all repeated configurations) and new (i.e. all random configurations)
 * condition are created here. This experiment can be used both on a conventional screen and with a virtual reality headset.
 */

public class Spawner : MonoBehaviour
{
    public static int TRIALSMAX = 8;
    public float angle;
    public Vector3 centerPosStart;
    public Vector3 centerPosInterTrial;
    private Quaternion centerRot;
    public int chooseCircle;

    public ChooseTrial chooseTrial;

    public bool correctResponse = true;
    public GameObject distractor;

    public List<Trial> fixedTrials = new List<Trial>();

    public bool flipTarget;
    public InterTrialInterBlock InterTrialInterBlock;
    public int numCircles;

    public int numObjects;
    public float radius;

    public int saveTargetPosition;
    public float scaleCircles;
    public float scaleDistractor;

    public float scaleTarget;
    public float spacingCircles;

    public SubInfo SubInfo;
    public SubjectInput SubjectInput;
    public GameObject Target;

    public Vector3 targetPos;
    public Timer Timer;

    public List<int> trialMarkers = new List<int>
        {1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9, 9, 9, 10, 10, 10, 10, 11, 11, 11, 11, 12, 12, 12, 12};

    // Generate data folder & fixed displays for the entire experiment
    private void Start()
    {
        for (var i = 0; i < TRIALSMAX; i++)
        {
            var fixedTrial = new Trial();
            fixedTrials.Add(fixedTrial);
        }
        
        System.IO.Directory.CreateDirectory(Application.dataPath + "/Data/" + "sub-" + SubInfo.subID);
        
        centerPosStart = new Vector3(numCircles, (numCircles / 2) + 2 , 0);
        centerPosInterTrial = new Vector3(numCircles, (numCircles / 2) + 2, 0);
        
        // Generate pseudo random display
        GenerateFixedDisplay(fixedTrials[0], 3, Random.Range(1, numObjects / 2));
        GenerateFixedDisplay(fixedTrials[1], 3, Random.Range(1, numObjects / 2));

        GenerateFixedDisplay(fixedTrials[2], 1, Random.Range(numObjects / 2, numObjects - 1));
        GenerateFixedDisplay(fixedTrials[3], 1, Random.Range(numObjects / 2, numObjects - 1));

        GenerateFixedDisplay(fixedTrials[4], 1, Random.Range(1, numObjects / 2));
        GenerateFixedDisplay(fixedTrials[5], 1, Random.Range(1, numObjects / 2));

        GenerateFixedDisplay(fixedTrials[6], 3, Random.Range(numObjects / 2, numObjects - 1));
        GenerateFixedDisplay(fixedTrials[7], 3, Random.Range(numObjects / 2, numObjects - 1));

        Timer = Timer.instance;
    }

    public void GenerateRandomDisplay(int targetChooseCircle, int randomRange)
    {
        var rndTrial = new Trial();

        SetBooleansRandom();

        rndTrial.isTargetFlipped = Random.value > 0.5f;
        rndTrial.targetChooseCircle = targetChooseCircle;
        rndTrial.targetChoosePosition = randomRange;

        saveTargetPosition = rndTrial.targetChoosePosition;

        rndTrial.PositionVoidLists = new List<List<int>>();
        rndTrial.RotationVoidLists = new List<List<int>>();

        for (var z = 0; z < numCircles; z++)
        {
            var trialConfig = new List<int> {1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};

            chooseTrial.ShuffleDisplayConfiguration(trialConfig);

            var RotationVoidList = new List<int>();

            for (var i = 0; i < numObjects; i++) RotationVoidList.Add(GenerateRotationsDistractor());

            rndTrial.PositionVoidLists.Add(trialConfig);
            rndTrial.RotationVoidLists.Add(RotationVoidList);
        }

        StartTrial(rndTrial);
    }

    public void GenerateFixedDisplay(Trial fixedTrial, int targetChooseCircle, int targetChoosePosition)
    {
        SetBooleansFixed();

        fixedTrial.isTargetFlipped = Random.value > 0.5f;
        fixedTrial.targetChooseCircle = targetChooseCircle;
        fixedTrial.targetChoosePosition = targetChoosePosition;

        saveTargetPosition = fixedTrial.targetChoosePosition;

        fixedTrial.PositionVoidLists = new List<List<int>>();
        fixedTrial.RotationVoidLists = new List<List<int>>();

        for (var i = 0; i <= chooseTrial.numFixedTrials; i++)
        for (var z = 0; z < numCircles; z++)
        {
            var trialConfig = new List<int> {1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};

            chooseTrial.ShuffleDisplayConfiguration(trialConfig);

            var RotationVoidList = new List<int>();

            for (var k = 0; k < numObjects; k++) RotationVoidList.Add(GenerateRotationsDistractor());

            fixedTrial.PositionVoidLists.Add(trialConfig);
            fixedTrial.RotationVoidLists.Add(RotationVoidList);
        }
    }

    public void StartTrial(Trial trial)
    {
        flipTarget = trial.isTargetFlipped;
        SubjectInput.isInterTrial = false;

        Timer.StartRecord();
        var center = transform.position;

        for (var i = 0; i < numCircles; i++)
        {
            var targetIsOnCircle = trial.targetChooseCircle == i;
            DrawCircle(center, targetIsOnCircle, trial, i);
            center.y += spacingCircles;
        }
    }

    public void SetBooleansFixed()
    {
        SubjectInput.isFixedTrial = true;
        SubjectInput.isInterTrial = false;
        correctResponse = true;
    }

    public void SetBooleansRandom()
    {
        SubjectInput.isFixedTrial = false;
        SubjectInput.isInterTrial = false;
        correctResponse = true;
    }

    public void DrawCircle(Vector3 center, bool targetIsOnCircle, Trial trial, int circle)
    {
        for (var i = 0; i < numObjects; i++)
        {
            var pos = CalculatePositionOnCircle(center, i * angle);
            var rot = Quaternion.LookRotation(pos - center);

            if (targetIsOnCircle && i == trial.targetChoosePosition)
            {
                InstantiateTarget(pos, rot);
            }
            else
            {
                var randomPositionVoidList = trial.PositionVoidLists[circle];
                var randomPositionVoid = randomPositionVoidList[i];

                var randomRotationVoidList = trial.RotationVoidLists[circle];
                var randomRotationVoid = randomRotationVoidList[i];

                InstantiateDistractor(randomPositionVoid, randomRotationVoid, pos, rot);
            }

            if (i == numObjects / 2)
            {
                centerPosStart = pos;
                centerRot = rot;
            }
        }
    }

    private int GenerateRotationsDistractor()
    {
        var rndRotateDistractor = Random.Range(0, 99);
        if (rndRotateDistractor >= 0 && rndRotateDistractor <= 25)
            return 0;
        if (rndRotateDistractor > 26 && rndRotateDistractor <= 50)
            return 90;
        if (rndRotateDistractor > 51 && rndRotateDistractor <= 75)
            return 180;
        return 270;
    }

    private void InstantiateDistractor(int randomPositionVoid, int randomRotationVoid, Vector3 pos, Quaternion rot)
    {
        if (randomPositionVoid == 1)
        {
            GameObject instanceOfDistractor = Instantiate(distractor, pos, rot, transform);
            instanceOfDistractor.transform.localScale = new Vector3(scaleDistractor, scaleDistractor, scaleDistractor);
            instanceOfDistractor.gameObject.tag = "Distractor";
            instanceOfDistractor.transform.Rotate(new Vector3(0, 0, randomRotationVoid));
        }
    }

    private void InstantiateTarget(Vector3 pos, Quaternion rot)
    {
        targetPos = pos;
        GameObject instanceOfTarget = Instantiate(Target, pos, rot, transform);
        if (flipTarget)
        {
            instanceOfTarget.transform.localScale = new Vector3(-1 * scaleTarget, scaleTarget, scaleTarget);
            instanceOfTarget.gameObject.tag = "Target";
        }
        else
        {
            instanceOfTarget.transform.localScale = new Vector3(scaleTarget, scaleTarget, scaleTarget);
        }
    }

    private Vector3 CalculatePositionOnCircle(Vector3 center, float ang)
    {
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad / scaleCircles);
        pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad / scaleCircles);
        pos.y = center.y;
        return pos;
    }

    public void DeleteAllChildren()
    {
        foreach (Transform child in transform) Destroy(child.gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}