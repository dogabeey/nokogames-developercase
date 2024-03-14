using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WorkerEntity : Entity
{
    [Tooltip("Newly added items will be added under those parents. If item stack exceeds the parent count, It will be added from the beginning in a loop.")]
    public List<Transform> itemStackParents;
    public List<ItemController> itemStack;
    public int maxCarryAmount = 10;
    public float stackSpacing;

    protected override void Start()
    {
        base.Start();
    }

    public void AddItemToEntity(ItemController item)
    {
        itemStack.Add(item);
        item.transform.SetParent(itemStackParents[itemStack.Count - 1].transform); // TODO: Check overflow
        item.transform.DOLocalMove(Vector3.zero, Const.Values.OBJECT_STACK_TWEEN_DURATION);
    }
}
