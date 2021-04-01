using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

//this class is what does the functionality of saving and loading
public static class SaveMan
{
    public static void SaveScore(StatLvlHolder statHolder)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/savedstats.law";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData playerData = new PlayerData(statHolder);

        formatter.Serialize(stream, playerData);
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/savedstats.law";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogWarning("Save file not found. This is fine if you haven't saved before");
            return null;
        }
    }
    public static void DeleteSave()
    {
        string path = Application.persistentDataPath + "/savedstats.law";
        if (File.Exists(path))
        {
            File.Delete(path);
        }
        else
            Debug.LogWarning("Save file not found. This is fine if you haven't saved before");
    }
}
