using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameWeather : MonoBehaviour
{

    [SerializeField] WeatherManager weatherManager;
    private bool accending, tempAccending;
 

    [Range(1, 100)] [SerializeField] 
    private float waterLevelChangeFrequency, temperatureChangeFrequency;

    [SerializeField] private float waterLevelChangeSpeed, temperatureChangeSpeed;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("TideChange");
        StartCoroutine("TemperatureChange");

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (weatherManager.seaLevel < -2.05 || weatherManager.seaLevel > -1)
        {
            StopCoroutine("TideChange");
            StartCoroutine("TideChange");
        }

        if (weatherManager.temperature < 0.01 || weatherManager.temperature > 0.99f)
        {
            StopCoroutine("TemperatureChange");
            StartCoroutine("TemperatureChange");
        }

        if (accending)
            weatherManager.seaLevel += Time.deltaTime * waterLevelChangeSpeed;
        else
            weatherManager.seaLevel -= Time.deltaTime * waterLevelChangeSpeed;

        if (tempAccending)
            weatherManager.temperature += Time.deltaTime * temperatureChangeSpeed;
        else
            weatherManager.temperature -= Time.deltaTime * temperatureChangeSpeed;

        weatherManager.OnValidate();
    }

    IEnumerator TideChange()
    {
        if (weatherManager.seaLevel < -2.05)
            accending = true;
        else if (weatherManager.seaLevel > -1)
            accending = false;
        else 
            accending = (Random.value > 0.5f);
        yield return new WaitForSeconds(Random.Range(1, waterLevelChangeFrequency));
        StartCoroutine("TideChange");
    }

    IEnumerator TemperatureChange()
    {
        if (weatherManager.temperature < 0.01)
            tempAccending = true;
        else if (weatherManager.temperature > 0.99f)
            tempAccending = false;
        else
            tempAccending = (Random.value > 0.5f);
        yield return new WaitForSeconds(Random.Range(1, temperatureChangeFrequency));
        StartCoroutine("TemperatureChange");
    }
}
