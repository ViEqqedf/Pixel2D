using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Voxel2D {
    public class ExecuteCommandBufferPass : ScriptableRenderPass {
        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData) {
            if (VoxelRenderer.renderCmd != null) {
                context.ExecuteCommandBuffer(VoxelRenderer.renderCmd);
            }
        }
    }

    public class RenderVoxelFeature : ScriptableRendererFeature {
        public RenderPassEvent renderPassEvent = RenderPassEvent.BeforeRendering;
        private ExecuteCommandBufferPass executeCommandBufferPass;

        public override void Create() {
            executeCommandBufferPass = new ExecuteCommandBufferPass() {
                renderPassEvent = renderPassEvent,
            };
        }

        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData) {
            executeCommandBufferPass.renderPassEvent = renderPassEvent;
            renderer.EnqueuePass(executeCommandBufferPass);
        }
    }
}