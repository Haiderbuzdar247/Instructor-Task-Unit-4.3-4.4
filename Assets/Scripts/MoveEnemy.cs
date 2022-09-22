using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator anim;
    public NavMeshAgent agent;
    public GameObject Explode;
    private Vector3 playerpos;
    private float _health;
    public bool haveStickyBomb;
    public float Health
    {
        get
        {
            return _health;
        }
    }
    void Start()
    {
        _health = Random.Range(3, 7);
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.instance != null)
        {
            playerpos = Player.instance.transform.position;
            agent.SetDestination(playerpos);
        }
    }
    public void GetDamage()
    {

    }
    public void DestroyObject()
    {
        Destroy(this.gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            Destroy(gameObject);

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("StickyBomb"))
        {
            if (!haveStickyBomb)
            {
                haveStickyBomb = true;
                other.gameObject.transform.parent = this.transform;
                StartCoroutine(DestroyGameObject());
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Mine"))
        {
            Instantiate(Explode, transform.position, transform.rotation);
            Destroy(gameObject);
            Destroy(other.gameObject);
            SpawnManager.Instance.audioplay();
            SpawnManager.Instance.scoreadd();
        }
    }
    
    private IEnumerator DestroyGameObject()
    {
        yield return new WaitForSeconds(3f);
        Instantiate(Explode, transform.position, transform.rotation);
        SpawnManager.Instance.scoreadd();
        SpawnManager.Instance.audioplay();
        Destroy(gameObject);
    }
}
