using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TaskAttack : Node
{
    private Transform _transform;
    private Transform _prevTarget;
    private PlayerMovement _enemy;

    private float _attackDamage = 1f;
    private float _attackTime = 1f;
    private float _attackCounter = 0f;

    public TaskAttack(Transform trans) { _transform = trans; }
    public TaskAttack(Transform trans, float attackTime,float attackDamage) 
    { 
        _transform = trans;
        _attackTime = attackTime;
        _attackDamage = attackDamage;
    }
    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");

        if(!target.CompareTag("Player"))
        {
            state = NodeState.FAILURE;
            return state;
        }
        if (target != _prevTarget)
        {
            _enemy = target.GetComponent<PlayerMovement>();
            _prevTarget = target;
        }

        _attackCounter += Time.deltaTime;
        if(_attackCounter >= _attackTime)
        {
            _enemy.TakeDamage(_attackDamage);
            _attackCounter = 0f;
            if (_enemy.GetHealth() <= 0f)
                ClearData("target");
        }
            

      

        state = NodeState.RUNNING;
        return state;
    }
}
