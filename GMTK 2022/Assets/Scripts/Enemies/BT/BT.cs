using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BehaviorTree 
{

    public abstract class BT : MonoBehaviour
    {

        private Node _root = null;

        protected virtual void Start()
        {
            _root = SetupTree();
        }

        private void Update()
        {
            if (_root != null)
                _root.Evaluate();
        }

        protected abstract Node SetupTree();

    }

    /*
      Sequence means every node and its children actions need to be a success
    otherwise itll be a failure. "and" logic gate
     */
    public class Sequence: Node
    {
        public Sequence() : base() { }
        public Sequence(List<Node> toddlers) : base(toddlers) { }

        public override NodeState Evaluate()
        {
            bool anyChildIsRunning = false;

            foreach(Node node in toddlers)
            {
                switch (node.Evaluate())
                {
                    case NodeState.FAILURE:
                        state = NodeState.FAILURE;
                        return state;
                    case NodeState.SUCCESS:
                        continue;
                    case NodeState.RUNNING:
                        anyChildIsRunning = true;
                        continue;
                    default:
                        state = NodeState.SUCCESS;
                        return state;
                }
            }
            state = anyChildIsRunning ? NodeState.RUNNING : NodeState.SUCCESS;
            return state;
        }
    }

    /*
     Runs sequentially through each child, however if a child has succeeded or is running we 
     return. "or" logic gate
    */
    public class Selector : Node
    {
        public Selector() : base() { }
        public Selector(List<Node> toddlers) : base(toddlers) { }

        public override NodeState Evaluate()
        {
            foreach (Node node in toddlers)
            {
                switch (node.Evaluate())
                {
                    case NodeState.FAILURE:
                        continue;
                    case NodeState.SUCCESS:
                        state = NodeState.SUCCESS;
                        return state;
                    case NodeState.RUNNING:
                        state = NodeState.RUNNING;
                        return state;
                    default:
                        continue;
                }
            }

            state = NodeState.FAILURE;
            return state;
        }

    }
}
