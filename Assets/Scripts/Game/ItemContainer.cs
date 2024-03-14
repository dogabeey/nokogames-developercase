using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class ItemContainer : MonoBehaviour
{
    public List<ItemController> items;
    public List<ItemModel> acceptedItems;
    public ObjectLayoutGroup stackParent;
    public float itemTransferPeriod;
    [ReadOnly]
    public ItemFactory connectedFactory;
    [ReadOnly]
    public List<WorkerEntity> workers = new List<WorkerEntity>();

    private void Start()
    {
        if(IsInput())
        {
            InvokeRepeating(nameof(TakeLastItemsFromWorkers), 0, itemTransferPeriod);
        }
        else
        {
            InvokeRepeating(nameof(GiveLastItemToFirstWorker), 0, itemTransferPeriod);
        }
    }

    private void TakeLastItemsFromWorkers()
    {
        foreach (WorkerEntity worker in workers)
        {
            List<ItemController> itemsToTake = worker.itemStack.Where(i => acceptedItems.Contains(i.itemModel)).ToList();
            ItemController item = itemsToTake.Last();

            worker.itemStack.Remove(item);
            items.Add(item);
            item.transform.parent = stackParent.transform;
        }
    }
    private void GiveLastItemToFirstWorker()
    {
        if(workers.Count > 0 && items.Count > 0)
        {
            WorkerEntity worker = workers[0];
            ItemController item = items.Last();

            items.Remove(item);
            worker.AddItemToEntity(item);
        }
    }

    public bool IsInput() => connectedFactory != null;

    public void AddItemToContainer(ItemController item)
    {
        items.Add(item);
        item.transform.parent = stackParent.transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out WorkerEntity worker))
        {
            workers.Add(worker);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out WorkerEntity worker))
        {
            workers.Remove(worker);
        }
    }
}
