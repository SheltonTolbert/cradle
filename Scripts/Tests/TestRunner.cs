using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRunner : MonoBehaviour
{
    public bool runAll = false;
    public bool runGeneratorTest = false;

    private int passedTests = 0;
    private int failedTests = 0;
    private GameObject context;
    GeneratorTest generatorTest = new GeneratorTest();

    public void Pass()
    {
        passedTests += 1;
    }

    public void Fail()
    {
        failedTests += 1;
    }

    private void PrintResults()
    {
        if (failedTests > 0)
        {
            Debug.Log($"{passedTests} out of {passedTests + failedTests} passed. {failedTests} failed.");
        }
        else
        {
            Debug.Log($"All {passedTests + failedTests} tests passed.");
        }

        passedTests = 0;
        failedTests = 0;
    }


    void Update()
    {
        if (runGeneratorTest)
        {
            runGeneratorTest = false;
            generatorTest.Run();
            PrintResults();
        }


    }

    void Start()
    {
        context = GameObject.Find("Generator");
        generatorTest.SetContext(context);


    }
}
