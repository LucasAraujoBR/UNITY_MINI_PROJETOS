using UnityEngine;

public class HeightmapDeformation : MonoBehaviour
{
    public Texture2D heightMap; // O *heightmap* a ser aplicado
    public float heightScale = 10f; // Fator de escala para a altura

    void Start()
    {
        ApplyHeightmap();
    }

    void ApplyHeightmap()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
        Color[] pixels = heightMap.GetPixels();

        int width = heightMap.width;
        int height = heightMap.height;

        for (int i = 0; i < vertices.Length; i++)
        {
            // Converte o índice do vértice para coordenadas no heightmap
            int x = Mathf.FloorToInt(i % width);
            int y = Mathf.FloorToInt(i / width);

            // Obtém o valor de altura do pixel
            float heightValue = pixels[y * width + x].grayscale;

            // Deforma o vértice com o valor de altura
            vertices[i] += new Vector3(0, heightValue * heightScale, 0);
        }

        // Aplique as novas posições de vértices e recalcule as normais
        mesh.vertices = vertices;
        mesh.RecalculateNormals();
    }
}
