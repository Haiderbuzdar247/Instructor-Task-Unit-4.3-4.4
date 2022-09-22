using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpawnManager : MonoBehaviour
{
    public AudioSource gameplay;
    public AudioClip spawn1;
    public AudioClip spawn2;
    public AudioClip spawn3;
    public TMP_Text points;
    public static int score = 0;
    public static SpawnManager Instance;
    public GameObject enemy;
    public GameObject bomb;
    public GameObject gems;
    public GameObject mines;
    public GameObject stickybomb;
    public int enemyCount;
    public int waveCount = 1;
    public bool multipickUP;
    public GameObject multipicks;
    private void Awake()
    {

        Instance = this;
    }
    void Start()
    {
        //gems.SetActive(false);
        //StartCoroutine(gemsappear());
        gameplay = GetComponent<AudioSource>();
        points.text = score.ToString();
        InvokeRepeating(nameof(SpawnMultiBomb), 2f, 15f);
    }
    void SpawnMultiBomb()
    {
        Instantiate(multipicks, GenerateSpawnPosition(), Quaternion.identity);
    }
    // Update is called once per frame
    //IEnumerator apeargems()
    //{

    //}
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (GameObject.FindGameObjectsWithTag("Mine").Length ==0)
            {
                Instantiate(mines, Player.instance.transform.position, Quaternion.identity);
                gameplay.PlayOneShot(spawn3);
            }
        }
        enemyCount = FindObjectsOfType<MoveEnemy>().Length;
        if (enemyCount == 0)
        {
            SpawnEnemyWave(waveCount);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (multipickUP)
            {
                for (int i = 0; i < 4; i++)
                {
                    Instantiate(bomb, new Vector3(Player.instance.transform.position.x, Player.instance.transform.position.y + 2, Player.instance.transform.position.z), Player.instance.transform.rotation);
                    gameplay.PlayOneShot(spawn1);
                }

            }
            else
            {
                Instantiate(bomb, new Vector3(Player.instance.transform.position.x, Player.instance.transform.position.y + 2, Player.instance.transform.position.z), Player.instance.transform.rotation);
                gameplay.PlayOneShot(spawn3);
            }


        }
        float stcikyBombCounts = GameObject.FindGameObjectsWithTag("StickyBomb").Length;
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (stcikyBombCounts == 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    Instantiate(stickybomb, new Vector3(Random.Range(Player.instance.transform.position.x - 10, Player.instance.transform.position.x + 10), Player.instance.transform.position.y, Random.Range(Player.instance.transform.position.z - 10, Player.instance.transform.position.z + 10)), Player.instance.transform.rotation);
                    gameplay.PlayOneShot(spawn3);
                }
            }


        }
        
    }
    void SpawnEnemyWave(int enemytospawn)
    {
        for (int i = 0; i < enemytospawn; i++)
        {
            Instantiate(enemy, GenerateSpawnPosition(), enemy.transform.rotation);
        }
        waveCount++;
    }
    Vector3 GenerateSpawnPosition()
    {
        float xPos = Random.Range(-5, 9);
        float zPos = Random.Range(-7, 8);
        return new Vector3(xPos, 0.5f, zPos);
    }
    public void scoreadd()
    {
        score++;
        points.text = score.ToString();
    }
    public void audioplay()
    {
        gameplay.PlayOneShot(spawn2);
    }
}
