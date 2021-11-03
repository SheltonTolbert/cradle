using UnityEngine;
public class Assert
{
    private string context;

    public Assert(string currentContext)
    {
        context = currentContext;
    }

    public Resolver expect(bool comparitor)
    {
        return new Resolver(comparitor, context);
    }

    public Resolver expect(string comparitor)
    {
        return new Resolver(comparitor, context);
    }

    public Resolver expect(int comparitor)
    {
        return new Resolver(comparitor, context);
    }

    public Resolver expect(float comparitor)
    {
        return new Resolver(comparitor, context);
    }

    public Resolver expect(Vector3 comparitor)
    {
        return new Resolver(comparitor, context);
    }
}
