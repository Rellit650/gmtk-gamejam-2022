using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TaskAttack : Node
{
    private Transform _transform;
    private Transform _prevTarget;
    private BaseEnemy _enemy;

    private float _attackTime = 1f;
    private float _attackCounter = 0f;

    public TaskAttack(Transform trans) { _transform = trans; }
    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");

       if(target != _prevTarget)
        {
            _enemy = target.GetComponent<TestEnemy>();
            _prevTarget = target;
        }

        _attackCounter += Time.deltaTime;
        if(_attackCounter >= _attackTime)
        {
            _enemy.TakeDamage(10f, Vector2.zero);

            if (_enemy.CurrentHealth <= 0f)
                ClearData("target");
            else
                _attackCounter = 0f;
        }
            

      

        state = NodeState.RUNNING;
        return state;
    }
}
