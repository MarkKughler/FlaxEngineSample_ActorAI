using FlaxEngine;

namespace ActorAI.BehaviorTree
{

    public abstract class Tree : Script
    {
        private Node _root = null;

        public override void OnStart()  { _root = SetupTree(); }

        public override void OnUpdate() { DebugDrawWaypointItems();}
        public override void OnFixedUpdate() { if (_root != null) _root.Evaluate(); }

        protected abstract Node SetupTree();
        protected abstract void DebugDrawWaypointItems();
    }

}
