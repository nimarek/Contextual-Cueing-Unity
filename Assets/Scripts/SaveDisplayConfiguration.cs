using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class SaveDisplayConfiguration : MonoBehaviour
{
    private readonly List<string[]> rowData = new List<string[]>();
    public Spawner Spawner;
    public Trial Trial;
    public SubjectInput SubjectInput;
    public InterTrialInterBlock InterTrialInterBlock;
    public SubInfo SubInfo;

    public void Start()
    {
        WriteHeader();
    }

    private string GetDataConfigPath()
    {
        return Application.dataPath + "/Data/" + "sub-" + FindObjectOfType<SubInfo>().subID + "_trialConfiguration.csv";
    }

    public void WriteHeader()
    {
        var rowDataTemp = new string[4];
        rowDataTemp[0] = "sub-ID";
        rowDataTemp[1] = "trialNumberGlobal";
        rowDataTemp[2] = "targetCircle";
        rowDataTemp[3] = "targetPosition";

        rowData.Add(rowDataTemp);

        WriteTrialConfiguration();
    }

    public void SaveTrialConfiguration()
    {
        var rowDataTemp = new string[4];
        rowDataTemp[0] = FindObjectOfType<SubInfo>().subID;
        rowDataTemp[1] = FindObjectOfType<ChooseTrial>().countTrialGlobal.ToString();
        rowDataTemp[2] = Spawner.chooseCircle.ToString();
        rowDataTemp[3] = Spawner.saveTargetPosition.ToString();

        rowData.Add(rowDataTemp);

        WriteTrialConfiguration();
    }

    private void WriteTrialConfiguration()
    {
        var output = new string[rowData.Count][];

        for (var i = 0; i < output.Length; i++) output[i] = rowData[i];

        var length = output.GetLength(0);
        var delimiter = "\t";

        var sb2 = new StringBuilder();

        for (var index = 0; index < length; index++)
            sb2.AppendLine(string.Join(delimiter, output[index]));

        var filePath = GetDataConfigPath();

        var outStream = File.CreateText(filePath);
        outStream.WriteLine(sb2);
        outStream.Close();
    }
}