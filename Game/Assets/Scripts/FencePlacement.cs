using UnityEngine;
using EasyRoads3Dv3;

public class FencePlacement : MonoBehaviour
{
    public ERModularRoad mRoad; // Reference to the EasyRoads3D RoadObjectScript
    public ERModularBase mBase; // Reference to the EasyRoads3D RoadObjectScript
    //public GameObject fencePrefab; // The fence prefab to be instantiated
    public ERRoad road; // Reference to the ERRoad object

    //public SideObject fencePrefab; // Prefab of the fence to be placed
    public GameObject fencePrefab; // Prefab of the fence to be placed

    public float fenceSpacing = 10f; // Spacing between individual fence objects
    public int fenceOffset = 1; // Offset from the road's center to place the fences

    private void Start()
    {

    }

    private void OnEnable()
    {
        mRoad = GetComponent<ERModularRoad>();
        //road = mRoad.road;
        //Debug.Log(road.GetName());
        //fencePrefab.
        Debug.Log(mRoad.markers.Count);

        PlaceFencesAlongRoad();
    }

    public void PlaceFencesAlongRoad()
    {
        // Iterate through each marker on the road
        for (int i = 0; i < mRoad.markers.Count; i++)
        {
            // Get the position of the marker
            Vector3 markerPosition = mRoad.markers[i].position;
            Debug.Log(markerPosition);
            // Instantiate the fence prefab at the marker position
            //SideObject fence = Instantiate(fencePrefab, markerPosition, Quaternion.identity);
            GameObject fence = Instantiate(fencePrefab, markerPosition, Quaternion.identity);
            //SideObjectChild

            //road.SetSidewalk

            // Set the fence's position along the road
            //road.SetSideObjectOffset(fence, i, OffsetPosition.Start, 0f);
            

            //// Set the fence as active for the marker
            //road.SideObjectMarkerSetActive(fence, i, true);
        }

        //// Iterate through each marker on the road
        //for (int i = 0; i < road.GetMarkerCount(); i++)
        //{
        //    // Get the position of the marker
        //    Vector3 markerPosition = road.GetMarkerPosition(i);

        //    // Instantiate the fence prefab at the marker position
        //    //SideObject fence = Instantiate(fencePrefab, markerPosition, Quaternion.identity);
        //    GameObject fence = Instantiate(fencePrefab, markerPosition, Quaternion.identity);
        //    //SideObjectChild

        //    //road.SetSidewalk

        //    // Set the fence's position along the road
        //    //road.SetSideObjectOffset(fence, i, OffsetPosition.Start, 0f);
            

        //    //// Set the fence as active for the marker
        //    //road.SideObjectMarkerSetActive(fence, i, true);
        //}
    }
}
