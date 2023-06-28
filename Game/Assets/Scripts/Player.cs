using KartGame.KartSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Checkpoint Checkpoint;
    public ArcadeKart Kart;

    [SerializeField] private float cantMoveDelay = .5f;


    private void Start()
    {
        if (Kart == null) Kart = GetComponent<ArcadeKart>();
    }

    public void SetCheckpoint(Checkpoint cp)
    {
        // Set checkpoint position
        if (Checkpoint != cp) Checkpoint = cp;
        Debug.Log($"Tried to set cp");

    }
    public void Respawn()
    {
        // Reset position to saved checkpoint
        // (Additional) Set velocity/speed to zero so the player doesnt respawn with super high speed
        if (Checkpoint != null) StartCoroutine(PauseMovement());

    }

    IEnumerator PauseMovement()
    {
        Kart.SetCanMove(false);
        yield return new WaitForSeconds(cantMoveDelay / 20f);
        Kart.ResetRigidbody();
        yield return new WaitForSeconds(cantMoveDelay);
        Kart.SetRotation(Checkpoint.CheckpointRotation);
        Kart.gameObject.transform.position = Checkpoint.CheckpointPosition;
        Kart.SetCanMove(true);
    }
}
