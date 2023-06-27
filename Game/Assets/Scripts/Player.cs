using KartGame.KartSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IPlayer
{
    [SerializeField] private Checkpoint checkpoint;
    public ArcadeKart Kart;

    [SerializeField] private float cantMoveDelay = .5f;


    private void Start()
    {
        Kart = GetComponentInParent<ArcadeKart>();
    }

    public void SetCheckpoint(Checkpoint cp)
    {
        // Set checkpoint position
        if (checkpoint != cp) checkpoint = cp;
        Debug.Log($"Tried to set cp");

    }
    public void ReactToDead()
    {
        // Reset position to saved checkpoint
        // (Additional) Set velocity/speed to zero so the player doesnt respawn with super high speed
        StartCoroutine(PauseMovement());

    }

    IEnumerator PauseMovement()
    {
        Kart.SetCanMove(false);
        yield return new WaitForSeconds(cantMoveDelay / 20f);
        Kart.ResetRigidbody();
        yield return new WaitForSeconds(cantMoveDelay);
        Kart.SetRotation(checkpoint.CheckpointRotation);
        Kart.gameObject.transform.position = checkpoint.CheckpointPosition;
        Kart.SetCanMove(true);
    }
}
