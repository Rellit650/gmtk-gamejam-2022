using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TaskMoveToTarget : Node
{
    private Transform _transform;
    private float _speed;

    public TaskMoveToTarget(Transform trans, float spd) 
    { 
        _transform = trans;
        _speed = spd;
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");
        if(target != null)
        {
            if (Vector3.Distance(_transform.position, target.position) > 0.01f)
            {
                _transform.position = Vector3.MoveTowards(
                    _transform.position,
                    target.position,
                    _speed * Time.deltaTime);
                _transform.LookAt(target.position);
            }
            state = NodeState.RUNNING;
            return state;
        }
        else
        {
            state = NodeState.FAILURE;
            return state;
        }
     

       
    }
}
