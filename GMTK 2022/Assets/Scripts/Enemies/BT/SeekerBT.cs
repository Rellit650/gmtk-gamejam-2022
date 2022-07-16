using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class SeekerBT : BT
{
    public float speed = 4f;
    public float fovRange = 10f;
    public float attRange = 10f;

    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new TaskEnemyInAttackRange(transform,attRange),
                new TaskAttack(transform,
                GetComponent<TestEnemy>().GetAttacKSpd(),
                GetComponent<TestEnemy>().GetAttackDmg()),
            }),
            new Sequence(new List<Node>
            {
                new TaskEnemyInFOV(transform,fovRange),
                new TaskMoveToTarget(transform,speed),
            }),
        });

        return root;
    }


}
