using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.Experimental.VFX;
using UnityEditor;

[ExecuteInEditMode]
public class WeatherManager : MonoBehaviour
{
    [SerializeField]
    private GameObject ocean;
    [SerializeField]
    private GameObject snowVFX;
    [SerializeField]
    private GameObject rainVFX;

    [Range(-2.05f, -1)]
    public float seaLevel; 

    [SerializeField]
    public Material[] materialCollection;
    [Range(0, 1)]
    private float[] materialFrostResisitance;

    [Range(0, 1)]
    public float fogIntensity;

    [Range(0, 1)]
    public float rainIntensity;

    [Range(0,1)]
    public float temperature;

    [SerializeField]
    public float frostResistanceOcean;



    // Update is called once per frame
    public void OnValidate()
    {
        ocean.transform.position = new Vector3(ocean.transform.position.x, seaLevel, ocean.transform.position.z);
        for (int i = 0; i < materialCollection.Length; i++)
        {
            materialCollection[i].SetFloat("Temperature", temperature);
        }

        for (int i = 0; i < materialCollection.Length; i++)
        {
            materialCollection[i].SetFloat("RainIntensity", rainIntensity);
        }
        ocean.GetComponent<Renderer>().sharedMaterial.SetFloat("RainIntensity", rainIntensity);
        if (temperature > 0.5f)
        {
            rainVFX.GetComponent<VisualEffect>().SetFloat("RainIntensity", rainIntensity * temperature);
            snowVFX.GetComponent<VisualEffect>().SetFloat("RainIntensity", 0);
        }
        else
        {
            snowVFX.GetComponent<VisualEffect>().SetFloat("RainIntensity", rainIntensity * (1 - temperature));
            rainVFX.GetComponent<VisualEffect>().SetFloat("RainIntensity", 0);
        }




    }

    /*public void OnInspectorGUI()
    {
        GUILayout.BeginVertical();
        scrollPos = GUILayout.BeginScrollView(scrollPos, false, true);

        EditorGUILayout.Slider(temperature, 0, 1);


        GUILayout.EndVertical();
        GUILayout.EndScrollView();
    }*/
}
