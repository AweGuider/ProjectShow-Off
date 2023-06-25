//using Demo;
using GD.MinMaxSlider;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Placement
{
    [System.Serializable]
    public class ObjectPrefab
    {
        public GameObject prefab;
        [Range(0f, 1f)]
        public float chance = 1f;
    }

    public enum ObjectType
    {
        Standard,
        Decoration,
        Building
    }

    public class PlacementGenerator : MonoBehaviour
    {
        [Header("General Settings")]

        [Tooltip("Kind of object to be spawned")]
        public ObjectType ObjectType = ObjectType.Standard;

        [HideInInspector] public bool useMultiplePrefabs = false;
        [SerializeField] List<ObjectPrefab> _objectPrefabs = new(1);

        [Tooltip("Check it if you don't want objects to be cleared every generation")]
        [SerializeField] bool _dontClear;

        [Tooltip("[BETA] Want to see handles of spawned objects?\n[WARNING] Don't use with big density values!")]
        public bool drawHandles;

        [Space(10)]

        [Header("On Area/Terrain Selected Settings")]
        [Tooltip("Key to use to spawn objects")]
        public KeyCode BuildKey = KeyCode.C;
        [Tooltip("Object you selected/want objects to be spawned on")]
        public GameObject TargetObject;

        [Header("Building Settings")]
        [HideInInspector] public int rows = 10;
        [HideInInspector] public int columns = 10;
        [HideInInspector] public int rowWidth = 10;
        [HideInInspector] public int columnWidth = 10;

        [HideInInspector] public float buildDelaySeconds = 0.1f;

        [Space(20)]

        [Header("Raycast Settings")]
        [Tooltip("How many objects you want to be spawned")]
        [SerializeField] int density = 5;

        [Header("Spawning Settings")]
        [SerializeField] float minHeight = -100f;
        [SerializeField] float maxHeight = 100f;
        public bool useRadius = false;
        [Tooltip("Radius value automatically changes xRange and zRange")]
        public float radius = 5f;
        [SerializeField] Vector2 xRange = new(-50, 50);
        [SerializeField] Vector2 zRange = new(-50, 50);

        [Header("Prefab Variation Settings")]
        [SerializeField, Range(0, 1)] float rotateTowardsNormal = 0.1f;
        //[SerializeField] Vector2 rotationRange = new(0, 360f);
        [SerializeField] [MinMaxSlider(0, 360)] private Vector2Int rotationRange = new(0, 360);
        [SerializeField] Vector3 minScale = new(0.9f, 0.9f, 0.9f);
        [SerializeField] Vector3 maxScale = new(1.1f, 1.1f, 1.1f);

        public List<GameObject> instantiatedPrefabs = new();

#if UNITY_EDITOR
        [ContextMenu("Generate")]
        public List<GameObject> Generate(Vector3 initialSample = new())
        {
            Clear();

            if (initialSample.magnitude == 0) initialSample = transform.position;

            for (int i = 0; i < density; i++)
            {
                float sampleX = initialSample.x + Random.Range(xRange.x, xRange.y);
                float sampleY = initialSample.z + Random.Range(zRange.x, zRange.y);

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

                Quaternion randomRotation = Quaternion.Euler(0, Random.Range(0f, Random.Range(rotationRange.x, rotationRange.y)), 0);

                instantiatedPrefab.transform.Rotate(Vector3.up, Random.Range(rotationRange.x, rotationRange.y), Space.Self);
                instantiatedPrefab.transform.rotation = randomRotation;

                //if (ObjectType == ObjectType.Building)
                //{

                //    //Quaternion randomRotation = Quaternion.Euler(0, Random.Range(0f, 3.6f * Random.Range(rotationRange.x, rotationRange.y)), 0);

                //    //instantiatedPrefab.transform.Rotate(Vector3.up, Random.Range(rotationRange.x, rotationRange.y), Space.Self);
                //    //instantiatedPrefab.transform.rotation = randomRotation;
                //    //if (instantiatedPrefab.TryGetComponent(out Shape shape))
                //    //{
                //    //    shape.Generate(buildDelaySeconds);
                //    //}
                //}
                //else
                //{
                //    instantiatedPrefab.transform.Rotate(Vector3.up, Random.Range(rotationRange.x, rotationRange.y), Space.Self);
                //    instantiatedPrefab.transform.rotation = Quaternion.Lerp(transform.rotation, transform.rotation * Quaternion.FromToRotation(instantiatedPrefab.transform.up, hit.normal), rotateTowardsNormal);
                //}
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
            if (!useMultiplePrefabs || _objectPrefabs.Count == 0)
            {
                if (_objectPrefabs.Count > 0)
                    return _objectPrefabs[0].prefab;
                else
                    return null;
            }

            float totalChance = 0f;
            foreach (var prefabChance in _objectPrefabs)
            {
                totalChance += prefabChance.chance;
            }

            float randomValue = Random.Range(0f, totalChance);
            float chanceSum = 0f;
            foreach (var prefabChance in _objectPrefabs)
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

            useMultiplePrefabs = _objectPrefabs.Count > 1;
        }
#endif
    }
}


