using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor
{
    public int ID;
    public string actorName;
    public List<int> useCasesID;

    public Actor(string actorName)
    {
        this.actorName = actorName;
    }
}
