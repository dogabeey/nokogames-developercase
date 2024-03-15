using UnityEngine;
using UnityEngine.AI;

public class AIStateController : MonoBehaviour
{
    public WorkerEntity workerEntity;
    public NavMeshAgent agent;

    internal WanderState wanderState;
    internal CollectState collectState;
    internal SellState sellState;

    State currentState;

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

    public void MoveToPosition(Vector3 position)
    {
        agent.SetDestination(position);
    }
}