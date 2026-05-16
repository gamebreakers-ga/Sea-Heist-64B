using System.Collections;
using UnityEngine;

public class fihscript : MonoBehaviour
{
    public int cost;

    public float moveSpeed = 2f;
    public float moveRange = 5f;
    public float directionChangeTime = 1f;

    private Vector3 moveDirection;

    private pickupscript p;

    private bool playerInRange;

    private Renderer rend;

    void Start()
    {
        cost = Random.Range(1500, 2501);

        p = FindObjectOfType<pickupscript>();

        rend = GetComponent<Renderer>();

        PickNewDirection();

        StartCoroutine(ChangeDirectionRoutine());

        if (cost < 1700)
        {
            rend.material.color = Color.red;
        }
        else if (cost < 1900)
        {
            rend.material.color = Color.blue;
        }
        else if (cost < 2100)
        {
            rend.material.color = Color.green;
        }
        else
        {
            rend.material.color = Color.magenta;
        }
    }

    void Update()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                5f * Time.deltaTime
            );
        }

        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (p != null)
            {
                if (p.fishBag.Count < p.bagSize)
                {
                    p.fishBag.Add(this);

                    Debug.Log("Picked up fish worth $" + cost);

                    gameObject.SetActive(false);
                }
                else
                {
                    Debug.Log("Bag Full");
                }
            }
        }
    }

    void PickNewDirection()
    {
        moveDirection = new Vector3(
            Random.Range(-1f, 1f),
            Random.Range(-0.5f, 0.5f),
            Random.Range(-1f, 1f)
        ).normalized;
    }

    IEnumerator ChangeDirectionRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(directionChangeTime);

            PickNewDirection();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}