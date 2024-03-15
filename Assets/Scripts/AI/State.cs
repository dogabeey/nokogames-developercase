using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class State : IState
{
    public AIStateController AIStateController;

    public State(AIStateController aiStateController)
    {
        AIStateController = aiStateController;
    }
     
    public virtual void OnEnter()
    {

    }

    public virtual void OnUpdate()
    {

    }

    public virtual void OnExit()
    {

    }
}

public class WanderState : State
{
    private float startTime;
    public WanderState(AIStateController aiStateController) : base(aiStateController)
    {
        startTime = Time.time;
    }

    public override void OnEnter()
    {
        Debug.Log("WanderState OnEnter");
    }

    public override void OnUpdate()
    {
        Debug.Log("WanderState OnUpdate");
        if (Time.time - startTime > 5)
        {
            AIStateController.ChangeState(new CollectState(AIStateController));
        }
    }

    public override void OnExit()
    {
        Debug.Log("WanderState OnExit");
    }
}
public class CollectState : State
{
    public CollectState(AIStateController aiStateController) : base(aiStateController)
    {
    }
    public override void OnEnter()
    {
        // Get closest ItemContainer that is not Input.
        ItemContainer closestOutputContainer = ItemContainer.itemContainers.First(c => Vector3.Distance(
            AIStateController.transform.position, c.transform.position) == ItemContainer.itemContainers.Min(
            c => 
                c.IsInput() ? float.MaxValue : Vector3.Distance(AIStateController.transform.position, c.transform.position)
            )
        );
        AIStateController.MoveToPosition(closestOutputContainer.transform.position);
    }

    public override void OnUpdate()
    {
        Debug.Log("CollectState OnUpdate");
    }

    public override void OnExit()
    {
        Debug.Log("CollectState OnExit");
    }
}

public class SellState : State
{
    public SellState(AIStateController aiStateController) : base(aiStateController)
    {
    }

    public override void OnEnter()
    {
        // Get Input container where the player has the same item model as the input's accepted item.
        ItemContainer inputContainer = ItemContainer.itemContainers.First(c => c.IsInput() && c.acceptedItem == AIStateController.workerEntity.itemStack.Last().ItemModel);
    }

    public override void OnUpdate()
    {
        Debug.Log("SellState OnUpdate");
    }

    public override void OnExit()
    {
        Debug.Log("SellState OnExit");
    }
}