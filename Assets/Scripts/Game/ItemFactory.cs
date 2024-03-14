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
    [Tooltip("The container which the factory will send items to.")]
    public ItemContainer outputContainer;
    [Tooltip("Item model of input. The factory will only take these items from the input container.")]
    public ItemModel inputItem;
    [Tooltip("The item model of the produced item.")]
    public ItemModel outputItem;
    [Tooltip("Production period.")]
    public float productionTime;

    private void Start()
    {
        inputContainer.connectedFactory = this;

        InvokeRepeating(nameof(ConvertLastOrProduce), 0, productionTime);
    }

    public void ConvertLastOrProduce()
    {
        if(inputContainer && inputItem)
        {
            if (inputContainer.items.Count > 0)
            {
                ConvertItem(inputContainer.items.Last(i => i.itemModel == inputItem));
            }
        }
        else
        {
            ProduceItem();
        }
    }
    public void ConvertItem(ItemController item)
    {
        item.itemModel = outputItem;
        inputContainer.items.Remove(item);
        item.transform.parent = outputContainer.stackParent.transform;
        outputContainer.items.Add(item);
    }
    public void ProduceItem()
    {
        ItemController item = Instantiate(itemControllerPrefab, inputContainer.stackParent.transform); //TODO: Pooling
        item.itemModel = outputItem;
        outputContainer.items.Add(item);
    }


}
