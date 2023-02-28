using FlaxEngine;

namespace ActorAI.BehaviorTree
{

    public class ActorAI_root : Tree
    {
        public bool debugDraw = false;
        public Transform[] waypoints;
        
        public static float speed = 200.0f;
        public static Actor actor;

        protected override Node SetupTree()
        {
            actor = Actor;
            Node root = new TaskPatrol(waypoints);
            return root;
        }

        protected override void DebugDrawWaypointItems()
        {
            if (debugDraw)
            {
                foreach (var wp in waypoints)
                {
                    DebugDraw.DrawSphere(new BoundingSphere(wp.Translation, 20.0f), Color.Red);
                }
            }
        }

    }

}
