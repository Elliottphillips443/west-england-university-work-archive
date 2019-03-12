using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class EndPanel : Panel
{
    float timer = 0;
    public int winnerNumber = 0;
    public Text winText;
    public Sprite winPic;
    public Image image;
    protected override void OnEnable()
    {
        base.OnEnable();
        firstButton.gameObject.SetActive(false);
        winText.text = "P" + winnerNumber + " Wins!";
        image.sprite = winPic;
        timer = 0;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(timer > 3f && !firstButton.gameObject.activeSelf)
        {
            firstButton.gameObject.SetActive(true);
            EventSystem.current.SetSelectedGameObject(firstButton.gameObject);
            firstButton.OnSelect(null);
        }
    }
}
