using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeGameButton : ButtonBaseClass {

    public GameObject pauseMenuObject;

    protected override void OnClickTask()
    {
        pauseMenuObject.GetComponent<PauseMenu>().ResumeGame();
    }
}
