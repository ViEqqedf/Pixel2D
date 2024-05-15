using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace Voxel2D {
    public class VoxelRenderer : MonoBehaviour {
        public static CommandBuffer renderCmd;

        private void Update() {
            renderCmd.Clear();
            // renderCmd.DrawProceduralIndirect();
        }
    }
}