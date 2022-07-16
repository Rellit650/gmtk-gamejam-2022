using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TaskEnemyInFOV : Node
{
    private Transform _transform;
    //used to check only in enemy layer and nothing else
    private static int _enemyLayerMask = 1 << 6;

    //Constructor
    public TaskEnemyInFOV(Transform trans){_transform = trans;}
    public override NodeState Evaluate()
    {
        object t = GetData("target");
        if(t == null)
        {
            //Collision check within a sphere
            Collider[] cols = Physics.OverlapSphere(
                _transform.position, BackForthBT.fovRange, _enemyLayerMask);

            if(cols.Length> 0)
            {
                Debug.Log("Collision: " + cols[0].transform.name);
            
                parent.parent.SetData("target", cols[0].transform);
                Debug.Log(((Transform)parent.parent.GetData("target")).name);
                state = NodeState.SUCCESS;
                return state;
            }
        }
        state = NodeState.FAILURE;
        return state;
    }
}
