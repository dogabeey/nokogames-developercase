using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMesh))]
public class WorkerEntity : Entity
{
    public ObjectLayoutGroup stackLayout;
    public int maxCarryAmount = 10;


}
