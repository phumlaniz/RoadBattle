using System.Xml.Serialization;
using Unity.VisualScripting;
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
            SproutVegetation(LeftBoundArea, treePrefabs);
            SproutVegetation(RightBoundArea, treePrefabs);
            SproutVegetation(LeftBoundArea, shrubPrefabs);
            SproutVegetation(RightBoundArea, shrubPrefabs);
        }

        private void SproutVegetation(BoundArea BoundArea, GameObject[] prefabs)
        {
            noiseTexture = new Texture2D(pixWidth, pixHeight);
            int randomNum = 0;
            for (float y = 0f; y < pixHeight; y++)
            {
                for (float x = 0f; x < pixWidth; x++)
                {
                    randomNum = Random.Range(0, 100);
                    bool shouldSproutTree = randomNum >= 50;
                    float xCoord = x / pixWidth;
                    float zCoord = y / pixHeight;
                    if (shouldSproutTree)
                    {
                        randomNum = Random.Range(0, prefabs.Length);
                        GameObject newTreeSpawn = Instantiate(prefabs[randomNum], TreeParent);
                        newTreeSpawn.isStatic = true;
                        float xPos = Mathf.Lerp(BoundArea.MinBound.position.x, BoundArea.MaxBound.position.x, xCoord);
                        float zPos = Mathf.Lerp(BoundArea.MinBound.position.z, BoundArea.MaxBound.position.z, zCoord);
                        newTreeSpawn.transform.position = new Vector3(xPos, 0, zPos);
                        newTreeSpawn.transform.eulerAngles = new Vector3(0, Random.Range(0, 360), 0);
                    }
                }
            }
        }
    }
}