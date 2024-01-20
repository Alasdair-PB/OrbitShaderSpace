/*
Depreciated

using UnityEngine; 
using UnityEditor.ShaderGraph;
using System.Reflection; 

public class CustomNodesC : CodeFunctionNode
{
    public CustomNodesC()
    {
        name = "Vector Lerp";
    }

    protected override MethodInfo GetFunctionToConvert()
    {
        return GetType().GetMethod("CustomNodesC", BindingFlags.Static | BindingFlags.NonPublic);
    }


    static string CustomNodesC (
        [Slot(0, Binding.None)] Vector4 a, 
        [Slot(1, Binding.None)] Vector4 b,
        [Slot(2, Binding.None)] float alpha,
        [Slot(3, Binding.None)] out Vector4 Out)
    {
        return @"{
            Out = lerp(a, b, alpha);
        }";
    }
}*/
