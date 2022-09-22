using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Player : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    public static Player instance;
    public Animator animator;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {

    }
    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        if (horizontalInput != 0 || verticalInput != 0)
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Run"))
            {
                animator.SetTrigger("Walk");

            }
        }
        else
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                animator.SetTrigger("Idle");

            }
        }
        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        movementDirection.Normalize();

        transform.Translate(movementDirection * speed * Time.fixedDeltaTime, Space.World);

        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.fixedDeltaTime);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MultiPickUp"))
        {
            SpawnManager.Instance.multipickUP = false;
            StartCoroutine(multibombPickUp());
            Destroy(other.gameObject);
        }
    }

    private IEnumerator multibombPickUp()
    {
        SpawnManager.Instance.multipickUP = true;
        yield return new WaitForSeconds(10f);
        SpawnManager.Instance.multipickUP = false;

    }
}