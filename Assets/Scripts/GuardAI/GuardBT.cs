using System.Collections.Generic;
using BehaviorTree;

public class GuardBT : BTree
{
    public UnityEngine.Transform[] waypoints;
    public UnityEngine.Transform waypointsCenter;

    public static float speed = 2f;
    public static float fovRange = 6f;
    public static float attackRange = 2f;

    protected override Node SetupTree()
    {
        //selector then sequence : we give an order of priorities : here, patrolling is the least important action
            Node root = new Selector(new List<Node>
            {
                new AttackInRange(transform),
                new Sequence(new List<Node>
                {
                    new CheckEnemyInFOVRange(transform),
                    new GoToTarget(transform),
                }),
                //new TaskPatrol(transform, waypoints),
                new CustomPatrol(transform, waypoints),
            });

        /*var direction = new Vector3 { [capsuleCollider.direction] = 1 };
        var offset = capsuleCollider.height / 2 - capsuleCollider.radius;
        var localPoint0 = capsuleCollider.center - direction * offset;
        var localPoint1 = capsuleCollider.center + direction * offset;

        var point0 = transform.TransformPoint(localPoint0);
        var point1 = transform.TransformPoint(localPoint1);

        var r = transform.TransformVector(capsuleCollider.radius, capsuleCollider.radius, capsuleCollider.radius);
        var radius = Enumerable.Range(0, 3).Select(xyz => xyz == capsuleCollider.direction ? 0 : r[xyz])
            .Select(Mathf.Abs).Max();
        Collider[] colliders = Physics.OverlapCapsule(point0, point1, radius, _obstacleLayerMask);
        if (colliders.Length > 0)
        {
            Debug.Log("collision");
            root = new TaskPatrol(transform, waypoints);
        }*/

        return root;
    }
}