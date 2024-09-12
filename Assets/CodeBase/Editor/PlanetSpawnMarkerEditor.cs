using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;
using CodeBase.Logic.Spawner;

namespace CodeBase.Editor
{
    [CustomEditor(typeof(PlanetSpawnMarker))]
    public class PlanetSpawnMarkerEditor : UnityEditor.Editor
    {
        private PlanetSpawnMarker _marker;

        private void OnEnable()
        {
            _marker = (PlanetSpawnMarker)target;
            UpdateMarkerName();
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUI.changed)
            {
                UpdateMarkerName();
            }
        }

        private void UpdateMarkerName()
        {
            if (!IsPrefab(_marker.gameObject))
            {
                if (_marker.gameObject.name != $"{_marker.TypeId} Planet Marker")
                {
                    _marker.gameObject.name = $"{_marker.TypeId} Planet Marker";
                    EditorSceneManager.MarkSceneDirty(_marker.gameObject.scene);
                }
            }
        }

        private bool IsPrefab(GameObject obj)
        {
            if (PrefabUtility.IsPartOfPrefabAsset(obj)) return true;

            var prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
            return prefabStage != null && prefabStage.IsPartOfPrefabContents(obj);
        }

        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(PlanetSpawnMarker marker, GizmoType gizmo)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(marker.transform.position, 0.5f);
        }
    }
}
