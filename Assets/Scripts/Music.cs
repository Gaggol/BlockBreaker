using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public class Music : MonoBehaviour
    {

        public Paddle _paddle;

        public AudioClip[] music;
        public AudioClip win;
        public AudioClip highscore;
        public AudioClip gameOver;

        AudioSource _audio;

        bool isPlayingWinMusic;

        bool canPlayMusic = true;

        // TODO: ADD OWN OR GET FREE MUSIC

        void Start() {
            _audio = GetComponent<AudioSource>();
            if(music == null || win == null || highscore == null || gameOver == null) {
                canPlayMusic = false;
            }
            StartCoroutine(PlayMusic());
        }

        private void Update() {
            if(_paddle.HasStarted) {
                if(GameStatus.GetAliveBricks() <= 0) {
                    if(!isPlayingWinMusic) {
                        GameStatus.hasWon = true;
                        isPlayingWinMusic = true;
                        if(canPlayMusic) {
                            _audio.Stop();
                            StopCoroutine(PlayMusic());
                            _audio.clip = win;
                            _audio.Play();
                        }
                    }
                }
                if(GameStatus.GetBalls() <= 0) {
                    if(!GameStatus.hasWon) {
                        if(!isPlayingLoss) {
                            isPlayingLoss = true;
                            if(canPlayMusic) {
                                StartCoroutine(PlayLoss());
                            } else {
                                SceneManager.LoadScene(0);
                            }
                        }
                    }
                }
            }
        }

        bool isPlayingLoss = false;

        IEnumerator PlayLoss() {
            _audio.clip = gameOver;
            _audio.Play();
            yield return new WaitForSeconds(_audio.clip.length);
            SceneManager.LoadScene(0);
        }

        IEnumerator PlayMusic() {
            for(int i = 0; i < music.Length; i++) {
                _audio.clip = music[i];
                _audio.Play();
                yield return new WaitForSeconds(_audio.clip.length);
            }
        }

    }
}