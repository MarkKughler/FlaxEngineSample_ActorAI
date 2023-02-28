using FlaxEngine;

namespace ActorAI.BehaviorTree
{

    public class TaskPatrol : Node
    {
        private Transform[] _waypoints;
        private int   _currentWaypointIndex = 0;
        private float _waitTime = 1.0f; // in seconds
        private float _waitCounter = 0.0f;
        private bool  _waiting = false;

         
        public TaskPatrol(Transform[] waypoints) { _waypoints = waypoints; }

        public override eNodeState Evaluate()
        {
            if (_waiting)
            {
                _waitCounter += Time.DeltaTime;
                if (_waitCounter < _waitTime) _waiting = false;
            }
            else
            {
                Transform wp = _waypoints[_currentWaypointIndex];
                float distanceToTarget = Vector3.Distance(ActorAI_root.actor.Transform.Translation, wp.Translation);
                if(distanceToTarget < 10.0f/*cm*/)
                {
                    _waitCounter = 0.0f;
                    _waiting = true;
                    ActorAI_root.actor.AddMovement(ActorAI_root.actor.Transform.Forward * distanceToTarget); // correct translation to target end
                    _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length;                 // advance index to next waypoint or loop
                }
                else
                {
                    Vector3 moveDelta = Vector3.Normalize(wp.Translation - ActorAI_root.actor.Transform.Translation) * ActorAI_root.speed * Time.DeltaTime;
                    ActorAI_root.actor.AddMovement(moveDelta);
                    ActorAI_root.actor.LookAt(wp.Translation); // todo : sLerp into it to improve turning visual quality
                    // todo : an about face turn flips the up axis. (ie. ping pong between two waypoints)
                    //        apply physics body constraints or track a guided look target killing two birds with one stone
                }
            }
            state = eNodeState.RUNNING;
            return state;
        }

    }

}
