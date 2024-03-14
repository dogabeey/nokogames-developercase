using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemContainer : MonoBehaviour
{
    public List<ItemController> items;
    public ObjectLayoutGroup stackParent;

    internal ItemFactory connectedFactory;

    private void Start()
    {
        
    }
}
