using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Vector3 CheckpointPosition;
    public float CheckpointRotation;

    // Start is called before the first frame update
    void Start()
    {
        OnValidate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Got triggered by {other.name}");
        if (other.TryGetComponent(out IPlayer player))
        {
            player.SetCheckpoint(this);
            Debug.Log($"Player name: {other.name}");
        }
    }

    private void OnValidate()
    {
        CheckpointPosition = transform.position;
        CheckpointRotation = transform.rotation.eulerAngles.y;
    }
}
