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
    public static class GameStatus
    {
        public static bool HasHitOrangeRow { get; set; }
        public static bool HasHitRedRow { get; set; }

        public static int Bounces { get; private set; }

        public static int RedBricks { get; set; }
        public static int OrangeBricks { get; set; }
        public static int GreenBricks { get; set; }
        public static int YellowBricks { get; set; }

        public static int AliveRedBricks { get; set; }
        public static int AliveOrangeBricks { get; set; }
        public static int AliveGreenBricks { get; set; }
        public static int AliveYellowBricks { get; set; }

        private static int _aliveBalls = 1;

        private static float _speedModifier = 0f;

        public static int Difficulty = 1;

        public static bool hasWon = false;

        public static void Reset() {
            hasWon = false;
            Score.Reset();
            HasHitOrangeRow = false;
            HasHitRedRow = false;
            Bounces = 0;
            RedBricks = 0;
            OrangeBricks = 0;
            GreenBricks = 0;
            YellowBricks = 0;
            AliveRedBricks = 0;
            AliveOrangeBricks = 0;
            AliveGreenBricks = 0;
            AliveYellowBricks = 0;
            _aliveBalls = 1;
            _speedModifier = 0;
        }

        public static int GetAliveBricks() {
            return AliveRedBricks + AliveOrangeBricks + AliveGreenBricks + AliveYellowBricks;
        }

        public static int GetTotalBricks() {
            return RedBricks + OrangeBricks + GreenBricks + YellowBricks;
        }

        public static float GetSpeedModifier() {
            _speedModifier = 0f;
            if(Bounces >= 4) {
                _speedModifier += .25f;
            }
            if(Bounces >= 12) {
                _speedModifier += .25f;
            }
            if(HasHitRedRow) {
                _speedModifier += .25f;
            }
            if(HasHitOrangeRow) {
                _speedModifier += .25f;
            }
            if(Difficulty == 0) {
                return _speedModifier / 2f;
            }
            if(Difficulty == 1) {
                return _speedModifier;
            }
            if(Difficulty == 2) {
                return _speedModifier * 2f;
            }
            return _speedModifier;
        }

        public static void AddBounce() {
            Bounces += 1;
        }

        public static void AddBall() {
            _aliveBalls += 1;
        }

        public static int GetBalls() {
            return _aliveBalls;
        }

        public static void LoseBall() {
            _aliveBalls -= 1;
        }
    }
}