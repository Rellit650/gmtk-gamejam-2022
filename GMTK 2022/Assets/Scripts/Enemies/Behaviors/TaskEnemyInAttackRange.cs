using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TaskEnemyInAttackRange : Node
{
    private Transform _transform;
    //used to check only in enemy layer and nothing else
    private static int _enemyLayerMask = 1 << 7;
    private float _attackRange;

    //Constructor
    public TaskEnemyInAttackRange(Transform trans,float attRange)
    {
        _transform = trans;
        _attackRange = attRange;
    }
    public override NodeState Evaluate()
    {
        object t = GetData("target");
        if(t == null)
        {
            state = NodeState.FAILURE;
            return state;
        }

        Transform target = (Transform)t;
        if(Vector3.Distance(_transform.position, target.position) <= _attackRange)
        {
            Debug.Log("Attack RANGE!");
            state = NodeState.SUCCESS;
            return state;
        }

        state = NodeState.FAILURE;
        return state;
    }
}
