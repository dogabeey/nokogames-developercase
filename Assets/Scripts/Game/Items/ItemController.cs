using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public ItemModel itemModel;
    public Transform meshParent;

    public ItemModel ItemModel
    {
        get
        {
            return itemModel;
        }
        set
        { 
            itemModel = value;
            SetItemModel();
        }
    }

    private void Start()
    {
        if (meshParent.childCount == 0)
            Instantiate(itemModel.itemMesh, meshParent);
    }

    public void SetItemModel()
    {
        if(meshParent.childCount > 0)
            Destroy(meshParent.GetChild(0).gameObject);
        Instantiate(itemModel.itemMesh, meshParent);
    }
}
