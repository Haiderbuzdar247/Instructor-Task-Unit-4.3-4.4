using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Bomb : MonoBehaviour
{

    public float radius;
    public LayerMask enemyMask;
    public List<Collider> enemies;
    public Rigidbody rb;
    public GameObject particleEffect;
    // Start is called before the first frame update
    void Start()
    {
        rb.AddForce(Vector3.up * 10f, ForceMode.Impulse);
        rb.AddForce(Vector3.forward * 10f, ForceMode.Impulse);
        StartCoroutine(Blast());
    }
    IEnumerator Blast()
    {
        yield return new WaitForSeconds(3f);
        enemies = Physics.OverlapSphere(transform.position, radius, enemyMask).ToList();
        if (enemies.Count != 0)
        {
            Debug.Log(enemyMask);

            foreach (Collider c in enemies)
            {
                if (c.GetComponent<MoveEnemy>() != null)
                {
                    c.GetComponent<MoveEnemy>().DestroyObject();
                    SpawnManager.Instance.scoreadd();
                }
            }
            
        }
        Instantiate(particleEffect, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }

}
