using CodeBase.Data;
using CodeBase.Logic.Spawner;
using CodeBase.SO;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Editor
{
    [CustomEditor(typeof(LevelStaticData))]
    public class LevelStaticDataEditor : UnityEditor.Editor
    {
        private const string InitialShipPointTag = "InitialShipPointTag";

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            LevelStaticData levelData = (LevelStaticData)target;

            if (GUILayout.Button("Collect"))
            {
                PlanetSpawnMarker[] spawnMarkers = FindObjectsOfType<PlanetSpawnMarker>();

                List<PlanetData> planets = spawnMarkers.Select(x => new PlanetData(x.TypeId, x.transform.position)).ToList();
                levelData.Planets = planets;

                levelData.LevelKey = SceneManager.GetActiveScene().name;

                levelData.InitialPlayerShipPosition = GameObject.FindWithTag(InitialShipPointTag).transform.position;
            }

            EditorUtility.SetDirty(target);
        }
    }
}