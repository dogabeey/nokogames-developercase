using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[ExecuteAlways]
public class ObjectLayoutGroup : MonoBehaviour
{
    [Tooltip("The reference point for the layout group. If null, the transform of this object will be used.")]
    public Transform startReference;
    [Tooltip("The offset from the reference point to the first object.")]
    public Vector3 offset;
    [Tooltip("The spacing between objects based on X, Y, Z axis.")]
    public Vector3 spacing;
    [Tooltip("The maximum number of objects in the layout group based on X, Y, Z axis.")]
    public Vector3Int maxSizes;

    private int childCount;

    [ExecuteAlways]
    private void Start()
    {
        childCount = transform.childCount;

        if (startReference == null)
        {
            startReference = transform;
        }
    }

    [ExecuteAlways]
    private void Update()
    {
        if (transform.childCount != childCount)
        {
            childCount = transform.childCount;
            OnChildAdded(transform.GetChild(transform.childCount - 1));
        }

    }
    void OnChildAdded(Transform child)
    {
        // Insert the code you want to execute when a new child is added
        Debug.Log("A new child has been added.");
        PlaceObjects(GetChildren());
    }

    private Transform[] GetChildren()
    {
            return GetComponentsInChildren<Transform>();
    }
    private void PlaceObjects(Transform[] objects)
    {
        int index = 0;

        for (int j = 0; j < maxSizes.y; j++)
        {
            for (int i = 0; i < maxSizes.x; i++)
            {
                for(int k = 0; k < maxSizes.z; k++)
                {
                    index++;
                    if (index < objects.Length)
                    {
                        objects[index].position = startReference.position + offset + new Vector3(i * spacing.x, j * spacing.y, k * spacing.z);
                    }
                }
            }
        }
    }
}
