using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GeneratorTest
{
    GameObject context;
    Assert assertion;

    public void SetContext(GameObject parent)
    {
        context = parent;
    }

    public void Run()
    {
        assertion = new Assert("Generator Test");
        Debug.Log("Running Generator tests");
        VerifyCellCount();
    }

    private void VerifyCellCount()
    {
        assertion.expect(context.transform.childCount).toEq(9);
    }
}
