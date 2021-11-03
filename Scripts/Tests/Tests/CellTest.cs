using UnityEngine;

public class CellTest
{
    private Cell cell;
    public Biome[] biomes;
    Assert assert;
    public void Run()
    {
        Debug.Log("Running Cell Tests");
        cell = (Cell)GameObject.Find("Cell(0,0)").GetComponent(typeof(Cell));
        assert = new Assert("Cell Test");

        VerifyDefaultBiome();
        ChangeBiome();
        ValidateCellSize();
    }

    private void VerifyDefaultBiome()
    {
        assert.expect(cell.defaultBiome.name).toEq("PlainsBiome");
    }

    private void ChangeBiome()
    {
        Biome[] biomes = cell.GetBiomes();

        cell.SetBiome(biomes[1]);
        assert.expect(cell.GetBiome().name).toEq("CityBiome");

        cell.SetBiome(biomes[0]);
        assert.expect(cell.GetBiome().name).toEq("PlainsBiome");
    }

    private void ValidateCellSize()
    {
        Vector3 expectedDimensions;

        expectedDimensions = new Vector3(1000.0f, 0.0f, 1000.0f);
        assert.expect(cell.GetBounds()).toEq(expectedDimensions);

        cell.SetSize(100, 1.0f);
        cell.GenerateMesh();

        expectedDimensions = new Vector3(100.0f, 0.0f, 100.0f);
        assert.expect(cell.GetBounds()).toEq(expectedDimensions);

        cell.SetSize(100, 10.0f);
        cell.GenerateMesh();
    }
}
