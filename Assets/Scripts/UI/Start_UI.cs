using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

//
//    ____            _____             ____         ____         ____       ___
//   /  _ \          /     \           /  _ \       /  _ \       / __ \     |   |
//  /  / \_\        /  / \  \         /  / \_\     /  / \_\     / /  \ \    |   |
// |  |            /  /   \  \       |  |         |  |         | |    | |   |   |
// |  |  ___      /  /_____\  \      |  |  ___    |  |  ___    | |    | |   |   |
// |  | |_  |    /  /_______\  \     |  | |_  |   |  | |_  |   | |    | |   |   |
// |  |   | |   /  /         \  \    |  |   | |   |  |   | |   | |    | |   |   |_______
//  \  \_/ /   /  /           \  \    \  \_/ /     \  \_/ /     \ \__/ /    |           |
//   \____/   /__/             \__\    \____/       \____/       \____/     |___________|
//
// github.com/Gaggol

namespace Gaggol
{
    public class Start_UI : MonoBehaviour
    {

        private void Update() {
            if(Input.GetKeyDown(KeyCode.Escape)) {
                Application.Quit();
            }
        }

        public void PlayButton() {
            GameStatus.Reset();
            SceneManager.LoadScene(1);
        }

        public void HandleInputData(int value) {
            if(value == 0) {
                GameStatus.Difficulty = 0;
            }
            if(value == 1) {
                GameStatus.Difficulty = 1;
            }
            if(value == 2) {
                GameStatus.Difficulty = 2;
            }
            if(value < 0 || value > 2) {
                throw new System.ArgumentOutOfRangeException();
            }
        }
    }
}