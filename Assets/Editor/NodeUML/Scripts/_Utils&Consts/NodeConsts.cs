using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeUML
{
    public static class NodeConsts
    {
        #region folders

        public const string FullResourcesFolder = "/Editor Default Resources/" + ResourcesFolder;
        public const string ResourcesFolder = "NodeUML";
        public const string CLASS_DATA_FILE = "Clases.json";
        public const string SEQUENCE_DATA_FILE = "Sequences.json";
        public const string USECASE_DATA_FILE = "Sequences.json";
        public const string ID_HANDLER = "IdHandler.json";

        #endregion

        #region NodeView Size

        public const int NodePosX = 10;
        public const int NodePosY = 10;
        public const int NodeWith = 130;
        public const int NodeHeight = 200;
        public static readonly Rect NodeTransform = new Rect(NodePosX, NodePosY, NodeWith, NodeHeight);

        #endregion

        #region Resources

        public const string RESOURCE_CONFIG = ResourcesFolder + "/ResourceConfig.asset";

        #endregion
    }
      
}
