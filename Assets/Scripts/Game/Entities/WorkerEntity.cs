using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WorkerEntity : Entity
{
    [Tooltip("Newly added items will be added under those parents. If item stack exceeds the parent count, It will be added from the beginning in a loop.")]
    public List<Transform> itemStackParents;
    public List<ItemController> itemStack;
    public int maxCarryAmount = 10;
    public float stackSpacing;
    [Space]
    public TMP_Text stackAmountText;
    public Image stackPanelBG;

    internal float lastItemTake;

    private int lastStackCount;

    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();

        foreach (Transform stackParent in itemStackParents)
        {
            Vector3 angles = stackParent.InverseTransformDirection(rb.velocity) / 5;
            angles = new Vector3(angles.x, angles.z, angles.y);
            stackParent.DOLocalRotate(angles, 0.22f);
        }

        DrawUI();

        if(lastStackCount != itemStack.Count)
        {
            if (itemStack.Count == maxCarryAmount)
            {
                EventManager.TriggerEvent(Const.GameEvents.WORKER_FULL, new EventParam(paramObj: gameObject));
            }
            if (itemStack.Count == 0)
            {
                EventManager.TriggerEvent(Const.GameEvents.WORKER_EMPTY, new EventParam(paramObj: gameObject));
            }
        }

        lastStackCount = itemStack.Count;
    }

    private void DrawUI()
    {
        if(stackAmountText) stackAmountText.text = itemStack.Count.ToString() + "/" + maxCarryAmount.ToString();
        if(stackPanelBG) stackPanelBG.color = Color.Lerp(Color.blue, Color.red, (float)itemStack.Count / maxCarryAmount);
    }


    public void AddItemToEntity(ItemController item)
    {
        lastItemTake = Time.time;

        itemStack.Add(item);
        item.transform.SetParent(itemStackParents[itemStack.Count - 1].transform); // TODO: Check overflow
        item.transform.DOLocalMove(Vector3.zero, Const.Values.OBJECT_STACK_TWEEN_DURATION);
        item.transform.DOLocalRotate(Vector3.zero, Const.Values.OBJECT_STACK_TWEEN_DURATION);
    }
}
