using UnityEngine;

namespace Voxel2D {
    public class Pool {
        public BaseUnit [,] container { get; }

        public Pool(int poolSize) {
            container = new BaseUnit[poolSize, poolSize];

            VoxelWorld manager = VoxelWorld.instance;
            for (int i = 0, count1 = poolSize; i < count1; i++) {
                for (int j = 0, count2 = poolSize; j < count2; j++) {
                    container[i, j] = new Empty();
                    container[i, j].Init(new Vector2(i, j));
                }
            }
        }
    }
}