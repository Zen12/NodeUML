using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeUML
{
    [System.Serializable]
    public class Sequence
    {

        private int ID;
        private string sequanceName;
        private Sequence.SequanceType type;

        public Sequence(int ID, string name, Sequence.SequanceType type)
        {
            this.ID = ID;
            sequanceName = name;
            this.type = type;
        }

        public enum SequanceType
        {
            actor,
            object_
        }
    }


}
