using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
/*
 * This script is used to track and save head movement data of every participant during trials. FixedUpdate() is
 * frame rate independent and therefore called on a regular basis. The head movement tracker is an empty GameObject
 * that needs to be a child of the camera.
 */
public class TrackHeadMotion : MonoBehaviour
{
    private readonly List<string[]> rowData = new List<string[]>();
    private readonly List<string[]> headDataTmp = new List<string[]>();
    public InterTrialInterBlock InterTrialInterBlock;
    public SubInfo SubInfo;
    public Timer Timer;

    public void Start()
    {
        WriteHeader();
    }
    
    private void FixedUpdate()
    {
        //Debug.Log("Global Time: " + FindObjectOfType<Timer>().globalTime.ToString("F3") + "Head Position: " + transform.position.ToString());
    }

    private string GetDataPath()
    {
        return Application.dataPath + "/Data/" + "sub-" + SubInfo.subID + "/sub-" + FindObjectOfType<SubInfo>().subID + "_head_data.csv";
    }

    public void WriteHeader()
    {
        var rowDataTemp = new string[6];
        rowDataTemp[0] = "sub-ID";
        rowDataTemp[1] = "sex";
        rowDataTemp[2] = "corrected";
        rowDataTemp[3] = "sub-ID";
        rowDataTemp[4] = "timeStampGlobal";
        rowDataTemp[5] = "headMotion";

        rowData.Add(rowDataTemp);
        WriteResponseData();
    }

    public void FindResponseVariables()
    {
        var rowDataTemp = new string[6];
        rowDataTemp[0] = FindObjectOfType<SubInfo>().subID;
        rowDataTemp[1] = FindObjectOfType<SubInfo>().sex;
        rowDataTemp[2] = FindObjectOfType<SubInfo>().corrected;
        rowDataTemp[3] = FindObjectOfType<SubInfo>().age;
        rowDataTemp[4] = FindObjectOfType<Timer>().globalTime.ToString("F3");
        rowDataTemp[5] = transform.position.ToString();
        rowData.Add(rowDataTemp);

        WriteResponseData();
    }

    private void WriteResponseData()
    {
        var output = new string[rowData.Count][];

        for (var i = 0; i < output.Length; i++) output[i] = rowData[i];

        var length = output.GetLength(0);
        var delimiter = "\t";

        var sb2 = new StringBuilder();

        for (var index = 0; index < length; index++)
            sb2.AppendLine(string.Join(delimiter, output[index]));

        var filePath = GetDataPath();

        var outStream = File.CreateText(filePath);
        outStream.WriteLine(sb2);
        outStream.Close();
    }
}
