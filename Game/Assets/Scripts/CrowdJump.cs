using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CrowdJump : MonoBehaviour
{
    [SerializeField] [Range(0, 1)] float jumpDelay; 
    [HideInInspector] public float minJumpDelay = 1f;
    [HideInInspector] public float maxJumpDelay = 3f;

    private Animator animator;
    private float nextJumpTime;

    private void Start()
    {
        jumpDelay = Random.Range(0f, 1f);
        animator = GetComponent<Animator>();
        StartCoroutine(JumpDelay());
        //CalculateNextJumpTime();
    }

    private void Update()
    {
        //if (Time.time >= nextJumpTime)
        //{
        //    animator.SetTrigger("Jump");
        //    CalculateNextJumpTime();
        //}
    }

    private void CalculateNextJumpTime()
    {
        nextJumpTime = Time.time + Random.Range(minJumpDelay, maxJumpDelay);
    }

    IEnumerator JumpDelay()
    {
        yield return new WaitForSeconds(jumpDelay);
        animator.SetTrigger("Jump");
    }
}
