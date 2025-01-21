using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
[Serializable]
public class LogData
{
    public List<string> shareLog;
    public List<string> changeLog;
    public List<string> changedData;
    
}
public class Logging : MonoBehaviour
{
    public int[,] mediaBeforeList = new int[20, 4];
    public MediaEntryManager mediaEntryManager;
    public LogData logData = new LogData();
    void Start()
    {
        logData.shareLog = new List<string>();
        logData.changeLog = new List<string>();
        for (int i = 0; i < mediaEntryManager.mediaList.Count; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                mediaBeforeList[i,j] = mediaEntryManager.mediaList[i].relation[j];
            }
        }
        
    }

    public void Share_Origin(string name)
    {
        logData.shareLog.Add(name + " is shared in original");
    }
    public void Share_BaseLine(string name)
    {
        logData.shareLog.Add(name + " is shared in baseline");
    }
    public void Share_Our(string name)
    {
        logData.shareLog.Add(name + " is shared in our way");
    }
    public void Set_Relation(string name, RelationType type, int num)
    {
        logData.changeLog.Add(name + " " + type.ToString() + " set to " + num);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            printTracking();
        }
    }
    public void printTracking()
    {
        for(int i=0;i<mediaEntryManager.mediaList.Count;i++)
        {
            for(int j = 0;j<4;j++)
            {
                if (mediaEntryManager.mediaList[i].relation[j] != mediaBeforeList[i, j])
                {
                    logData.changedData.Add(mediaEntryManager.mediaList[i].name + " " + MediaEntry.GetRelationTypeFromIndex(j).ToString() + " " + mediaEntryManager.mediaList[i].relation[j]);
                }
            }
        }
        // ��ȡ��ǰ���ں�ʱ��
        string dateTime = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");

        // �����ļ���
        string fileName = $"HandPositions_{dateTime}.json";

        // ��ȡUnity��Ŀ��PersistentDataPath��Ȼ������Զ����trackLog�ļ���·��
        string logFolderPath = Path.Combine(Application.persistentDataPath, "trackLog");
        // ȷ��trackLog�ļ��д���
        if (!Directory.Exists(logFolderPath))
        {
            Directory.CreateDirectory(logFolderPath);
        }

        // �������ļ�·��
        string filePath = Path.Combine(logFolderPath, fileName);

        // ��JSON�ַ���д���ļ�
        File.WriteAllText(filePath, JsonUtility.ToJson(logData));

        Debug.Log("Hand positions have been saved as a JSON file in the trackLog folder.");
    }
}
