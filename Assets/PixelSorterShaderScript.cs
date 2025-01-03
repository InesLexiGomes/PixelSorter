using UnityEngine;

public class PixelSorterShaderScript : MonoBehaviour
{
    [SerializeField] private ComputeShader pixelSorter;
    
    private RenderTexture renderTexture;
    private RenderTexture cameraDepth;
    private RenderTexture mask;

    // The mask will be contained between these two parameters
    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float depthThresholdLow, depthThresholdHigh;

    private void Awake()
    {
        Application.targetFrameRate = 30;
    }

    private void Start()
    {
        ResetTex();
    }

    private void FixedUpdate()
    {
        ResetTex();
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        pixelSorter.SetFloat("DepthThresholdLow", depthThresholdLow);
        pixelSorter.SetFloat("DepthThresholdHigh", depthThresholdHigh);

        pixelSorter.SetInt("Width", Screen.width);
        pixelSorter.SetInt("Height", Screen.height);

        Graphics.Blit(Shader.GetGlobalTexture("_CameraDepthTexture"), cameraDepth);
        pixelSorter.SetTexture(pixelSorter.FindKernel("CSMask"), "CameraDepth", cameraDepth);
        pixelSorter.SetTexture(pixelSorter.FindKernel("CSSort"), "CameraDepth", cameraDepth);
        pixelSorter.SetTexture(pixelSorter.FindKernel("CSMask"), "Mask", mask);
        pixelSorter.SetTexture(pixelSorter.FindKernel("CSSort"), "Mask", mask);
        pixelSorter.SetTexture(pixelSorter.FindKernel("CSSort"), "Result", renderTexture);

        pixelSorter.Dispatch(pixelSorter.FindKernel("CSMask"), renderTexture.width / 16, renderTexture.height / 16, 1);

        Graphics.Blit(renderTexture, destination);
    }

    private void ResetTex()
    {
        renderTexture = new RenderTexture(Screen.width, Screen.height, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear);
        renderTexture.enableRandomWrite = true;
        renderTexture.Create();

        cameraDepth = new RenderTexture(Screen.width, Screen.height, 0, RenderTextureFormat.R8, RenderTextureReadWrite.Linear);
        cameraDepth.enableRandomWrite = true;
        cameraDepth.Create();

        mask = new RenderTexture(Screen.width, Screen.height, 0, RenderTextureFormat.R8, RenderTextureReadWrite.Linear);
        mask.enableRandomWrite = true;
        mask.Create();
    }
}