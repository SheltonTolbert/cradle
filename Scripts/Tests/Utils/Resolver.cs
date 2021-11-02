using UnityEngine;

public class Resolver
{
    private int intVal;
    private float floatVal;
    private string stringVal;
    private bool boolVal;
    private string context;
    private TestRunner testRunner = GameObject.Find("TestRunner").GetComponent<TestRunner>();

    private enum MODES
    {
        BOOLEAN,
        FLOAT,
        INTEGER,
        STRING,
        NULL
    }

    private Resolver.MODES mode = MODES.NULL;


    public Resolver(bool Bool, string currentContext)
    {
        context = currentContext;
        mode = MODES.BOOLEAN;
        boolVal = Bool;
    }


    public Resolver(int Int, string currentContext)
    {
        context = currentContext;
        mode = MODES.INTEGER;
        intVal = Int;
    }


    public Resolver(float Float, string currentContext)
    {
        context = currentContext;
        mode = MODES.FLOAT;
        floatVal = Float;
    }

    public Resolver(string String, string currentContext)
    {
        context = currentContext;
        mode = MODES.STRING;
        stringVal = String;
    }


    private void Reset()
    {
        mode = MODES.NULL;
    }

    public void toEq(bool _comparitor)
    {
        if (boolVal == _comparitor)
        {
            testRunner.Pass();
        }
        else if (mode != MODES.BOOLEAN)
        {
            Debug.LogError($"{context} FAILED:Expected {mode}, got boolean");
            testRunner.Fail();
        }
        else if (boolVal != _comparitor)
        {
            Debug.LogError($"{context} FAILED:Expected {boolVal} to equal {_comparitor}");
            testRunner.Fail();
        }
        Reset();
    }

    public void toEq(string _comparitor)
    {
        if (stringVal == _comparitor)
        {
            testRunner.Pass();
        }
        else if (mode != MODES.STRING)
        {
            Debug.LogError($"{context} FAILED:Expected {mode}, got string");
            testRunner.Fail();
        }
        else if (stringVal != _comparitor)
        {
            Debug.LogError($"{context} FAILED:Expected {stringVal} to equal {_comparitor}");
            testRunner.Fail();
        }
        Reset();
    }

    public void toEq(int _comparitor)
    {
        if (intVal == _comparitor)
        {
            testRunner.Pass();
        }
        else if (mode != MODES.INTEGER)
        {
            Debug.LogError($"{context} FAILED:Expected {mode}, got int");
            testRunner.Fail();
        }
        else if (intVal != _comparitor)
        {
            Debug.LogError($"{context} FAILED:Expected {intVal} to equal {_comparitor}");
            testRunner.Fail();
        }
        Reset();
    }

    public void toEq(float _comparitor)
    {
        if (floatVal == _comparitor)
        {
            testRunner.Pass();
        }
        else if (mode != MODES.FLOAT)
        {
            Debug.LogError($"{context} FAILED:Expected {mode}, got float");
            testRunner.Fail();
        }
        else if (floatVal != _comparitor)
        {
            Debug.LogError($"{context} FAILED:Expected {floatVal} to equal {_comparitor}");
            testRunner.Fail();
        }
        Reset();
    }



    public void toNotEq(bool _comparitor)
    {
        if (boolVal != _comparitor)
        {
            testRunner.Pass();
        }
        else if (mode != MODES.BOOLEAN)
        {
            Debug.LogError($"{context} FAILED:Expected {mode}, got boolean");
            testRunner.Fail();
        }
        else if (boolVal == _comparitor)
        {
            Debug.LogError($"{context} FAILED:Expected {boolVal} to equal {_comparitor}");
            testRunner.Fail();
        }
        Reset();
    }

    public void toNotEq(string _comparitor)
    {
        if (stringVal != _comparitor)
        {
            testRunner.Pass();
        }
        else if (mode != MODES.STRING)
        {
            Debug.LogError($"{context} FAILED:Expected {mode}, got string");
            testRunner.Fail();
        }
        else if (stringVal == _comparitor)
        {
            Debug.LogError($"{context} FAILED:Expected {stringVal} to equal {_comparitor}");
            testRunner.Fail();
        }
        Reset();
    }

    public void toNotEq(int _comparitor)
    {
        if (intVal != _comparitor)
        {
            testRunner.Pass();
        }
        else if (mode != MODES.INTEGER)
        {
            Debug.LogError($"{context} FAILED:Expected {mode}, got int");
            testRunner.Fail();
        }
        else if (intVal == _comparitor)
        {
            Debug.LogError($"{context} FAILED:Expected {intVal} to equal {_comparitor}");
            testRunner.Fail();
        }
        Reset();
    }

    public void toNotEq(float _comparitor)
    {
        if (floatVal != _comparitor)
        {
            testRunner.Pass();
        }
        else if (mode != MODES.FLOAT)
        {
            Debug.LogError($"{context} FAILED:Expected {mode}, got float");
            testRunner.Fail();
        }
        else if (floatVal == _comparitor)
        {
            Debug.LogError($"{context} FAILED:Expected {floatVal} to equal {_comparitor}");
            testRunner.Fail();
        }
        Reset();
    }

}
