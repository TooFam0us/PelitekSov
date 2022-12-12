using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform plrBody;

    bool isMoving = false;

    private float rotateSpeed = 1f;
    private Coroutine lookCoroutine;
    private Transform lookTarget;
    private bool pause = false;
    ObjectInteraction hitObject;

    // if too far walk near object
    // if close enough turn towards object

    void Update()
    {
        if (GameManager.Instance.IsGameOver())
            return;
        if (Input.GetMouseButtonDown(0)) // M1 klikattu
        {
            // Tutkitaan mitä on klikattu
            Ray moveTo = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(moveTo, out var hitInfo))
            { // Onko klikattu objektia
                if (lookCoroutine != null) StopCoroutine(lookCoroutine);
                if (hitInfo.collider.CompareTag("Interactable"))
                { // Onko objekti interactable
                    // Smooooothly rotate to look at interactable item
                    hitObject = hitInfo.collider.gameObject.GetComponent<ObjectInteraction>();
                    isMoving = true;
                    float distance = Vector3.Distance(transform.position, hitInfo.collider.transform.position);
                    if (distance < 2f)
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

        if (isMoving && hitObject)
        {
            Debug.Log(Vector3.Distance(transform.position, hitObject.transform.position));
            if (Vector3.Distance(transform.position, hitObject.transform.position) < 2f)
            {
                hitObject.Interact();
                hitObject = null;
                isMoving = false;

            }
        }

    }

    private IEnumerator lookAt(Transform tgt) // Used to rotate player towards interactable object
    {
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(tgt.position.x, 0, tgt.position.z) - new Vector3(transform.position.x, 0, transform.position.z));
        float time = 0;

        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, time);
            time += Time.deltaTime * rotateSpeed;
            yield return null;
        }
    }

}
