using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace NodeUML
{
    public static class SaveUtility
    {
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
}
