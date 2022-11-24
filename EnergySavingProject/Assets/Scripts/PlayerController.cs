using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform plrBody;

    private float rotateSpeed = 1f;
    private Coroutine lookCoroutine;
    private Transform lookTarget;

    void Update()
    {
        if (Input.GetMouseButtonUp(0)) // M1 klikattu
        {
            // Tutkitaan mitä on klikattu
            Ray moveTo = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(moveTo, out var hitInfo)) { // Onko klikattu objektia
                if (hitInfo.collider.CompareTag("Interactable")) { // Onko objekti interactable
                    // Smooooothly rotate to look at interactable item
                    lookCoroutine = StartCoroutine(lookAt(hitInfo.collider.transform));
                }
                else
                {
                    // Walk to clicked destination
                    agent.SetDestination(hitInfo.point);
                }
                
            }
        }
    }

    private IEnumerator lookAt(Transform tgt) // Used to rotate player towards interactable object
    {
        Quaternion lookRotation = Quaternion.LookRotation(tgt.position - transform.position);
        float time = 0;

        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, time);
            time += Time.deltaTime * rotateSpeed;
            yield return null;
        }
    }

}
