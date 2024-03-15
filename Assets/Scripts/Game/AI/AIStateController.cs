using UnityEngine;
using UnityEngine.AI;

public class AIStateController : MonoBehaviour
{
    public WorkerEntity workerEntity;

    internal WanderState wanderState;
    internal CollectState collectState;
    internal SellState sellState;

    private State currentState;
    private void OnEnable()
    {
        EventManager.StartListening(Const.GameEvents.WORKER_FULL, OnWorkerFull);
        EventManager.StartListening(Const.GameEvents.WORKER_EMPTY, OnWorkerEmpty);
        EventManager.StartListening(Const.GameEvents.MACHINE_INPUT_FULL, OnMachineInputFull);
    }
    private void OnDisable()
    {
        EventManager.StopListening(Const.GameEvents.WORKER_FULL, OnWorkerFull);
        EventManager.StopListening(Const.GameEvents.WORKER_EMPTY, OnWorkerEmpty);
        EventManager.StopListening(Const.GameEvents.MACHINE_INPUT_FULL, OnMachineInputFull);
    }
    
    private void OnWorkerFull(EventParam e)
    {
        if(e.paramObj == workerEntity.gameObject)
        {
            ChangeState(sellState);
        }
    }
    private void OnWorkerEmpty(EventParam e)
    {
        if (e.paramObj == workerEntity.gameObject)
        {
            ChangeState(collectState);
        }
    }
    private void OnMachineInputFull(EventParam e)
    {
        ChangeState(wanderState);
    }

    private void Start()
    {
        wanderState = new WanderState(this);
        collectState = new CollectState(this);
        sellState = new SellState(this);

        ChangeState(wanderState);

        // Running current state in case of stucking
        if(workerEntity.agent.remainingDistance <= workerEntity.agent.stoppingDistance)
        {
            InvokeRepeating(nameof(ResetCurrentState), 0, 2f);
        }
    }

    void Update()
    {
        if (currentState != null)
        {
            currentState.OnUpdate();
        }
    }

    public void ChangeState(State newState)
    {
        if (currentState != null)
        {
            currentState.OnExit();
        }
        currentState = newState;
        currentState.OnEnter();
    }
    public void ResetCurrentState()
    {
        ChangeState(currentState);
    }

    public void MoveToPosition(Vector3 position)
    {
        workerEntity.MoveToPosition(position);
    }
}