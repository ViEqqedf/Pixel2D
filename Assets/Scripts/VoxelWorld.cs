using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Voxel2D {
    public class VoxelWorld : MonoBehaviour {
        public const int WORLD_BLOCK_SIZE = 256;
        public const float TICK_INTERVAL = 0.05f;

        public static VoxelWorld instance { get; private set; }
        private Pool pool;
        private float countDownTimer = 0;
        private int simulationCount = 1;

        private void Awake() {
            if (instance == null) {
                instance = this;
                DontDestroyOnLoad(gameObject);
            } else {
                Destroy(gameObject);
            }
        }

        private void Start() {
            pool = new Pool(WORLD_BLOCK_SIZE);
        }

        private void FixedUpdate() {
            countDownTimer += Time.fixedDeltaTime;
            if (countDownTimer >= TICK_INTERVAL) {
                for (int i = WORLD_BLOCK_SIZE - 1, countX = -1; i > countX; i--) {
                    for (int j = WORLD_BLOCK_SIZE - 1, countY = -1; j > countY; j--) {
                        pool.container[i, j].UnitUpdate(simulationCount);
                    }
                }

                countDownTimer = 0;
                simulationCount++;
            }
        }
    }
}