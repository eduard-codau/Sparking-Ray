using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSwitch : MonoBehaviour
{
    [SerializeField]
    private Sprite[] switchSprites;

    private int state;

    private Image switchImage;

    // Start is called before the first frame update
    void Start()
    {
        state = 0;
        switchImage = GetComponent<Button>().image as Image;
        switchImage.sprite = switchSprites[state];

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchState()
    {
        if (state == 0)
        {
            state = 1;
        }
        else
        {
            state = 0;
        }

        switchImage.sprite = switchSprites[state];
    }
}

