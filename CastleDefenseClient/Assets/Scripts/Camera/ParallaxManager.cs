using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxManager : MonoBehaviour
{
    [SerializeField] List<ParallaxObject> parallaxObjectList = new();
    [SerializeField] Camera targetCamera;

    void Awake()
    {
        foreach (ParallaxObject po in parallaxObjectList)
        {
            po.parallaxObjectStartPosition = po.parallaxObjectTransform.position;
        }
    }

    void Update()
    {
        foreach (ParallaxObject po in parallaxObjectList)
        {
            po.parallaxObjectTransform.position = (Vector3)po.parallaxObjectStartPosition + new Vector3(targetCamera.transform.position.x, 0, 0) * po.parallaxAmount;
        }
    }
}

[System.Serializable]
class ParallaxObject
{
    public Transform parallaxObjectTransform;
    public Vector2 parallaxObjectStartPosition;
    [Range(0, 1)] public float parallaxAmount;
}
