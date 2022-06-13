using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    bool isRunning;
    bool isWalking;
    PhotonView view;
    Animator anim;
    Health healthScript;

    LineRenderer rend;

    bool facingRight = true;

    public float minX, maxX, minY, maxY;

    public Text nameDisplay;
    // Start is called before the first frame update

    void Start()
    {
        view = GetComponent<PhotonView>();
        anim = GetComponent<Animator>();
        healthScript = FindObjectOfType<Health>();
        rend = FindObjectOfType<LineRenderer>();


        if (view.IsMine)
        {
            nameDisplay.text = PhotonNetwork.NickName;
        }
        else
        {
            nameDisplay.text = view.Owner.NickName;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (view.IsMine)
        {

            Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            Vector2 moveAmount = moveInput.normalized * speed * Time.deltaTime;
            transform.position += (Vector3)moveAmount;

            Wrap();

            if((moveInput[0]<0 && facingRight))
            {
                flip();
            }
            else if((moveInput[0]>0 && !facingRight))
            {
                flip();
            }

            if(moveInput == Vector2.zero)
            {
                speed = 5;
                anim.SetBool("isRunning", false);
                anim.SetBool("isWalking",false);
            }
            else
            {
  
                if (Input.GetKey(KeyCode.Space) == true)
                {
                    anim.SetBool("isRunning", true);
                    speed = 10;
                }
                else if (Input.GetKey(KeyCode.Space) == false)
                {
                    anim.SetBool("isWalking", true);
                    anim.SetBool("isRunning", false);
                    speed = 5;
                }
            }

            rend.SetPosition(0, transform.position);
        }
        else
        {
            rend.SetPosition(1, transform.position);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(view.IsMine)
        {
            if (collision.tag == "Enemy")
            {
                healthScript.TakeDamage();
            }
        }
    }

    void flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);

        nameDisplay.transform.Rotate(0f, -180f, 0f);

    }

    void Wrap()
    {
        if(transform.position.x < minX)
        {
            transform.position = new Vector2(maxX, transform.position.y);
        }
        if (transform.position.x > maxX)
        {
            transform.position = new Vector2(minX, transform.position.y);
        }
        if (transform.position.y < minY)
        {
            transform.position = new Vector2(transform.position.x, maxY);
        }
        if (transform.position.y > maxY)
        {
            transform.position = new Vector2(transform.position.x, minY);
        }
    }
}
