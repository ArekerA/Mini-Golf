using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveFileManager : MonoBehaviour
{
    private SaveFileData data = new SaveFileData();
    private string dataPath;
    private readonly string fileName = "SaveFile.txt";
    public void Awake()
    {
        dataPath = Path.Combine(Application.persistentDataPath, fileName);
    }
    public void Load()
    {
        if (File.Exists(dataPath))
        {
            using (StreamReader streamReader = File.OpenText(dataPath))
            {
                string jsonString = streamReader.ReadToEnd();
                data = JsonUtility.FromJson<SaveFileData>(jsonString);
            }
        }
        else
        {
            data.cameraSensitivity = 1;
            data.soundtrackVolume = 0;
            data.effectVolume = 0;
            Save();
        }
        StaticUI.cameraSensitivity = data.cameraSensitivity;
        StaticAudio.soundtrackVolume = data.soundtrackVolume;
        StaticAudio.effectVolume = data.effectVolume;
    }
    public void Save()
    {
        data.cameraSensitivity = StaticUI.cameraSensitivity;
        data.soundtrackVolume = StaticAudio.soundtrackVolume;
        data.effectVolume = StaticAudio.effectVolume;
        string jsonString = JsonUtility.ToJson(data);
        using (StreamWriter streamWriter = File.CreateText(dataPath))
        {
            streamWriter.Write(jsonString);
        }
    }
}
