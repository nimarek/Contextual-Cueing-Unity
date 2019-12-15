using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class TrackBehavioralData : MonoBehaviour
{
    private readonly List<string[]> rowData = new List<string[]>();
    public SubInfo SubInfo;
    public Timer Timer;
    public InterTrialInterBlock InterTrialInterBlock;

    public void Start()
    {
        SubInfo = new SubInfo();
        writeHeaderRT();
    }

    private string getBehavPath()
    {
        return Application.dataPath + "/Data/" + "sub-" + FindObjectOfType<SubInfo>().subID + "_data.csv";
    }

    public void writeHeaderRT()
    {
        var rowDataTemp = new string[14];
        rowDataTemp[0] = "sub-ID";
        rowDataTemp[1] = "gender";
        rowDataTemp[2] = "corrected";
        rowDataTemp[3] = "age";
        rowDataTemp[4] = "block";
        rowDataTemp[5] = "trialNumberGlobal";
        rowDataTemp[6] = "trialNumberBlock";
        rowDataTemp[7] = "trialType";
        rowDataTemp[8] = "timeStampGlobal";
        rowDataTemp[9] = "rt";
        rowDataTemp[10] = "correctResponse";
        rowDataTemp[11] = "subResponse";
        rowDataTemp[12] = "targetPosition";
        rowData.Add(rowDataTemp);

        writeBehavData();
    }

    public void saveTrial()
    {
        var rowDataTemp = new string[14];
        rowDataTemp[0] = FindObjectOfType<SubInfo>().subID;
        rowDataTemp[1] = FindObjectOfType<SubInfo>().gender;
        rowDataTemp[2] = FindObjectOfType<SubInfo>().corrected;
        rowDataTemp[3] = FindObjectOfType<SubInfo>().age;
        rowDataTemp[4] = FindObjectOfType<ChooseTrial>().countBlock.ToString();
        rowDataTemp[5] = FindObjectOfType<ChooseTrial>().countTrialGlobal.ToString();
        rowDataTemp[6] = FindObjectOfType<ChooseTrial>().countTrialBlock.ToString();
        rowDataTemp[7] = FindObjectOfType<SubjectInput>().isFixedTrial.ToString(); // true for fixed, false for rnd
        rowDataTemp[8] = FindObjectOfType<Timer>().globalTime.ToString();
        rowDataTemp[9] = FindObjectOfType<Timer>().currentTime.ToString();
        rowDataTemp[10] = FindObjectOfType<Spawner>().flipTarget.ToString();
        rowDataTemp[11] =
            FindObjectOfType<Spawner>().correctResponse.ToString(); // true for correct, false for incorrect
        rowDataTemp[12] = FindObjectOfType<Spawner>().saveTargetPosition.ToString();
        rowData.Add(rowDataTemp);

        writeBehavData();
    }

    private void writeBehavData()
    {
        var output = new string[rowData.Count][];

        for (var i = 0; i < output.Length; i++) output[i] = rowData[i];

        var length = output.GetLength(0);
        var delimiter = "\t";

        var sb2 = new StringBuilder();

        for (var index = 0; index < length; index++)
            sb2.AppendLine(string.Join(delimiter, output[index]));

        var filePath = getBehavPath();

        var outStream = File.CreateText(filePath);
        outStream.WriteLine(sb2);
        outStream.Close();
    }
}