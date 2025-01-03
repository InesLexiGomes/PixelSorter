# Pixel Sorter

## Introduction

Heavily inspired by the video titled "I Tried Sorting Pixels" by Acerola in this project I decided to make a glitch effect by sorting the pixels in the camera render.

## Process

### Starting out

First I started by getting myself acquainted with compute shaders with help from the video by Game Dev Guide linked in the references.

### Mask

To apply the mask I didn't want to use luminance like in the original inspiration so instead I wanted to use the distance to the camera.

I remembered learning in class that this distance was saved in a texture so I used Shader.GetGlobalTexture() to get it in the c# script and then sent it over to the compute shader.

There are also parameters to pick from which to which distance the shader should affect.

### What was used to sort the pixels

I used the luminance formula to make a method that calculates the luminance of any given pixel.

### Sorting Pixels

#### Comparing the Pixels

The first method I made would take in 2 pixels and then would compare their luminance to determine which one should come first, however, if at least one of the pixels was masked, they would remain in the same position.

#### Swapping the pixels

This method starts with an if that takes in the method for comparing the pixels and then creates a temporary copy of the first pixel so it can be swapped second B without them ending up with the same value.

#### Actually sorting the pixels

For this last method I had to check Quinston Pimenta's explanation of the bitonic merge sorter.

The first loop as he calls it the stage where we first define that the current bitonic sequence length is 2, since it is the smallest it can be and for each loop it is multiplied by 2.

The second loop is called the pass where we divide the current bitonic sequence length by 2 to get the comparison distance. For each pass we divide it by 2 until it is 1, so for the first stage we would only have one pass but would increase the amount of passes by the stage number.

The last loop is to compare each element present and swap with the pixel whose id is equal to the comparison distance added to the id of the first pixel.

## Conclusion

This shader is more of a fun visual that can be used to show a glitching effect and probably isn't performance ready to be used in a full video game.

## References

### Sorting references

[I Tried Sorting Pixels by Acerola](https://www.youtube.com/watch?v=HMmmBDRy-jE)

[Pixel-Sorting by GarettGunnell](https://github.com/GarrettGunnell/Pixel-Sorting)

[Implementing Bitonic Merge Sort in Vulkan Compute by tgfrerer](https://poniesandlight.co.uk/reflect/bitonic_merge_sort/)

[Wikipedia Bitonic sorter](https://en.wikipedia.org/wiki/Bitonic_sorter#)

[Bitonic (Merge) Sort | Explanation and Code Tutorial by Quinston Pimenta](https://quinstonpimenta.medium.com/bitonic-merge-sort-explanation-and-code-tutorial-5688bd3507fb)

[Bitonic Merge Sort | Explanation and Code Tutorial | Everything you need to know! by Quinston Pimenta](https://www.youtube.com/watch?v=w544Rn4KC8I)

### Shader references

[Getting Started with Compute Shaders in Unity by Game Dev Guide](https://www.youtube.com/watch?app=desktop&v=BrZ4pWwkpto&t=725s)

[Unity Documentation MonoBehaviour.OnRenderImage](https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnRenderImage.html)

[Compute shader read texture value Question by RZGames_Jethro](https://discussions.unity.com/t/compute-shader-read-texture-value/809142)

[Accessing Builtin textures from a compute shader Question by Arrqh](https://discussions.unity.com/t/accessing-builtin-textures-from-a-compute-shader/621513)

[OnRenderImage -> Graphics.Blit in URP Question by Ikaro881](https://discussions.unity.com/t/onrenderimage-graphics-blit-in-urp/849360)

[GPU What are the proper thread dimensions for a compute shader with a very large work load? Question by Ducky](https://stackoverflow.com/questions/72214160/gpu-what-are-the-proper-thread-dimensions-for-a-compute-shader-with-a-very-large)

[HLSL Documentation AllMemoryBarrierWithGroupSync function](https://learn.microsoft.com/en-us/windows/win32/direct3dhlsl/allmemorybarrierwithgroupsync)

[GPU Ray tracing: "kernel at index(0) is invalid" Question by George111111111](https://discussions.unity.com/t/gpu-ray-tracing-kernel-at-index-0-is-invalid/247022)

### Other References

[Wikipedia Relative luminance](https://en.wikipedia.org/wiki/Relative_luminance)
