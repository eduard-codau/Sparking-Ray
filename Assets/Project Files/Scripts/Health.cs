using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System;
using MongoDB.Driver;

public class Health : MonoBehaviour
{
    public int health = 10;
    public Text healthDisplay;

    PhotonView view;

    public GameObject gameOver;

    private const string MONGO_URI = "mongodb+srv://admin:admin@cluster0.t9phy.mongodb.net/scores?retryWrites=true&w=majority";
    private const string DATABASE_NAME = "scores";
    private MongoClient client;
    private IMongoDatabase db;
    private static bool commited;

    public void TakeDamage()
    {
        view.RPC("TakeDamageRPC", RpcTarget.All);
    }
    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
        client = new MongoClient(MONGO_URI);
        db = client.GetDatabase(DATABASE_NAME);
        commited = false;
    }

    [PunRPC]
    public void TakeDamageRPC()
    {
        health--;

        if (health <= 0)
        {
            gameOver.SetActive(true);
           
        }

        healthDisplay.text = health.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(health<=0 && commited == false)
        {
            commited = true;

            Invoke("InsertIntoDB", 1);
        }
    }

    void InsertIntoDB()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            IMongoCollection<Model_User> userCollection = db.GetCollection<Model_User>("scores");

            // ma adaug pe mine
            Model_User mine = new Model_User();
            mine.score = FindObjectOfType<Score>().score;
            mine.username = PhotonNetwork.PlayerList[0].NickName;//PhotonNetwork.NickName;

            userCollection.InsertOne(mine);

            // adaug si celalalt jucator
            Model_User his = new Model_User();
            his.score = FindObjectOfType<Score>().score;
            his.username = PhotonNetwork.PlayerList[1].NickName;

            userCollection.InsertOne(his);
            //Spawner spawner = FindObjectOfType<Spawner>();

            //Array.Clear(spawner.spawnPoints,0,spawner.spawnPoints.Length);
        }
    }
}
