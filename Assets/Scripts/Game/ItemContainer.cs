using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Security.Cryptography;
using UnityEngine.UI;
using Unity.VisualScripting;
using TMPro;

public class ItemContainer : MonoBehaviour
{
    public static List<ItemContainer> itemContainers = new();

    public List<ItemController> items;
    public ItemModel acceptedItem;
    public ObjectLayoutGroup stackParent;
    public int maxStacks;
    public float itemTransferPeriod;
    public Vector3 rotationOffset;
    public Vector3 scale = Vector3.one;
    [Space]
    public SpriteRenderer bgImage;
    public SpriteRenderer iconImage;
    public TMP_Text stackCountText;

    internal ItemFactory connectedFactory;
    internal List<WorkerEntity> workers = new List<WorkerEntity>();

    private int lastStackCount;

    private IEnumerator Start()
    {
        itemContainers.Add(this);

        yield return new WaitForSeconds(0.5f);

        if (IsInput())
        {
            InvokeRepeating(nameof(TakeLastItemsFromWorkers), 0, itemTransferPeriod);
        }
        else
        {
            InvokeRepeating(nameof(GiveLastItemToFirstWorker), 0, itemTransferPeriod);
        }
    }

    private void Update()
    {
        
        DrawUI();

        if (lastStackCount != items.Count)
        {
            if (items.Count == maxStacks && IsInput())
            {
                EventManager.TriggerEvent(Const.GameEvents.MACHINE_INPUT_FULL, new EventParam(paramObj: gameObject));
            }
            if (items.Count == 0 && !IsInput())
            {
                EventManager.TriggerEvent(Const.GameEvents.MACHINE_OUTPUT_EMPTY, new EventParam(paramObj: gameObject));
            }
        }

        lastStackCount = items.Count;
    }

    private void OnDestroy()
    {
        itemContainers.Remove(this);
    }

    private void DrawUI()
    {
        if (bgImage) bgImage.color = IsInput() ? Color.red : Color.green;
        if (iconImage && IsInput()) iconImage.sprite = acceptedItem.icon;
        if (stackCountText) stackCountText.text = items.Count.ToString() + "/" + maxStacks.ToString();
    }

    private void TakeLastItemsFromWorkers()
    {
        foreach (WorkerEntity worker in workers)
        {
            List<ItemController> itemsToTake = worker.itemStack.Where(i => acceptedItem == i.ItemModel).ToList();
            if(itemsToTake.Any())
            {
                ItemController item = itemsToTake.Last();
                worker.itemStack.Remove(item);
                items.Add(item);
                item.transform.parent = stackParent.transform;
                SetTransform(item);
            }

        }
    }
    private void GiveLastItemToFirstWorker()
    {
        if(workers.Count > 0 && items.Count > 0)
        {
            WorkerEntity worker = workers[0];
            if(worker.itemStack.Count < worker.maxCarryAmount)
            {
                ItemController item = items.Last();

                items.Remove(item);
                worker.AddItemToEntity(item);
            }
        }
    }

    public bool IsInput() => connectedFactory != null;

    public void AddItemToContainer(ItemController item)
    {
        items.Add(item);
        item.transform.parent = stackParent.transform;
    }

    public void SetTransform(ItemController item)
    {
        item.transform.localEulerAngles = rotationOffset;
        item.transform.localScale = Vector3.Scale(item.transform.localScale, scale);
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
