using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TaskEnemyInFOV : Node
{
    private Transform _transform;
    //used to check only in enemy layer and nothing else
    private static int _enemyLayerMask = 1 << 7;
    private float _fovRange;

    //Constructor
    public TaskEnemyInFOV(Transform trans,float fovRange)
    {
        _transform = trans;
        _fovRange = fovRange;
    }
    public override NodeState Evaluate()
    {
        object t = GetData("target");
        if(t == null)
        {
            //Collision check within a sphere
            Collider[] cols = Physics.OverlapSphere(
                _transform.position, _fovRange, _enemyLayerMask);

            if(cols.Length > 0)
            {
                parent.parent.SetData("target", cols[0].transform);
                state = NodeState.SUCCESS;              
                return state;
            }
            state = NodeState.FAILURE;
            return state;
        }

        //clear target, this is so if player gets outside view it'll reset
        parent.parent.ClearData("target");
        state = NodeState.RUNNING;
        return state;
    }
}
