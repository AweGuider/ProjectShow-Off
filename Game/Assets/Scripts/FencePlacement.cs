using UnityEngine;
using EasyRoads3Dv3;

public class FencePlacement : MonoBehaviour
{
    public ERModularRoad mRoad; // Reference to the EasyRoads3D RoadObjectScript
    public ERModularBase mBase; // Reference to the EasyRoads3D RoadObjectScript
    //public GameObject fencePrefab; // The fence prefab to be instantiated
    public ERRoad road; // Reference to the ERRoad object

    public SideObject fencePrefab; // Prefab of the fence to be placed

    public float fenceSpacing = 10f; // Spacing between individual fence objects
    public int fenceOffset = 1; // Offset from the road's center to place the fences

    private void Start()
    {
        //PlaceFencesAlongRoad();
        mRoad = GetComponent<ERModularRoad>();
        road = mRoad.road;
        Debug.Log(road.GetName());
    }



    public void PlaceFencesAlongRoad()
    {
        // Iterate through each marker on the road
        for (int i = 0; i < road.GetMarkerCount(); i++)
        {
            // Get the position of the marker
            Vector3 markerPosition = road.GetMarkerPosition(i);

            // Instantiate the fence prefab at the marker position
            SideObject fence = Instantiate(fencePrefab, markerPosition, Quaternion.identity);

            // Set the fence's position along the road
            road.SetSideObjectOffset(fence, i, OffsetPosition.Start, 0f);
            

            // Set the fence as active for the marker
            road.SideObjectMarkerSetActive(fence, i, true);
        }
    }

    //private void PlaceFencesAlongRoad()
    //{
    //    if (mRoad == null || fencePrefab == null)
    //    {
    //        Debug.LogError("Road object or fence prefab not assigned!");
    //        return;
    //    }

    //    float roadLength = mRoad.roadWidth; // Get the length of the road (assuming road width represents length)

    //    for (float distance = 0f; distance < roadLength; distance += fenceSpacing)
    //    {
    //        Vector3 position = roadd.GetPosition(distance, ref fenceOffset); // Get the position along the road

    //        // Instantiate the fence object at the calculated position
    //        GameObject fence = Instantiate(fencePrefab, position, Quaternion.identity);

    //        // Optionally, you can parent the fence to a container object for better organization
    //        fence.transform.SetParent(transform);
    //    }
    //}
}
