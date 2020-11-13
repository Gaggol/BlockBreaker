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
    public class Brick : MonoBehaviour
    {

        public enum BrickColor { Yellow, Green, Orange, Red }

        BrickColor _brickColor;

        public void Setup(Sprite damagedSprite, BrickColor brickColor, int health = 2) {
            _brickColor = brickColor;
            _damagedSprite = damagedSprite;
            _health = health;
        }

        int _health;

        bool Dead;

        Sprite _damagedSprite;

        SpriteRenderer _spriteRenderer;

        private void Awake() {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start() {

            switch(_brickColor) {
                case BrickColor.Yellow:
                    _spriteRenderer.color = new Color(.75f, .75f, .25f);
                    if(GameStatus.Difficulty == 0 || GameStatus.Difficulty == 1) {
                        _health = 1;
                    }
                    GameStatus.YellowBricks += 1;
                    GameStatus.AliveYellowBricks += 1;
                    break;
                case BrickColor.Green:
                    _spriteRenderer.color = new Color(0f, .75f, 0f);
                    if(GameStatus.Difficulty == 0 || GameStatus.Difficulty == 1) {
                        _health = 1;
                    }
                    GameStatus.GreenBricks += 1;
                    GameStatus.AliveGreenBricks += 1;
                    break;
                case BrickColor.Orange:
                    _spriteRenderer.color = new Color(1f, .5f, 0f);
                    GameStatus.OrangeBricks += 1;
                    GameStatus.AliveOrangeBricks += 1;
                    break;
                case BrickColor.Red:
                    _spriteRenderer.color = new Color(.75f, 0f, 0f);
                    GameStatus.RedBricks += 1;
                    GameStatus.AliveRedBricks += 1;
                    break;
                default:
                    _spriteRenderer.color = Color.magenta;
                    Debug.LogError("Wrong BrickColor");
                    break;
            }
        }

        public BrickColor Hit(bool value = false) {
            if(value) {
                _health = 0;
            }
            _health -= 1;
            _spriteRenderer.sprite = _damagedSprite;
            if(_health <= 0) {
                if(Dead) {
                    return _brickColor;
                }
                Dead = true;
                gameObject.SetActive(false);
                switch(_brickColor) {
                    case BrickColor.Yellow:
                        GameStatus.AliveYellowBricks -= 1;
                        break;
                    case BrickColor.Green:
                        GameStatus.AliveGreenBricks -= 1;
                        break;
                    case BrickColor.Orange:
                        GameStatus.AliveOrangeBricks -= 1;
                        break;
                    case BrickColor.Red:
                        GameStatus.AliveRedBricks -= 1;
                        break;
                }
                Score.AddScore(_brickColor);
            }
            return _brickColor;
        }

    }
}