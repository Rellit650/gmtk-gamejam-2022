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
            Vector2 pos;
            pos = _transform.position;
            Collider2D[] cols = Physics2D.OverlapCircleAll(
                pos, _fovRange, _enemyLayerMask);

            if(cols.Length > 0)
            {
                Vector3 lookDir3D = cols[0].transform.position - _transform.position;

                // normalize the vector: this makes the x and y components numerically
                // equal to the sine and cosine of the angle:
                lookDir3D.z = 0f;
                lookDir3D.Normalize();
                // get the basic angle:
                float ang = Mathf.Asin(lookDir3D.y) * Mathf.Rad2Deg;
                // fix the angle for 2nd and 3rd quadrants:
                if (lookDir3D.x < 0)
                {
                    ang = 180 - ang;
                }
                else // fix the angle for 4th quadrant:
                if (lookDir3D.y < 0)
                {
                    ang = 360 + ang;
                }

                _transform.rotation = Quaternion.Euler(0f, 0f, ang);

                parent.parent.SetData("target", cols[0].transform);
                state = NodeState.SUCCESS;              
                return state;
            }
            state = NodeState.FAILURE;
            return state;
        }

        //clear target, this is so if player gets outside view it'll reset
        parent.parent.ClearData("target");
        state = NodeState.FAILURE;
        return state;
    }
}
