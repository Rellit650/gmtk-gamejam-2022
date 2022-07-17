using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BehaviorTree 
{
    public enum NodeState
    {
        RUNNING,
        SUCCESS,
        FAILURE
    }

    public class Node
    {
        protected NodeState state;

        public Node parent;
        protected List<Node> toddlers = new List<Node>();


        public Node() { parent = null; }

        public Node(List<Node>toddlers)
        {
            foreach(Node tod in toddlers)
            {
                _Attach(tod);
            }
        }

        private void _Attach(Node node)
        {
            node.parent = this;
            toddlers.Add(node);

        }

        public virtual NodeState Evaluate() => NodeState.FAILURE;

        private Dictionary<string, object> _dataContext =
            new Dictionary<string, object>();

        public void SetData(string key, object value)
        {
            _dataContext[key] = value;
        }

        public object GetData(string key)
        {
            object value = null;
            if (_dataContext.TryGetValue(key, out value))
                return value;

            Node node = parent;
            while (node != null)
            {
                value = node.GetData(key);
                if (value != null)
                    return value;
                node = node.parent;
            }
            return null;
        }

        public bool ClearData(string key)
        {
            if (_dataContext.ContainsKey(key))
            {
                _dataContext.Remove(key);
                return true;
            }

            Node node = parent;
            while (node != null)
            {
                bool cleared = node.ClearData(key);
                if (cleared)
                    return true;
                node = node.parent;
            }
            return false;
        }
    }

}
