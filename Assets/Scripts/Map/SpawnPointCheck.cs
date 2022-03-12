using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointCheck : MonoBehaviour
{
    GeneratorEngine generatorEngine;

    void Awake()
    {
        generatorEngine = GameObject.FindGameObjectWithTag("LevelGenerator").GetComponent<GeneratorEngine>();
        generatorEngine.RegisterSpawnPoint(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        generatorEngine.DeRegisterSpawnPoint(gameObject);
        Destroy(gameObject);
    }

}
