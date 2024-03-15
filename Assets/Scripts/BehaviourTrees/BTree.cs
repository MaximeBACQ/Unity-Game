using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public abstract class BTree : MonoBehaviour
    {

        private Node root = null;

        /*private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Obstacle"))
            {
                //collision = true;
                Debug.Log("collision");
            }
        }*/

        protected void Start()
        {
            //each tree corresponding to an npc can now have it's own tree by overriding the SetupTree method
            root = SetupTree();
        }

        private void Update()
        {
            if (root != null)
                root.Evaluate(); // each node (gototarget, custompatrol, ...) will have their own way of evaluating
        }

        protected abstract Node SetupTree();

    }

}