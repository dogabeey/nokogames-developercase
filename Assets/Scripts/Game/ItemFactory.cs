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
                if(outputContainer && outputItem)
                {
                    if (outputContainer.maxStacks > outputContainer.items.Count)
                    {
                        ConvertItem(inputContainer.items.LastOrDefault(i => i.ItemModel == inputItem));
                    }
                }
                else
                {
                    DestroyItem(inputContainer.items.LastOrDefault(i => i.ItemModel == inputItem));
                }
            }
        }
        else
        {
            if(outputContainer.maxStacks > outputContainer.items.Count)
            {
                ProduceItem();
            }
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
    public void DestroyItem(ItemController item)
    {
        inputContainer.items.Remove(item);
        SimplePool.Despawn(item.gameObject);

        animator.SetTrigger(productionTriggerAnimText);
    }
    public void ProduceItem()
    {
        ItemController item = SimplePool.Spawn(itemControllerPrefab.gameObject, outputContainer.stackParent.transform.position, outputContainer.stackParent.transform.rotation).GetComponent<ItemController>(); //TODO: Pooling
        item.transform.parent = outputContainer.stackParent.transform;
        item.ItemModel = outputItem;
        outputContainer.items.Add(item);

        outputContainer.SetTransform(item);
    }
}
