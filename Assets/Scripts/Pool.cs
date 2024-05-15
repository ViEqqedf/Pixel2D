using UnityEngine;

namespace Voxel2D {
    public class Pool {
        public BaseUnit [,] Container { get; }

        public Pool(int poolSize) {
            Container = new BaseUnit[poolSize, poolSize];

            VoxelWorld manager = VoxelWorld.Instance;
            for (int i = 0, count1 = poolSize; i < count1; i++) {
                for (int j = 0, count2 = poolSize; j < count2; j++) {
                    Container[i, j] = new Empty();
                    Container[i, j].Init(new Vector2(i, j));
                }
            }
        }
    }
}