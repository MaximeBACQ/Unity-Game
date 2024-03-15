using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class CheckEnemyInFOVRange : Node
{
    // << shifts the bits of 1 to 6 bits on the left (1 = 0000000000001, 1<<6 = 000001000000)
    private static int _playerLayerMask = 1 << 6;

    private Transform _transform;
    private Animator _animator;

    public CheckEnemyInFOVRange(Transform transform)
    {
        _transform = transform;
        _animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        object t = GetData("target");
        // if we have a current target, automatically suceed and go outside the if
        if (t == null)
        {
            //check for a collision in the fovrange of the guard (collisions will only work on the player layer mask)
            Collider[] colliders = Physics.OverlapSphere(
                _transform.position, GuardBT.fovRange, _playerLayerMask);
            /*            Vector3 patrolBox = new Vector3(4.48f, 0, 6.91f);
                        Collider[] colliders = Physics.OverlapBox(
                            _waypointsCenter, patrolBox, _playerLayerMask);*/

            //if there is one
            if (colliders.Length > 0)
            {
                //store in the target slot of our dictionnary, in the root (which is 2 levels above so we use parent.parent)
                parent.parent.SetData("target", colliders[0].transform);     
                _animator.SetBool("Walking", true);
                state = NodeState.SUCCESS;
                return state;
            }

            state = NodeState.FAILURE;
            return state;
        }

        state = NodeState.SUCCESS;
        return state;
    }

}