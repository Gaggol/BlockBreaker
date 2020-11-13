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
    public static class Score
    {
        static int _score = 0;

        // TODO: ADD HIGH SCORE
        // TODO: ADD SAVING HIGH SCORE

        public static void Reset() {
            _score = 0;
        }

        static void _addScore(int score) {
            if(GameStatus.Difficulty == 0) {
                score = score / 2;
                if(score <= 0) {
                    _score += 1;
                } else {
                    _score += score;
                }
            }
            if(GameStatus.Difficulty == 1) {
                _score += score;
            }
            if(GameStatus.Difficulty == 2) {
                _score += score * 2;
            }
        }

        public static void AddScore(Brick.BrickColor brickColor) {
            switch(brickColor) {
                case Brick.BrickColor.Yellow:
                    _addScore(1);
                    break;
                case Brick.BrickColor.Green:
                    _addScore(3);
                    break;
                case Brick.BrickColor.Orange:
                    _addScore(5);
                    break;
                case Brick.BrickColor.Red:
                    _addScore(7);
                    break;
            }
        }

        public static int GetScore() {
            return _score;
        }
    }
}