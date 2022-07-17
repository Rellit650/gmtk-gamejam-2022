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
             
                Vector3 lookDir3D = target.transform.position - _transform.position;
                _transform.Translate(lookDir3D.normalized * _speed * Time.deltaTime);
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
