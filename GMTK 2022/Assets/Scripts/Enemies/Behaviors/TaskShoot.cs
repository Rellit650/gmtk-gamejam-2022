using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TaskShoot : Node
{
    Transform _prevTarget;
    TestEnemy _enemy;
    SentryTurrentBT _sentryBT;

    private float _attackDamage = 1f;
    private float _attackTime = 1f;
    private float _attackCounter = 0f;

    public TaskShoot(Transform trans, float attackSpd, float attackDmg)
    {
        _sentryBT = trans.GetComponent<SentryTurrentBT>();
        _attackTime = attackSpd;
        _attackDamage = attackDmg;
    }
    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");

        if (target != _prevTarget)
        {
            _enemy = target.GetComponent<TestEnemy>();
            _prevTarget = target;
        }

        _attackCounter += Time.deltaTime;
        if (_attackCounter >= _attackTime)
        {
            GameObject bullet = _sentryBT.GetBullet();
            if(bullet != null)
            {
                bullet.transform.position = _sentryBT.barrelPos.position;
                bullet.transform.GetComponent<Rigidbody>().AddForce(
                target.position - bullet.transform.position * _sentryBT.bulletSpd);

            }
            
            _attackCounter = 0f;
            if (_enemy.GetHealth() <= 0f)
                ClearData("target");
        }

        state = NodeState.RUNNING;
        return state;
    }
}
