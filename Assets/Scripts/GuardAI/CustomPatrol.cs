using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

using UnityEngine.AI;
using UnityEngine.UIElements;

namespace BehaviorTree
{
    public class CustomPatrol : Node
    {
        private Transform _transform;
        private NavMeshAgent _navMeshAgent;
        private Animator _animator;
        private Transform[] _waypoints;
        private int _currentWaypointIndex = 0;

        private float _waitTime = 1f;
        private float _waitCounter = 0f;
        private bool _waiting = true;
        public CustomPatrol(Transform transform, Transform[] waypoints)
        {
            _transform = transform;
            _animator = _transform.GetComponent<Animator>();
            _waypoints = waypoints;
            _navMeshAgent = _transform.GetComponent<NavMeshAgent>();
        }

        public override NodeState Evaluate()
        {
            // this if statement is to make the guard wait a second before going to another waypoint
            if (_waiting)
            {
                _waitCounter += Time.deltaTime;
                if (_waitCounter >= _waitTime)
                {
                    _waiting = false;
                    _animator.SetBool("Walking", true);
                }
            }
            else
            {
                Transform wp = _waypoints[_currentWaypointIndex]; // get the next waypoint where it has to go
                Vector3 direction = (wp.position - _transform.position).normalized;
                float distance = Vector3.Distance(_transform.position, wp.position);
                if (distance < 0.1f) // if it's on the waypoint
                {
                    _transform.position = wp.position; //place npc exactly on waypoint
                    _waitCounter = 0f;
                    _waiting = true;

                    //used mod on next line to make it so that it never goes above the number of waypoints
                    _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length; 
                                                                                           
                    _animator.SetBool("Walking", false); // stop walking
                }
                else
                {
                    _navMeshAgent.SetDestination(wp.position);

                    /*Vector3 move = direction * _navMeshAgent.speed * Time.deltaTime; // Calculate the movement vector
                    _navMeshAgent.Move(move); // Move the agent
                    _transform.LookAt(wp.position); // Rotate to face the target waypoint*/

                    /*Quaternion rotation;
                    Vector3 targetPos = ComputeDestination(90, 0.05f, _transform.position, _transform.forward); ;

                    rotation = Quaternion.LookRotation(targetPos - _transform.position);

                    //_transform.position = Vector3.MoveTowards(_transform.position, wp.position, GuardBT.speed * Time.deltaTime);
                    var turnTowardNavSteeringTarget = _navMeshAgent.steeringTarget;

                    Vector3 direction = (turnTowardNavSteeringTarget - _transform.position).normalized;
                    Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                    _transform.rotation = Quaternion.Slerp(_transform.rotation, lookRotation, Time.deltaTime * 5);

                    //_transform.LookAt(wp.position);
                    _navMeshAgent.isStopped = false;
                    _navMeshAgent.enabled = true;
                    _navMeshAgent.Move(_transform.forward * Time.deltaTime);*/
                    //_navMeshAgent.SetDestination(wp.position);

                }
            }

            state = NodeState.RUNNING;
            return state;
        }


    }
}
