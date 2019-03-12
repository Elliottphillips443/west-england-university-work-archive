using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class Panel : MonoBehaviour {

    public Button firstButton;

    virtual protected void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(firstButton.gameObject);
        firstButton.OnSelect(null);
    }
}
