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
    public class Ball : MonoBehaviour
    {
        bool _hitOrangeRow;
        bool _hitRedRow;

        bool _hitTopWall;

        float speed = 14f;
        float speedModifier = 0f;

        Rigidbody2D rb;

        Paddle _paddle;

        Vector2 movement;

        bool _isAlive = true;

        public bool invulnerable = false;

        private void Awake() {
            rb = GetComponent<Rigidbody2D>();
            _paddle = FindObjectOfType<Paddle>();
            if(GameStatus.Difficulty == 0) {
                speed = 10f;
            }
        }

        private void Start() {
            speedModifier = GameStatus.GetSpeedModifier();
            Debug.Log(speedModifier + ", " + GameStatus.GetSpeedModifier());
        }

        private void FixedUpdate() {

            Vector2 vel = rb.velocity;

            if(vel.x < 0) {
                vel.x = -(speed + speedModifier);
            } else {
                vel.x = (speed + speedModifier);
            }

            if(vel.y < 0) {
                vel.y = -(speed + speedModifier);
            } else {
                vel.y = (speed + speedModifier);
            }
            rb.velocity = vel;
            speedModifier = GameStatus.GetSpeedModifier();
        }

        void AddBounce() {
            GameStatus.AddBounce();
        }

        public void ShootBall(bool value = false) {
            if(value) {
                rb.AddForce(new Vector2(-speed, speed), ForceMode2D.Impulse);
            } else {
                rb.AddForce(new Vector2(speed, speed), ForceMode2D.Impulse);
            }
        }

        void Death() {
            if(invulnerable) {
                return;
            }
            if(_isAlive) {
                GameStatus.LoseBall();
                gameObject.SetActive(false);
            }
            _isAlive = false;
        }

        Brick _brick;

        private void OnCollisionEnter2D(Collision2D collision) {
            Vector3 rot = transform.eulerAngles;
            /*
            Vector2 inDirection = rb.velocity;

            while((Mathf.Abs(inDirection.x) + Mathf.Abs(inDirection.y)) < speed) {
                if(inDirection.x < 0) {
                    inDirection.x -= .1f;
                } else {
                    inDirection.x += .1f;
                }
                if(inDirection.y < 0) {
                    inDirection.y -= .1f;
                } else {
                    inDirection.y += .1f;
                }
            }

            Vector2 inNormal = collision.contacts[0].normal.normalized;

            Debug.Log(inDirection);
            Debug.Log(inNormal);
            Debug.Log(Vector2.Reflect(inDirection, inNormal));

            rb.velocity = Vector2.Reflect(inDirection, inNormal);
            */
            //transform.eulerAngles = rot;

            if(collision.gameObject.name == "Wall-Bottom") {
                Death();
            } else {
                if(collision.gameObject.name == "Wall-Top") {
                    if(!_hitTopWall) {
                        _hitTopWall = true;
                        _paddle.ShrinkPaddle();
                    }
                }
                if(collision.gameObject.GetComponent<Paddle>()) {
                    AddBounce();
                }
                if(_brick = collision.gameObject.GetComponent<Brick>()) {
                    AddBounce();

                    Brick.BrickColor _brickColor = _brick.Hit();

                    if(!GameStatus.HasHitOrangeRow) {
                        if(_brickColor == Brick.BrickColor.Orange) {
                            GameStatus.HasHitOrangeRow = true;
                        }
                    }
                    if(!GameStatus.HasHitRedRow) {
                        if(_brickColor == Brick.BrickColor.Red) {
                            GameStatus.HasHitRedRow = true;
                        }
                    }
                }
            }
        }

    }
}