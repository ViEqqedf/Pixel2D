using System;
using UnityEngine;

namespace Voxel2D {
    [CreateAssetMenu(menuName = "Voxel2D/Shader Resources")]
    public sealed class ShaderResourcesAsset : ScriptableObject {
        [Serializable]
        public class Shaders {
            public Shader procedural;

            public Shaders Clone() {
                return (Shaders)MemberwiseClone();
            }
        }

        [Serializable]
        public class PropertyNames {
            public string tex;
            public string vertices;
            public string baseVertex;

            public PropertyNames Clone() {
                return (PropertyNames)MemberwiseClone();
            }
        }

        public Shaders shaders;
        public PropertyNames propertyNames;
    }
}