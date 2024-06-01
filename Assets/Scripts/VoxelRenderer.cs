using UnityEngine;

namespace Voxel2D {
    public class VoxelRenderer {
        public Texture2D renderTex;

        public void Initialize() {
            renderTex = new Texture2D(PublicBoard.WORLD_BLOCK_SIZE, PublicBoard.WORLD_BLOCK_SIZE);
        }

        public void Tick(Color[] pixels) {
            renderTex.SetPixels(pixels);
        }

        public void Dispose() {
        }
    }
}