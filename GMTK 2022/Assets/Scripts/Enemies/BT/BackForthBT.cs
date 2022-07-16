using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class BackForthBT : BT
{
    public UnityEngine.Transform[] waypoints;

    public static float speed = 4f;
    public static float fovRange = 10f;

    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new TaskEnemyInFOV(this.transform),
                new TaskMoveToTarget(this.transform),
            }),
            new TaskPatrol(transform,waypoints),
        });

        return root;
    }
}
