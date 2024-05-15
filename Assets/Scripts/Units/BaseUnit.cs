using System;
using UnityEngine;

namespace Voxel2D {
    public enum UnitType {
        Empty = 1 << 0,
        Water = 1 << 1,
        Sand = 1 << 2,
    }

    public class BaseUnit {
        public UnitType unitType;
        public byte updateMask = Byte.MaxValue;
        public Vector2 pos;
        public Color color = Color.black;
        protected bool isInit;

        public BaseUnit(UnitType unitType) {
            this.unitType = unitType;
        }

        public void Init(Vector2 createPos) {
            this.pos = createPos;
            isInit = true;
        }

        public virtual void UnitUpdate(int simulationCount) {
            updateMask = (byte)(simulationCount&(long)1);
        }
    }
}