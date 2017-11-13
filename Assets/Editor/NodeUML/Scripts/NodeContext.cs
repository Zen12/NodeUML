using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeUML
{
    public class NodeContext
    {
        public IdHandler idHandler;
        public System.Action<int, ulong> OnMakeRelation;
        public System.Action<int> OnMakeRelationToClass;
        public System.Action<int, ulong> OnDeleteField;
        public System.Action<int> OnDeleteClass;

        public SnapShot currentSnapeShot;

        public NodeContext(IdHandler idHandler, System.Action<int, ulong> OnMakeRelation, System.Action<int> OnMakeRelationToClass,
                           System.Action<int, ulong> OnDeleteField, System.Action<int> OnDeleteClass)
        {
            this.idHandler = idHandler;
            this.OnMakeRelation = OnMakeRelation;
            this.OnMakeRelationToClass = OnMakeRelationToClass;
            this.OnDeleteField = OnDeleteField;
            this.OnDeleteClass = OnDeleteClass;
        }

        public bool IsClassInCurrentContext(int id)
        {
            return currentSnapeShot.classListID.Contains(id);
        }

    }
}
