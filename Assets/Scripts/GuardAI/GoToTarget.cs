using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
using UnityEngine.AI;

public class GoToTarget : Node
{
    private Transform _transform;
    //private CapsuleCollider capsuleCollider;
    private NavMeshAgent _navMeshAgent;

    public GoToTarget(Transform transform)
    {
        _transform = transform;
        //capsuleCollider = transform.GetComponent<CapsuleCollider>();
        _navMeshAgent = transform.GetComponent<NavMeshAgent>();
    }

    public override NodeState Evaluate()
    {

        Transform target = (Transform)GetData("target");

        if (Vector3.Distance(_transform.position, target.position) > 0.01f)
        {
            float clampedY = Mathf.Min(target.position.y - 1, 2); //clamped Y to ground so that npc doesn't fly
            _navMeshAgent.isStopped = true; // workaround to stop patrolling on navmesh
            _transform.position = Vector3.MoveTowards(
                _transform.position, new Vector3(target.position.x, clampedY, target.position.z), GuardBT.speed * Time.deltaTime);

            //workaround because npc used to rotate around Y axis when approaching with _transform.LookAt()
            Vector3 targetDirection = target.position - _transform.position;
            targetDirection.y = 0; // Remove Y component of direction to maintain upright orientation
            if (targetDirection != Vector3.zero)
            {
                //look in the target's direction
                Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
                //rotate from transform's rotation to target's rotation in given time
                _transform.rotation = Quaternion.Slerp(_transform.rotation, targetRotation, GuardBT.speed * Time.deltaTime);
            }
        }

        state = NodeState.RUNNING;
        return state;
    }

}
