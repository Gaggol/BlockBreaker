using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public class Paddle : MonoBehaviour
    {

        Vector3 origScale;
        Vector3 currScale;

        public bool HasStarted;

        public GameObject ballGhost;
        public GameObject ballSpawn;

        public GameObject ballPrefab;

        float ballSpeedModifier = 0f;
        int _bounces = 0;

        float paddleSpeed = 14f;

        bool yellowHalfSpawn;
        bool yellowFullSpawn;
        bool greenHalfSpawn;
        bool greenFullSpawn;
        bool orangeHalfSpawn;
        bool orangeFullSpawn;
        bool redHalfSpawn;
        bool redFullSpawn;

        Rigidbody2D rb;

        private void Awake() {
            rb = GetComponent<Rigidbody2D>();
            origScale = transform.localScale;
            currScale = origScale;
        }

        Vector2 movement;

        public float GetSpeedModifier() {
            return ballSpeedModifier;
        }

        // Start is called before the first frame update
        void Start() {

        }

        private void FixedUpdate() {
            rb.velocity = movement * paddleSpeed;
        }

        bool bricksPaddleScaled = false;

        bool StartedWinSpawn = false;

        IEnumerator WinSpawn() {
            while(true) {
                AddBall();
                yield return new WaitForSeconds(0.1f);
            }
        }

        // Update is called once per frame
        void Update() {
            if(GameStatus.hasWon) {
                if(!StartedWinSpawn) {
                    StartedWinSpawn = true;
                    transform.position = new Vector2(0, -13);
                    SpriteRenderer sr = GetComponent<SpriteRenderer>();
                    sr.enabled = false;
                    Ball[] balls = FindObjectsOfType<Ball>();
                    for(int i = 0; i < balls.Length; i++) {
                        Destroy(balls[i].gameObject);
                    }
                    gameObject.GetComponent<Collider2D>().enabled = false;
                    StartCoroutine(WinSpawn());
                }
            }
            if(currScale.x > 15f) {
                currScale.x = 15f;
            }
            if(currScale.x <= 0f) {
                currScale.x = .5f;
            }
            transform.localScale = currScale;
            if(!bricksPaddleScaled) {
                try {
                    if((GameStatus.GetTotalBricks() / GameStatus.GetAliveBricks()) == 2) {
                        currScale.x -= .5f;
                        bricksPaddleScaled = true;
                    }
                }
                catch(System.DivideByZeroException) { }
            }
            movement = Vector2.zero;
            if(!GameStatus.hasWon) {
                movement.x = Input.GetAxisRaw("Horizontal");
            }
            if(Input.GetKey(KeyCode.LeftShift)) {
                if(Input.GetKeyDown(KeyCode.UpArrow)) {
                    AddBall();
                }
                if(Input.GetKeyDown(KeyCode.DownArrow)) {
                    currScale.x += .5f;
                }
                if(Input.GetKeyDown(KeyCode.L)) {
                    Brick[] bricks = FindObjectsOfType<Brick>();
                    for(int i = 0; i < bricks.Length; i++) {
                        bricks[i].Hit(true);
                    }
                }
            }
            if(Input.GetKey(KeyCode.LeftAlt)) {
                if(Input.GetKeyDown(KeyCode.DownArrow)) {
                    currScale.x -= .5f;
                }
            }

            if(!HasStarted) {
                if(Input.GetKeyDown(KeyCode.Space)) {
                    Vector3 pos = transform.position;
                    pos.y += 1f;

                    ballGhost.SetActive(false);


                    GameObject newBall = Instantiate(ballPrefab);

                    newBall.transform.position = pos;
                    newBall.GetComponent<Ball>().ShootBall();

                    if(GameStatus.Difficulty == 0) {
                        AddBall();
                        //GameStatus.AddBall();
                    }

                    HasStarted = true;
                }
            }
            if(GameStatus.GetBalls() <= 0) {
                Death();
            }
            if(!yellowHalfSpawn) {
                try {
                    if((GameStatus.YellowBricks / GameStatus.AliveYellowBricks) == 2) {
                        yellowHalfSpawn = true;
                        AddBall();
                    }
                }
                catch(System.DivideByZeroException) { 
                    // IF WE HIT DIVIDE BY ZERO, THEN THE MAP IS REALLY SMALL
                    yellowHalfSpawn = true;
                    orangeHalfSpawn = true;
                    greenHalfSpawn = true;
                    redHalfSpawn = true;
                    yellowFullSpawn = true;
                    orangeFullSpawn = true;
                    greenFullSpawn = true;
                    redFullSpawn = true;
                }
            } else {
                if(!yellowFullSpawn) {
                    if(GameStatus.AliveYellowBricks == 0) {
                        yellowFullSpawn = true;
                        AddBall();
                    }
                }
            }
            if(!orangeHalfSpawn) {
                if((GameStatus.OrangeBricks / GameStatus.AliveOrangeBricks) == 2) {
                    orangeHalfSpawn = true;
                    AddBall();
                }
            } else {
                if(!orangeFullSpawn) {
                    if(GameStatus.AliveOrangeBricks == 0) {
                        orangeFullSpawn = true;
                        AddBall();
                    }
                }
            }
            if(!greenHalfSpawn) {
                if((GameStatus.GreenBricks / GameStatus.AliveGreenBricks) == 2) {
                    greenHalfSpawn = true;
                    AddBall();
                }
            } else {
                if(!greenFullSpawn) {
                    if(GameStatus.AliveGreenBricks == 0) {
                        greenFullSpawn = true;
                        AddBall();
                    }
                }
            }
            if(!redHalfSpawn) {
                if((GameStatus.RedBricks / GameStatus.AliveRedBricks) == 2) {
                    redHalfSpawn = true;
                    AddBall();
                }
            } else {
                if(!redFullSpawn) {
                    if(GameStatus.AliveRedBricks == 0) {
                        redFullSpawn = true;
                        AddBall();
                    }
                }
            }
        }

        public void AddBall() {
            if(GameStatus.hasWon) {
                Vector3 pos = transform.position;

                GameObject newBall = Instantiate(ballPrefab);
                newBall.transform.position = pos;
                newBall.GetComponent<Ball>().ShootBall();
                newBall.GetComponent<Ball>().invulnerable = true;

                newBall = Instantiate(ballPrefab);
                newBall.transform.position = pos;
                newBall.GetComponent<Ball>().ShootBall(true);
                newBall.GetComponent<Ball>().invulnerable = true;
            } else {
                Vector3 pos = transform.position;
                pos.y += 1f;

                GameObject newBall = Instantiate(ballPrefab);
                newBall.transform.position = pos;
                newBall.GetComponent<Ball>().ShootBall();
                GameStatus.AddBall();
            }
        }

        void Death() {
            Debug.Log("Dead");
        }

        bool hasShrunkPaddle;

        public void ShrinkPaddle() {
            if(hasShrunkPaddle) {
                return;
            } else {
                hasShrunkPaddle = true;
            }
            currScale.x -= .5f;
        }
    }
}