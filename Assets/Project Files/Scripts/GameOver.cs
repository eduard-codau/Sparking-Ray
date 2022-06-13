using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

using static Sound;
using static AudioManager;

public class GameOver : MonoBehaviour
{
    public Text scoreDisplay;
    public GameObject restartButton;
    public GameObject waitingText;

    PhotonView view;

    public AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
        scoreDisplay.text = "Score: " + FindObjectOfType<Score>().score.ToString();

        if(PhotonNetwork.IsMasterClient == false)
        {
            restartButton.SetActive(false);
            waitingText.SetActive(true);
            audioManager.StopSound("Main_theme");
            FindObjectOfType<AudioManager>().PlaySound("Game_over");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickRestart()
    {
        view.RPC("Restart", RpcTarget.All);
    }

    [PunRPC]
    void Restart()
    {
        // pentru salvarea scorului in DB o singura data se poate folosi un
        // if(PhotonNetwork.IsMasterClient)
        PhotonNetwork.LoadLevel("Game");
    }
}
