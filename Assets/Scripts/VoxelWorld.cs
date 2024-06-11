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
            Material mat = rendererIns.Initialize();
            GetComponent<MeshRenderer>().material = mat;
        }

        private void OnDestroy() {
            rendererIns.Dispose();
            rendererIns = null;
        }

        private void Update() {
            List<Color> colorList = new List<Color>();
            for (int i = 0; i < PublicBoard.WORLD_BLOCK_SIZE; i++) {
                for (int j = 0; j < PublicBoard.WORLD_BLOCK_SIZE; j++) {
                    colorList.Add(pool.Container[i, j].color);
                }
            }

            rendererIns.Tick(colorList.ToArray());
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