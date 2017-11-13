using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeUML
{
    [System.Serializable]
    public class IdHandler
    {
        public int maxClassID = 0;
        public ulong nodeInfoID = 1;
        public int snapShotID = 1;

        public int GetClassID()
        {
            return maxClassID++;
        }

        public ulong GetNodeInfoItemID()
        {
            return nodeInfoID++;
        }

        public int GetSnapShotID()
        {
            return snapShotID++;
        }

    }
}
