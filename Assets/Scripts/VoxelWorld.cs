using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Voxel2D {
    public class VoxelWorld : MonoBehaviour {
        public static VoxelWorld Instance { get; private set; }
        private VoxelRenderer rendererIns;
        private Pool pool;
        private float countDownTimer = 0;
        private int simulationCount = 1;
        private List<ushort> triangleList;

        private void Awake() {
            if (VoxelWorld.Instance == null) {
                VoxelWorld.Instance = this;
                DontDestroyOnLoad(gameObject);
            } else {
                Destroy(gameObject);
                return;
            }
        }

        private void Start() {
            pool = new Pool(PublicBoard.WORLD_BLOCK_SIZE);

            rendererIns?.Dispose();
            rendererIns = new VoxelRenderer();
            rendererIns.Initialize();
        }

        private void OnDestroy() {
            rendererIns.Dispose();
            rendererIns = null;
        }

        private void Update() {
            rendererIns.Tick(default);
        }

        private void FixedUpdate() {
            countDownTimer += Time.fixedDeltaTime;
            if (countDownTimer >= PublicBoard.TICK_INTERVAL) {
                for (int i = PublicBoard.WORLD_BLOCK_SIZE - 1, countX = -1; i > countX; i--) {
                    for (int j = PublicBoard.WORLD_BLOCK_SIZE - 1, countY = -1; j > countY; j--) {
                        pool.Container[i, j].UnitUpdate(simulationCount);
                    }
                }

                countDownTimer = 0;
                simulationCount++;
            }
        }
    }
}