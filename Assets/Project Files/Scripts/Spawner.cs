using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

using static Sound;
using static AudioManager;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject enemy;

    public float startTimeBtwSpawns;

    float timeBtwSpawns;

    static bool ok = false;
    public AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        timeBtwSpawns = startTimeBtwSpawns;
        ok = false;
    }

    // Update is called once per frame
    void Update()
    {

        if(PhotonNetwork.IsMasterClient == false || PhotonNetwork.CurrentRoom.PlayerCount != 2)
        {
            return;
        }

        if(ok == false)
        {
            audioManager.PlaySound("Main_theme");
            ok = true;
        }

        if(FindObjectOfType<Health>().health <= 0)
        {
            audioManager.StopSound("Main_theme");
            Enemy[] enemies = FindObjectsOfType<Enemy>();
            foreach (Enemy e in enemies)
            {
                PhotonNetwork.Destroy(e.gameObject);
            }
        }
        
        if(spawnPoints.Length <= 1)
        {
            return;
        }

        if (timeBtwSpawns <= 0 && FindObjectOfType<Health>().health > 0)
        {
            Vector3 spawnPosition = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
            audioManager.PlaySound("Monster_spawn");
            //spawnam
            PhotonNetwork.Instantiate(enemy.name, spawnPosition, Quaternion.identity);
            timeBtwSpawns += startTimeBtwSpawns;
        }
        else
        {
            timeBtwSpawns -= Time.deltaTime;
        }
    }
}