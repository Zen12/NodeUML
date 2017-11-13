using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeUML
{
    [System.Serializable]
    public class SnapShot
    {
        public int ID;
        [SerializeField]
        public List<int> classListID;
        public string snapShotName;

        private NodeContext context;

        public SnapShot(NodeContext context, string name)
        {
            this.context = context;
            ID = context.idHandler.GetSnapShotID();
            classListID = new List<int>();
            this.snapShotName = name;
        }

        public void AddClass(int ID)
        {
            if (!classListID.Contains(ID))
            {
                classListID.Add(ID);
            }
        }

        public void RemoveClass(int ID)
        {
            classListID.RemoveAll((int obj) => obj == ID);
        }
    }
}
