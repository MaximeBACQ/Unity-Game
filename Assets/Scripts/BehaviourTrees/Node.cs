using System.Collections;
using System.Collections.Generic;

//make it a namespace so that we can easily use it everywhere in the project
namespace BehaviorTree
{
    // all possible states of a node
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
        protected List<Node> children = new List<Node>();

        private Dictionary<string, object> _dataContext = new Dictionary<string, object>();

        //constructors

        //empty one is useful if we want a node that doesn't take a list of nodes as a parameter
        public Node()
        {
            parent = null;
        }
        public Node(List<Node> children)
        {
            //for each child, attach it as a children to the current node
            foreach (Node child in children)
                _Attach(child);
        }

        //method to attach a node to the current node
        private void _Attach(Node node)
        {
            node.parent = this;
            children.Add(node);
        }

        //virtual because we want every node's child to be able to implement it it's way
        public virtual NodeState Evaluate() => NodeState.FAILURE;

        //will be used to attach data to npc's target
        public void SetData(string key, object value)
        {
            _dataContext[key] = value;
        }

        //retrieve the useful data of the future targets
        public object GetData(string key)
        {
            object value = null;
            //if there's data attached to key (in the node)
            if (_dataContext.TryGetValue(key, out value))
                return value;

            //check the same but everywhere in the branch
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
    }

}