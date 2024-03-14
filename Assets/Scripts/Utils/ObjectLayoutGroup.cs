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

    [ExecuteAlways]
    private void Start()
    {
        if (startReference == null)
        {
            startReference = transform;
        }
    }

    [ExecuteAlways]
    private void Update()
    {
        PlaceObjects(GetChildren());
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
                    index++;
                    if (index < objects.Length)
                    {
                        if(Application.isPlaying)
                        {
                            objects[index].DOMove(startReference.position + offset + new Vector3(i * spacing.x, j * spacing.y, k * spacing.z), Const.Values.OBJECT_STACK_TWEEN_DURATION);
                        }
                        else
                        {
                            objects[index].position = startReference.position + offset + new Vector3(i * spacing.x, j * spacing.y, k * spacing.z);
                        }
                    }
                }
            }
        }
    }
}
