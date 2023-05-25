using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class WaveSpawner : MonoBehaviour
{
    public float timeBwnWaves;
    public float startTime;
    public int spawnIncrement = 5;

    public GameObject[] police;

    public Vector3[] spawnLocations;
    public bool spawnWaves = true;
    public int nightSearchInterval = 5;

    public Light sunLight;
    public Light spotLight;

    public AudioSource chopper;

    int waveNum = 1;
    float waveTimer;

    Vector3 SpotLightStart = new Vector3(-10,0,-8);

    // Start is called before the first frame update
    void Start()
    {
        waveTimer = startTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(spawnWaves){
            if(waveTimer < 0 && waveNum % nightSearchInterval > 0){
                StartCoroutine(SpawnWave());
                waveTimer = timeBwnWaves;
                waveNum ++;
            }
            else if(waveTimer < 0){
                StartNightSearch();
                waveTimer = timeBwnWaves;
                waveNum ++;
            }
            else{
                waveTimer -= Time.deltaTime;
            }
        }
        
    }

    void StartNightSearch(){
        StartCoroutine(SunController());
        StartCoroutine(SpotLightTracker());
        chopper.Play();
    }

    IEnumerator SpawnWave(){
        int spawnCount = waveNum * spawnIncrement;
        Vector3 spawnPoint = spawnLocations[Random.Range(0,spawnLocations.Length)];
        for (int i = 0; i < spawnCount; i++)
        {
            Instantiate(police[Random.Range(0,police.Length)], spawnPoint, Quaternion.identity, transform);
            yield return new WaitForSeconds(0.25f);
        }
    }

    IEnumerator SunController(){
        //Takes 4.5 seconds to transition
        for (int i = 0; i <= 90; i++)
        {
            sunLight.transform.rotation = Quaternion.Euler(Mathf.Lerp(0,90, i/90f),0,0);
            yield return new WaitForSeconds(0.05f); 
        }

        yield return new WaitForSeconds(10f);

        for (int i = 0; i <= 90; i++)
        {
            sunLight.transform.rotation = Quaternion.Euler(Mathf.Lerp(90,0, i/90f),0,0);
            yield return new WaitForSeconds(0.05f); 
        }
    }

    IEnumerator SpotLightTracker(){
        spotLight.enabled = true;
        
        Vector3[] waypoints = new Vector3[6];
        for (int i = 0; i < 6; i++)
        {
            waypoints[i] = new Vector3(Random.Range(-8f,8f),Random.Range(-5f,5f),-8);
        }

        for(int i = 0; i <= 225; i++){
            spotLight.transform.position = Vector3.Lerp(SpotLightStart,waypoints[0],i/225f);
            yield return new WaitForSeconds(0.02f);
        }

        for(int i = 0; i < waypoints.Length-1; i++){
            for (int j = 1; j <= 100; j++)
            {
                spotLight.transform.position = Vector3.Lerp(waypoints[i], waypoints[i+1], j/100f);
                yield return new WaitForSeconds(0.02f);
            }
        }

        for(int i = 0; i <= 225; i++){
            spotLight.transform.position = Vector3.Lerp(waypoints[5], new Vector3(10,0,-8), i/225f);
            yield return new WaitForSeconds(0.02f);
        }
        spotLight.transform.position = SpotLightStart;
        spotLight.enabled = false;
    }
}
