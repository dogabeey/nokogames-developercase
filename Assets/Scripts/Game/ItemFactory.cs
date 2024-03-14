using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemFactory : MonoBehaviour
{
    [AssetsOnly]
    public ItemController itemControllerPrefab;
    [Tooltip("Factory will convert the last item in the input container, which is the same type with inputItem, to the output item. If input or itemModel is null, this factory will simply produce the output item out of thin air.")]
    public ItemContainer inputContainer;
    [Tooltip("The animator of the factory. The production animation will be played on this animator.")]
    public Animator animator;
    [Tooltip("The container which the factory will send items to.")]
    public ItemContainer outputContainer;
    [Tooltip("Item model of input. The factory will only take these items from the input container.")]
    public ItemModel inputItem;
    [Tooltip("The item model of the produced item.")]
    public ItemModel outputItem;
    [Tooltip("Production period.")]
    public float productionTime;
    [Tooltip("Production animation string.")]
    public string productionTriggerAnimText;

    private void Start()
    {
        if(inputContainer)
            inputContainer.connectedFactory = this;

        InvokeRepeating(nameof(ConvertLastOrProduce), 0, productionTime);
    }

    public void ConvertLastOrProduce()
    {
        if(inputContainer && inputItem)
        {
            if (inputContainer.items.Count > 0)
            {
                ConvertItem(inputContainer.items.Last(i => i.ItemModel == inputItem));
            }
        }
        else
        {
            ProduceItem();
        }
    }
    public void ConvertItem(ItemController item)
    {
        item.ItemModel = outputItem;
        inputContainer.items.Remove(item);
        item.transform.parent = outputContainer.stackParent.transform;
        outputContainer.items.Add(item);
        animator.SetTrigger(productionTriggerAnimText);

        outputContainer.SetTransform(item);
    }
    public void ProduceItem()
    {
        ItemController item = Instantiate(itemControllerPrefab, outputContainer.stackParent.transform); //TODO: Pooling
        item.ItemModel = outputItem;
        outputContainer.items.Add(item);

        outputContainer.SetTransform(item);
    }
}
