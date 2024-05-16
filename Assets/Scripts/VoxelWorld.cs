using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Voxel2D {
    public class VoxelWorld : MonoBehaviour {
        public const int WORLD_BLOCK_SIZE = 256;
        public const float TICK_INTERVAL = 0.05f;

        public ShaderResourcesAsset shaderResourcesAsset;
        public static VoxelWorld Instance { get; private set; }
        private VoxelRenderer rendererInstance;
        private Pool pool;
        private float countDownTimer = 0;
        private int simulationCount = 1;

        private void Awake() {
            if (VoxelWorld.Instance == null) {
                VoxelWorld.Instance = this;
                DontDestroyOnLoad(gameObject);
            } else {
                Destroy(gameObject);
            }

            rendererInstance?.Dispose();
            if (shaderResourcesAsset != null) {
                rendererInstance = new VoxelRenderer(shaderResourcesAsset);
                rendererInstance.Initialize();
            } else {
                Debug.LogError("ShaderResourcesAsset is empty!");
            }
        }

        private void Start() {
            pool = new Pool(WORLD_BLOCK_SIZE);
        }

        private void OnDestroy() {
            rendererInstance.Dispose();
            rendererInstance = null;
        }

        private void Update() {
            // rendererInstance.Tick();
        }

        private void FixedUpdate() {
            countDownTimer += Time.fixedDeltaTime;
            if (countDownTimer >= TICK_INTERVAL) {
                for (int i = WORLD_BLOCK_SIZE - 1, countX = -1; i > countX; i--) {
                    for (int j = WORLD_BLOCK_SIZE - 1, countY = -1; j > countY; j--) {
                        pool.Container[i, j].UnitUpdate(simulationCount);
                    }
                }

                countDownTimer = 0;
                simulationCount++;
            }
        }
    }
}