using Placement;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlacementGenerator))]
public class PlacementGeneratorEditor : Editor
{

    SerializedProperty useMultiplePrefabsProp;
    SerializedProperty prefabChancesProp;
    SerializedProperty useRadiusProp;
    SerializedProperty radiusProp;

    SerializedProperty objectTypeProp;

    SerializedProperty targetObjectProp; // Reference to the target object

    SerializedProperty rowsProp;
    SerializedProperty columnsProp;
    SerializedProperty rowWidthProp;
    SerializedProperty columnWidthProp;
    SerializedProperty buildDelaySecondsProp;

    PlacementGenerator placementGenerator;

    private void OnEnable()
    {
        useMultiplePrefabsProp = serializedObject.FindProperty("useMultiplePrefabs");
        prefabChancesProp = serializedObject.FindProperty("prefabChances");
        useRadiusProp = serializedObject.FindProperty("useRadius");
        radiusProp = serializedObject.FindProperty("radius");

        objectTypeProp = serializedObject.FindProperty("ObjectType");

        rowsProp = serializedObject.FindProperty("rows");
        columnsProp = serializedObject.FindProperty("columns");
        rowWidthProp = serializedObject.FindProperty("rowWidth");
        columnWidthProp = serializedObject.FindProperty("columnWidth");
        buildDelaySecondsProp = serializedObject.FindProperty("buildDelaySeconds");

        targetObjectProp = serializedObject.FindProperty("targetObject"); // Initialize the target object reference

        placementGenerator = (PlacementGenerator)target;

    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        //EditorGUILayout.PropertyField(objectTypeProp);

        ObjectType objectType = (ObjectType)objectTypeProp.enumValueIndex;

        //if (objectType == ObjectType.Building)
        //{
        //    EditorGUILayout.PropertyField(rowsProp);
        //    EditorGUILayout.PropertyField(columnsProp);
        //    EditorGUILayout.PropertyField(rowWidthProp);
        //    EditorGUILayout.PropertyField(columnWidthProp);
        //    EditorGUILayout.PropertyField(buildDelaySecondsProp);
        //}

        DrawDefaultInspector();

        EditorGUILayout.Space();



        //EditorGUILayout.PropertyField(useMultiplePrefabsProp);

        //if (placementGenerator.useMultiplePrefabs)
        //{
        //    EditorGUILayout.PropertyField(prefabChancesProp, true);
        //}
        //else
        //{
        //    EditorGUILayout.PropertyField(prefabChancesProp.GetArrayElementAtIndex(0), true);
        //}



        //EditorGUILayout.PropertyField(useRadiusProp);
        //if (placementGenerator.useRadius)
        //{
        //    EditorGUILayout.PropertyField(radiusProp);
        //}

        serializedObject.ApplyModifiedProperties();

        GUILayout.Space(10);

        if (GUILayout.Button("Generate"))
        {
            placementGenerator.Generate();
        }

        if (GUILayout.Button("Clear"))
        {
            placementGenerator.Clear();
        }
    }

    public void OnSceneGUI()
    {
        Event e = Event.current;
        if (e.type == EventType.KeyDown && e.keyCode == placementGenerator.BuildKey)
        {
            e.Use();

            // TODO (Ex 2): raycast into the scene.
            //  If this painter object is hit, create a new house and add it to 
            //   BuildingPainter's list.
            //  Optionally: if a previously generated house is hit, destroy it again.
            //  Don't forget to register these action for undo, and mark the scene as dirty.


            // Raycast onto the target object
            GameObject targetObject = placementGenerator.TargetObject;
            if (targetObject != null)
            {
                Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    //Debug.Log($"Hit {hit.collider.name}");
                    if (hit.collider.gameObject == targetObject)
                    {
                        //Debug.Log("Building added at mouse position: " + hit.point);
                        foreach (GameObject obj in placementGenerator.Generate(hit.point))
                        {
                            Undo.RegisterCreatedObjectUndo(obj, "Added building");
                        }
                    }
                }
            }

        }

        if (placementGenerator.drawHandles) DrawBuildingTransforms();
    }


    private void DrawBuildingTransforms()
    {

        for (int i = 0; i < placementGenerator.instantiatedPrefabs.Count; i++)
        {

            // Take care of destroying buildings manually:
            GameObject obj = placementGenerator.instantiatedPrefabs[i];
            Transform handleTransform = obj.transform;

            if (obj == null)
            {
                placementGenerator.instantiatedPrefabs.RemoveAt(i);
                i--;
                continue;
            }

            EditorGUI.BeginChangeCheck();
            Vector3 newPosition = Handles.PositionHandle(handleTransform.position, Quaternion.identity);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(obj, "Move curve point");
                handleTransform.position = newPosition;

            }
            // TODO (Ex 2): Draw a handle at the position of this building.
            //  Try to draw a position, rotation and scale gizmo at the same time.
            //  Don't forget to record changes in the undo list, and mark the scene as dirty.
        }
    }
}
