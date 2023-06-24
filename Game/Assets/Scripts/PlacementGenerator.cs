//using Demo;
using GD.MinMaxSlider;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

[System.Serializable]
public class PrefabChance
{
    public GameObject prefab;
    [Range(0f, 1f)]
    public float chance = 1f;
}

public enum ObjectType
{
    Decoration,
    Building
}

public class PlacementGenerator : MonoBehaviour
{
    public GameObject targetObject;
    public bool drawHandles;

    [SerializeField] bool _dontClear;

    [Header("Building Settings")]
    [HideInInspector] public int rows = 10;
    [HideInInspector] public int columns = 10;
    [HideInInspector] public int rowWidth = 10;
    [HideInInspector] public int columnWidth = 10;

    [HideInInspector] public float buildDelaySeconds = 0.1f;

    public KeyCode BuildKey = KeyCode.C;
    public ObjectType ObjectType = ObjectType.Decoration;

    public List<GameObject> instantiatedPrefabs = new();

    public bool useMultiplePrefabs = false;
    [SerializeField] List<PrefabChance> prefabChances = new List<PrefabChance>(1);

    [Header("Raycast Settings")]
    [SerializeField] int density = 1000;

    [Space]

    [Header("Mouse Settings")]
    public bool useRadius = false;
    [Tooltip("Radius value automatically changes xRange and zRange")]
    public float radius = 5f;

    [Header("Spawning Range")]
    [SerializeField] float minHeight = 0;
    [SerializeField] float maxHeight = 10f;
    [SerializeField] Vector2 xRange = new(-50, 50);
    [SerializeField] Vector2 zRange = new(-50, 50);

    [Header("Prefab Variation Settings")]
    [SerializeField, Range(0, 1)] float rotateTowardsNormal = 0.1f;
    [SerializeField] Vector2 rotationRange = new(0, 360f);
    [SerializeField] Vector3 minScale = new(0.9f, 0.9f, 0.9f);
    [SerializeField] Vector3 maxScale = new(1.1f, 1.1f, 1.1f);


    //[Header("Set rotation and position for each building")]
    //[SerializeField] [MinMaxSlider(0, 100)] private Vector2Int rotationRange;
    //private int rotRand;

#if UNITY_EDITOR
    [ContextMenu("Generate")]
    public List<GameObject> Generate(Vector3 initialSample = new())
    {
        if (initialSample.magnitude == 0) initialSample = transform.position;

        Clear();

        //List<GameObject> instantiatedGameobjects = new();

        for (int i = 0; i < density; i++)
        {
            float sampleX = initialSample.x + Random.Range(xRange.x, xRange.y);
            float sampleY = initialSample.z + Random.Range(zRange.x, zRange.y);

            //float sampleX = initialSample.x + (useRadius ? Random.Range(-radius, radius) : Random.Range(xRange.x, xRange.y));
            //float sampleY = initialSample.z + (useRadius ? Random.Range(-radius, radius) : Random.Range(zRange.x, zRange.y));

            Vector3 rayStart = new Vector3(sampleX, maxHeight, sampleY);

            if (!Physics.Raycast(rayStart, Vector3.down, out RaycastHit hit, Mathf.Infinity))
                continue;

            if (hit.point.y < minHeight)
                continue;

            GameObject instantiatedPrefab = GetRandomPrefab();
            if (instantiatedPrefab == null)
                continue;

            instantiatedPrefab = (GameObject)PrefabUtility.InstantiatePrefab(instantiatedPrefab, transform);
            instantiatedPrefab.transform.position = hit.point;
            //instantiatedPrefab.transform.Rotate(Vector3.up, Random.Range(rotationRange.x, rotationRange.y), Space.Self);
            //instantiatedPrefab.transform.rotation = Quaternion.Lerp(transform.rotation, transform.rotation * Quaternion.FromToRotation(instantiatedPrefab.transform.up, hit.normal), rotateTowardsNormal);


            if (ObjectType == ObjectType.Building)
            {

                //Quaternion randomRotation = Quaternion.Euler(0, Random.Range(0f, 3.6f * Random.Range(rotationRange.x, rotationRange.y)), 0);

                //instantiatedPrefab.transform.Rotate(Vector3.up, Random.Range(rotationRange.x, rotationRange.y), Space.Self);
                //instantiatedPrefab.transform.rotation = randomRotation;
                //if (instantiatedPrefab.TryGetComponent(out Shape shape))
                //{
                //    shape.Generate(buildDelaySeconds);
                //}
            }
            else
            {
                instantiatedPrefab.transform.Rotate(Vector3.up, Random.Range(rotationRange.x, rotationRange.y), Space.Self);
                instantiatedPrefab.transform.rotation = Quaternion.Lerp(transform.rotation, transform.rotation * Quaternion.FromToRotation(instantiatedPrefab.transform.up, hit.normal), rotateTowardsNormal);
            }
            instantiatedPrefab.transform.localScale = new Vector3(
                Random.Range(minScale.x, maxScale.x),
                Random.Range(minScale.y, maxScale.y),
                Random.Range(minScale.z, maxScale.z)
            );

            instantiatedPrefabs.Add(instantiatedPrefab);
        }
        return instantiatedPrefabs;
    }

    private GameObject GetRandomPrefab()
    {
        if (!useMultiplePrefabs || prefabChances.Count == 0)
        {
            if (prefabChances.Count > 0)
                return prefabChances[0].prefab;
            else
                return null;
        }

        float totalChance = 0f;
        foreach (var prefabChance in prefabChances)
        {
            totalChance += prefabChance.chance;
        }

        float randomValue = Random.Range(0f, totalChance);
        float chanceSum = 0f;
        foreach (var prefabChance in prefabChances)
        {
            chanceSum += prefabChance.chance;
            if (randomValue <= chanceSum)
            {
                return prefabChance.prefab;
            }
        }

        return null;
    }

    public void Clear()
    {
        if (_dontClear) return;

        instantiatedPrefabs.Clear();
        while (transform.childCount != 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }

    private void OnValidate()
    {
        if (useRadius)
        {
            xRange = new Vector2(-radius, radius);
            zRange = new Vector2(-radius, radius);
        }
    }
#endif
}
