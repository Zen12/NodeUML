using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeUML
{
    [System.Serializable]
    public class SnapShotUseCase
    {
        public int ID;
        [SerializeField]
        public List<int> actorID;
        public List<int> useCaseID;
        public string snapShotName;


        public SnapShotUseCase(string name)
        {
            ID = IdHandler.GetInstance().GetSnapShotID();
            actorID = new List<int>();
            this.snapShotName = name;
        }

        public void AddActor(int ID)
        {
            if (!actorID.Contains(ID))
            {
                actorID.Add(ID);
            }
        }

        public void RemoveActor(int ID)
        {
            actorID.RemoveAll((int obj) => obj == ID);
        }

        public void AddUseCase(int ID)
        {
            if (!useCaseID.Contains(ID))
            {
                useCaseID.Add(ID);
            }
        }

        public void RemoveUseCase(int ID)
        {
            useCaseID.RemoveAll((int obj) => obj == ID);
        }
    }
}
