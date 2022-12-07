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
    private bool pause = false;

    // if too far walk near object
    // if close enough turn towards object

    void Update()
    {
        if (Input.GetMouseButtonUp(0)) // M1 klikattu
        {
            // Tutkitaan mitä on klikattu
            Ray moveTo = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(moveTo, out var hitInfo)) { // Onko klikattu objektia
                if (lookCoroutine != null) StopCoroutine(lookCoroutine);
                if (hitInfo.collider.CompareTag("Interactable")) { // Onko objekti interactable
                    // Smooooothly rotate to look at interactable item
                    float distance = Vector3.Distance(plrBody.position, hitInfo.collider.transform.position);
                    if (distance < 1.5f)
                    {
                        lookCoroutine = StartCoroutine(lookAt(hitInfo.collider.transform));
                    }
                    else
                    {
                        agent.SetDestination(hitInfo.point);
                    }
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
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3 (tgt.position.x, 0, tgt.position.z) - new Vector3(transform.position.x, 0, transform.position.z));
        float time = 0;

        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, time);
            time += Time.deltaTime * rotateSpeed;
            yield return null;
        }
    }

}
