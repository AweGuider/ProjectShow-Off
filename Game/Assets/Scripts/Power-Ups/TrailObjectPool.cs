using System.Collections.Generic;
using UnityEngine;

public class TrailObjectPool : MonoBehaviour
{
    public GameObject trailSegmentPrefab;
    public int poolSize = 10;

    private List<GameObject> pooledObjects = new List<GameObject>();

    private void Start()
    {
        // Instantiate and populate the object pool
        for (int i = 0; i < poolSize; i++)
        {
            GameObject trailSegment = Instantiate(trailSegmentPrefab);
            trailSegment.SetActive(false);
            pooledObjects.Add(trailSegment);
        }
    }

    public GameObject GetPooledObject()
    {
        // Find an inactive object from the pool
        foreach (GameObject trailSegment in pooledObjects)
        {
            if (!trailSegment.activeInHierarchy)
            {
                return trailSegment;
            }
        }

        // If all objects are active, create a new one and add it to the pool
        GameObject newTrailSegment = Instantiate(trailSegmentPrefab);
        newTrailSegment.SetActive(false);
        pooledObjects.Add(newTrailSegment);

        return newTrailSegment;
    }
}