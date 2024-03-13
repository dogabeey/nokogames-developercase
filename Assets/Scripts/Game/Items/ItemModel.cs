using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemModel", menuName = "Game/Items/ItemModel")]
public class ItemModel : ScriptableObject
{
    public string itemName;
    public int maxStack;
    public Sprite icon;
    public MeshRenderer itemMesh;
}
