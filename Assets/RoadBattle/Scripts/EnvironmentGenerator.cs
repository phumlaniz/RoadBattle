using System.Xml.Serialization;
using UnityEngine;

namespace RoadBattle
{
    public class EnvironmentGenerator : MonoBehaviour
    {
        [Header("Inputs")]
        [SerializeField]
        private Transform TreeParent;

        [Header("Prefabs")]
        [SerializeField]
        private GameObject[] treePrefabs;
        [SerializeField]
        private GameObject[] shrubPrefabs;

        [Header("Settings")]
        [SerializeField]
        private BoundArea LeftBoundArea;
        [SerializeField]
        private BoundArea RightBoundArea;
        [SerializeField]
        private float SprountFrequency = 0.3f;
        [SerializeField]
        private int pixWidth;
        [SerializeField]
        private int pixHeight;
        [SerializeField]
        private float xOrg;
        [SerializeField]
        private float yOrg;
        [SerializeField]
        private float xScale = 1f;
        [SerializeField]
        private float yScale = 1f;

        private Texture2D noiseTexture;

        void Start()
        {
            noiseTexture = new Texture2D(pixWidth, pixHeight);
            for (float y = 0f; y < noiseTexture.height; y++)
            {
                for (float x = 0f; x < noiseTexture.width; x++)
                {
                    float xCoord = xOrg + x / noiseTexture.width * xScale;
                    float yCoord = yOrg + y / noiseTexture.height * yScale;
                    float sample = Mathf.PerlinNoise(xCoord, yCoord);
                    if (sample >= SprountFrequency)
                    {
                        GameObject newTreeSpawn = Instantiate(treePrefabs[0], TreeParent);/*
                        float xpos = Mathf.Lerp(LeftBoundArea.MinBound.position.x, LeftBoundArea.MaxBound.position.x, xCoord);
                        float ypos = Mathf.Lerp(LeftBoundArea.MinBound.position.y, LeftBoundArea.MaxBound.position.y, yCoord);*/
                        newTreeSpawn.transform.position = new Vector3(xCoord, 0, yCoord);
                    }
                }
            }
        }
    }
}