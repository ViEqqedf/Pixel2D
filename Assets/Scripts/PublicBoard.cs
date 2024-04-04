namespace Voxel2D {
    public static class PublicBoard {
        public enum UnitType {
            Empty = 1 << 0,
            Water = 1 << 1,
            Sand = 1 << 2,
        }
    }
}