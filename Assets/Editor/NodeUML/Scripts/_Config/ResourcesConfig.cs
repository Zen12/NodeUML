using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace NodeUML
{
    [CreateAssetMenu(fileName = "ResourceConfig", menuName = "NodeUML/Config", order = 1)]
    public class ResourcesConfig : ScriptableObject
    {
        public Texture actor;

        private static ResourcesConfig instance;

        public static ResourcesConfig GetInstance()
        {
            if (instance == null)
            {
                var o = EditorGUIUtility.Load(NodeConsts.RESOURCE_CONFIG);
                instance = (ResourcesConfig)o;
            }
            return instance;
        }

    }
}