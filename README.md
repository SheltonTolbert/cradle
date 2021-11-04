
  

### Biome 



--- 



The Biome class acts as an abstract interface for developing custom biomes to work with the Cell class. 








  

## SetBounds(Vector2)



> Sets the bounds of the Origin. This will almost always be the same as the cell, 

> though in some cases, such as creating boundaries of empty space, it is necessary to set custom values.



## SetOrigin(Vector3)



> Sets the three dimensional origin of the Biome. This will almost always be the same as the cell,

> though in some cases, such as creating boundaries of empty space, it is necessary to set custom values.



## SetParent(string parent)



> Sets the name of the parent Cell for later reference. This is more performant than passing the context. 



## Render()



> Executes the render logic of the biome.






  





### Cell 



--- 



The Cell class is responsible for generating biomes.



The Cell class contains the following public variables:



```



- Biome defaultBiome

- A fall back biome should the Cell encounter a runtime exception. If left empty a default empty biome will be used. 

- Biome[] possibleBiomes

- An array of all possible biomes. The Cell can choose from. 

- int sideLength

- A default Cell size. This is typically overwritten by the Generator but is useful  for debugging a Cell outside of a parent Generator. 

- bool renderDebug

- This bool acts a toggle for rendering the Cell debug view. 

```








 

## void SetSize(int size, float scale)



> Sets the initial size and scale of the cell 






 

## void SetOrigin(Vector3 origin)



> Sets origin of the cell 






 

## void SetBiomes(Biome[] biomes)



> Sets possible biomes of the cell 






 

## void ClearBiomes(int length = 0)



> Clears possible biomes array 






 

## int GetNumBiomes()



> Returns number of biomes in possible biomes array 






 

## Biome[] GetBiomes()



> Returns the possible biomes array 






 

## Biome GetBiome()



> Returns the active biome 






 

## Vector3 GetBounds()



> Returns the size of the Cell






 

## void SetBiome(int index)



> Sets the active biome by index






 

## void SetBiome(Biome biome)



> Sets the active biome Biome






 

## void RenderBiome()



> Renders the current biome






  

### Generator



---



The Generator class is responsible for generating and managing the cells which make up the game environment. 

The Generator class contains the following public variables: 



```



- Cell cell

- The Cell should be populated with a Cell that is a component of a prefab. This way default biomes can be set by the user.

- Bool regenerate

- This Boolean serves a toggle for regenerating the grid at runtime. This is useful when changing seeds, boundaries and other rendering variables.

- Bool soloCell 

- This Boolean acts as a toggle for only generating one cell, rather than generating the 9 in a standard grid. This is useful for debugging. 

- Float noiseScale

- This variable controls the horizontal (x,z) scale of the perlin noise function.

- Float originX

- The x coordinate of the grid.

- Float originZ

- The z coordinate of the grid.

- Int biomeIndex

- Enables the specification of a biome to render, based on the possible biomes array of the associated cell. Only works in solo cell mode. 

- Int resolution

- A scalar value which controls the resolution of the terrain mesh. 

- int seed

- Global seed value for generating predictable random numbers. Useful for creating reproduceable procedural environments. 



```






  

## public float getPerlinValue(float x, float y)



> Returns a coherent noise value between 0 and 1 based on the origin, seed and noise scale of the generator




 

## public (int,int) GetDimensions()



> returns a tuple with the demensions of the grid




  

##private void InstantiateCell(int index, float x, float z, Color color)



> Instantiates a cell at a point x,z with a terrain color specified by the color argument. Cell is added to the cell array at the specified index



