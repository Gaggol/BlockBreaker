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

        int _bounces = 0;
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

        private void Update() {
            if(Input.GetKeyDown(KeyCode.R)) {
                rb.velocity = Vector2.zero;
                rb.AddForce(new Vector2(speed, speed), ForceMode2D.Impulse);
            }
            if(speedModifier < GameStatus.GetSpeedModifier()) {
                AddBallSpeedModifier(GameStatus.GetSpeedModifier());
            }
        }

        void AddBounce() {
            GameStatus.Bounces += 1;
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

        public void AddBallSpeedModifier(float speed) {
            speedModifier = speed;
            
            // FIXME: FIX SPEED MODIFIER

            /*if(rb.velocity.x < 0) {
                rb.AddForce(new Vector2(-speed, 0));
            } else {
                rb.AddForce(new Vector2(speed, 0));
            }
            if(rb.velocity.y < 0) {
                rb.AddForce(new Vector2(0, -speed));
            } else {
                rb.AddForce(new Vector2(0, speed));
            }*/
        }

        Brick _brick;

        private void OnCollisionEnter2D(Collision2D collision) {
            Vector3 rot = transform.eulerAngles;

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