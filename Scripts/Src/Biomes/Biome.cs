using UnityEngine;

/* @docs 
### Biome 

--- 

The Biome class acts as an abstract interface for developing custom biomes to work with the Cell class. 


*/
public abstract class Biome : MonoBehaviour
{
    /* @docs 
    # SetBounds(Vector2)

    > Sets the bounds of the Origin. This will almost always be the same as the cell, 
    > though in some cases, such as creating boundaries of empty space, it is necessary to set custom values.

    # SetOrigin(Vector3)

    > Sets the three dimensional origin of the Biome. This will almost always be the same as the cell,
    > though in some cases, such as creating boundaries of empty space, it is necessary to set custom values.

    # SetParent(string parent)

    > Sets the name of the parent Cell for later reference. This is more performant than passing the context. 

    # Render()

    > Executes the render logic of the biome.

    */

    public abstract void SetBounds(Vector2 cellBounds);
    public abstract void SetOrigin(Vector3 origin);
    public abstract void SetParent(string parent);
    public abstract void Render();
    public abstract Material GetTerrainMaterial();
}
