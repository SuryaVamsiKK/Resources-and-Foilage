using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Randomizer : MonoBehaviour
{
    [HideInInspector] public int seed;
    [HideInInspector] public int noOfNumbers;

    public void GenerateSingleNumber(int seed)
    {
        Random.InitState(seed);
        PrintRandom();
    }
    public void GenerateSingleNumber()
    {
        Random.InitState(seed);
        PrintRandom();
    }
    public void GenerateNNumbers(int seed, int noOfNumbers)
    {
        Random.InitState(seed);

        for (int i = 0; i < noOfNumbers; i++)
        {
            PrintRandom();
        }
    }
    public void GenerateNNumbers()
    {
        Random.InitState(seed);

        for (int i = 0; i < noOfNumbers; i++)
        {
            PrintRandom();
        }
    }

    void PrintRandom()
    {
        Debug.Log(Random.Range(1f, 100f));
    }
}
