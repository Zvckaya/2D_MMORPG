using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.IO;




#if UNITY_EDITOR
using UnityEditor;
#endif

public class MapEditor 
{
#if UNITY_EDITOR

   

    [MenuItem("Tools/GenerateMap %#g")]

    private static void GenerateMap()
    {
        GameObject[] gameObject = Resources.LoadAll<GameObject>("Prefabs/Map");

        foreach (GameObject go in gameObject)
        {
            Tilemap tmBase = Util.FindChild<Tilemap>(go, "Tilemap_Base", true);
            Tilemap _tm = Util.FindChild<Tilemap>(go, "Tilemap_Collision", true);

            using (var writer = File.CreateText($"Assets/Resources/Map/{go.name}.txt"))
            {
                writer.WriteLine(_tm.cellBounds.xMin);
                writer.WriteLine(_tm.cellBounds.xMax);
                writer.WriteLine(_tm.cellBounds.yMin);
                writer.WriteLine(_tm.cellBounds.yMax);

                for (int y = _tm.cellBounds.yMax; y >= _tm.cellBounds.yMin; y--)
                {
                    for (int x = _tm.cellBounds.xMin; x <= _tm.cellBounds.xMax; x++)
                    {
                        TileBase tile = _tm.GetTile(new Vector3Int(x, y, 0));
                        if (tile != null)
                            writer.Write("1");
                        else
                            writer.Write("0");
                    }
                    writer.WriteLine();
                }
            }
        }

  
    }

#endif
}
