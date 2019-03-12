using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGameButton : ButtonBaseClass {

    protected override void OnClickTask()
    {
        Application.Quit();
    }
}
