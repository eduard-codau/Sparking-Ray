using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Enemy : MonoBehaviour
{
    PlayerController[] players;
    PlayerController nearestPlayer;
    public float speed;
    bool facingRight = true;

    Score score;

    public GameObject deathFX;
    public PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
        players = FindObjectsOfType<PlayerController>();
        score = FindObjectOfType<Score>();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceOne = Vector2.Distance(transform.position, players[0].transform.position);
        float distanceTwo = Vector2.Distance(transform.position, players[1].transform.position);

        if(distanceOne < distanceTwo)
        {
            nearestPlayer = players[0];
        }
        else
        {
            nearestPlayer = players[1];
        }

        if (nearestPlayer != null)
        {
            
            if ((nearestPlayer.transform.position.x < transform.position.x) && facingRight)
            {
                flip();
            }
            else if ((nearestPlayer.transform.position.x > transform.position.x) && !facingRight)
            {
                flip();
            }
            transform.position = Vector2.MoveTowards(transform.position, nearestPlayer.transform.position, speed*Time.deltaTime);
        }
    }

    void flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(PhotonNetwork.IsMasterClient)
        {
            if (collision.tag == "Ray")
            {
                score.AddScore();
                view.RPC("SpawnParticle", RpcTarget.All);
                PhotonNetwork.Destroy(this.gameObject);
            }
        }
    }

    [PunRPC]
    void SpawnParticle()
    {
        Instantiate(deathFX, transform.position, Quaternion.identity);
    }
}
