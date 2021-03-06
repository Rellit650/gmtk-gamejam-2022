using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class BackForthBT : BT
{
    public UnityEngine.Transform[] waypoints;

    public float speed = 4f;
    public float fovRange = 10f;

    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new TaskEnemyInFOV(transform,fovRange),
                new TaskMoveToTarget(transform,speed),
            }),
            new TaskPatrol(transform,waypoints,speed),
        });

        return root;
    }
}
