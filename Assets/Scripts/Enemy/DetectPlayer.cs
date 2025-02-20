using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(
        name: "DetectPlayer",
        description: "Detects player when player is within a certain radius",
        category: "Action/Utility", //where it's placed in the create node
        story: "[Agent] detects [Player] within [Radius] unit(s)"//, //what is shown on the node itself
        //id: "f0cd1414cf8e67c47214e54fc922c793"
    )]

public partial class DetectPlayer : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Agent; //Blackboard Variable is a generic variable
    [SerializeReference] public BlackboardVariable<GameObject> Player;
    [SerializeReference] public BlackboardVariable<float> Radius = new(10f); //default val of 10

    //defines it's start behaviour
    protected override Status OnStart()
    {
        if ( Agent == null  || Player == null)
        {
            return Status.Failure;
        }
        else
        {
            return Status.Running;
        }

        //For status there is Unintitialized, Running, Success, Failure, Waiting and Interrupted (last two are only needed for custom control nodes)
        //return base.OnStart();
    }

    protected override Status OnUpdate()
    {
        //if reference is lost
        if (Agent == null || Player == null)
        {
            return Status.Failure;
        }

        //if the distance is within the radius. 
        if (Vector3.Distance(Agent.Value.transform.position, Player.Value.transform.position) < Radius.Value)
        {
            //success is returned to tell the parent node the result
            return Status.Success;
        }

        return Status.Running;
    }
}
