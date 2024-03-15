using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

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
    [Tooltip("The tag of the children to be considered for layout. If empty, no tag restriction is applied.")]
    public string childrenTag;

    private int childCount;


    private void Start()
    {
        if (startReference == null)
        {
            startReference = transform;
        }
    }

    private void Update()
    {
        if (childCount != transform.childCount)
        {
            childCount = transform.childCount;
            PlaceObjects(GetChildren());
        }
    }

    private Transform[] GetChildren()
    {
        Transform[] children = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            children[i] = transform.GetChild(i);
        }

        return children;
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
                    if (index < objects.Length)
                    {
                        objects[index].position = startReference.position + offset + new Vector3(i * spacing.x, j * spacing.y, k * spacing.z);
                    }
                    index++;
                }
            }
        }
    }
}
