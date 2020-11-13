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
    public class BrickGenerator : MonoBehaviour
    {

        public GameObject brickGO;
        public Sprite damagedSprite;

        int _mapX = 14;
        int _mapY = 7;

        private void Awake() {
            if(brickGO == null) {
                Debug.LogError("BRICKGO MISSING");
            }
            if(damagedSprite == null) {
                Debug.LogError("DAMAGEDSPRITE MISSING");
            }
        }

        // Start is called before the first frame update
        void Start() {
            for(int y = 0; y <= _mapY; y++) {
                for(int x = 0; x < _mapX; x++) {
                    Vector3 pos = Vector3.zero;
                    pos.x = pos.x + (x * 2);
                    pos.y = pos.y - y;

                    GameObject newGO = Instantiate(brickGO, transform);
                    newGO.transform.localPosition = pos;

                    Brick brick = newGO.AddComponent<Brick>();

                    if(y == 0 || y == 1) {
                        brick.Setup(damagedSprite, Brick.BrickColor.Red);
                        brick.name = y + ", " + x + ", RED";
                    }
                    if(y == 2 || y == 3) {
                        brick.Setup(damagedSprite, Brick.BrickColor.Orange);
                        brick.name = y + ", " + x + ", Orange";
                    }
                    if(y == 4 || y == 5) {
                        brick.Setup(damagedSprite, Brick.BrickColor.Green);
                        brick.name = y + ", " + x + ", Green";
                    }
                    if(y == 6 || y == 7) {
                        brick.Setup(damagedSprite, Brick.BrickColor.Yellow);
                        brick.name = y + ", " + x + ", Yellow";
                    }
                }
            }
        }
    }
}