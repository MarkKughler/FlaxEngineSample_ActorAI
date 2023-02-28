using System.Collections.Generic;

namespace ActorAI.BehaviorTree
{

    public class Sequence : Node
    {
        public Sequence() : base() { }
        public Sequence(List<Node> children) : base(children) { }

        public override eNodeState Evaluate()
        {
            bool anyChildIsRunning = false;
            foreach(Node node in children)
            {
                switch(node.Evaluate())
                {
                    case eNodeState.FAILURE:
                        state = eNodeState.FAILURE;
                        return state;

                    case eNodeState.SUCCESS:
                        continue;

                    case eNodeState.RUNNING:
                        anyChildIsRunning = true;
                        continue;

                    default:
                        state = eNodeState.SUCCESS;
                        return state;
                }
            }
            state = anyChildIsRunning ? eNodeState.RUNNING : eNodeState.SUCCESS;
            return state;
        }

    }

}
