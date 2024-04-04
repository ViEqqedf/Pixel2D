using System;
using UnityEngine;

namespace Voxel2D {
    public class BaseUnit {
        public UnitType unitType;
        public byte updateMask = Byte.MaxValue;
        public Vector2 pos;
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