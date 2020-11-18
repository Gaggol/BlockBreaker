using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Pause : MonoBehaviour
{

    public GameObject HUDPause_Panel;

    bool isShown;

    private void Awake() {
        isShown = false;
        HUDPause_Panel.SetActive(isShown);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            isShown = !isShown;
            HUDPause_Panel.SetActive(isShown);
            if(isShown) {
                Time.timeScale = 0;
            } else {
                Time.timeScale = 1;
            }
        }
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void ResumeGame() {
        Time.timeScale = 1;
        isShown = false;
        HUDPause_Panel.SetActive(false);
    }
}
