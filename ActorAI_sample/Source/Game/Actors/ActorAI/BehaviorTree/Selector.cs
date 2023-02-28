using System.Collections.Generic;

namespace ActorAI.BehaviorTree
{

    public class Selector : Node
    {
        public Selector() : base() { }
        public Selector(List<Node> children) : base(children) { }

        public override eNodeState Evaluate()
        {
            foreach (Node node in children)
            {
                switch (node.Evaluate())
                {
                    case eNodeState.FAILURE:
                        continue;

                    case eNodeState.SUCCESS:
                        state = eNodeState.SUCCESS;
                        return state;

                    case eNodeState.RUNNING:
                        state = eNodeState.RUNNING;
                        return state;

                    default:
                        continue;
                }
            }
            state = eNodeState.RUNNING; 
            return state;
        }

    }

}
