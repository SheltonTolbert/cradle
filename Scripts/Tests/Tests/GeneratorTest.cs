using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GeneratorTest
{
    GameObject context;
    Assert assertion;
    Generator generator;

    public void SetContext(GameObject parent)
    {
        context = parent;
    }

    public void Run()
    {
        assertion = new Assert("Generator Test");
        generator = (Generator)context.GetComponent(typeof(Generator));

        Debug.Log("Running Generator tests");
        VerifyCellCount();
        VerifyPerlinValue();
        GeneratorInitializedWithCorrectProperties();
    }

    private void VerifyCellCount()
    {
        assertion.expect(context.transform.childCount).toEq(9);
    }

    private void VerifyPerlinValue()
    {
        float x = 0.0f;
        float y = 0.0f;

        float noiseValue = Mathf.Round(generator.GetPerlinValue(x, y) * 100f) / 100f;
        int defaultSeed = 1;

        assertion.expect(generator.seed).toEq(defaultSeed);
        assertion.expect(noiseValue).toEq(0.47f);
    }

    private void GeneratorInitializedWithCorrectProperties()
    {
        (int gridWidth, int gridHeight) = generator.GetDimensions();
        (int expectedWidth, int expectedHeight) = (300, 300);

        assertion.expect(gridWidth).toEq(expectedWidth);
        assertion.expect(gridHeight).toEq(expectedHeight);
    }

}
