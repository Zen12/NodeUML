using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveUtility
{
    //TODO tested it on windows
    public static void SaveInProject(string data, string path)
    {
        path = Application.dataPath + path;
        if (File.Exists(path))
        {
            File.Delete(path);
        }
        File.WriteAllText(path, data);
    }

}
