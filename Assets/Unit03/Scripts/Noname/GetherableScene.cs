using System;
using UnityEngine;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Noname.SceneManage
{
    [System.Serializable]
    public struct GetherableScene : IDisposable, IEquatable<GetherableScene>
    {
        public const int INVALID_BUILD_INDEX = -1;
        public static GetherableScene Empty => new() { m_sceneName = "", m_guid = "" };

        [SerializeField] string m_sceneName;
        [SerializeField] string m_guid;

#if UNITY_EDITOR
        [SerializeField] string m_scenePath;
        [SerializeField] SceneAsset m_sceneAsset;
#endif

        public string SceneName => m_sceneName;
        public string Guid => m_guid;

#if UNITY_EDITOR
        public string ScenePath => m_scenePath;
        public SceneAsset SceneAsset => m_sceneAsset;
#endif

        public bool IsVaild
        {
            get
            {
                bool ret = true;
#if UNITY_EDITOR
                if (m_sceneAsset == null)
                {
                    ret = false;
                }
                else
#endif
                if (string.IsNullOrWhiteSpace(m_sceneName))
                {
                    ret = false;
                }
                else
                if (string.IsNullOrWhiteSpace(m_guid))
                {
                    ret = false;
                }
                return ret;
            }
        }




        public void Dispose()
        {
            m_sceneName = null;
            m_guid = null;
#if UNITY_EDITOR
            m_scenePath = null;
            m_sceneAsset = null;
#endif
        }

        public readonly bool Equals(GetherableScene other)
        {
            return this.m_guid.Equals(other.m_guid);
        }
#if UNITY_EDITOR
        public GetherableScene(string sceneName, string guid, string scenePath, SceneAsset sceneAsset)
        {
            m_sceneName = sceneName;
            m_guid = guid;
            m_scenePath = scenePath;
            m_sceneAsset = sceneAsset;
        }
#endif
    }
}
namespace Noname.Extentions
{
    using Noname.SceneManage;
#if UNITY_EDITOR

    public static class GetherableSceneExtensions
    {

        public static GetherableScene ToGetherableScene(this SceneAsset sceneAsset)
        {
            string sceneAssetPath = AssetDatabase.GetAssetPath(sceneAsset);
            string sceneName = sceneAssetPath.Split('/', '\\').Last().Split('.')[0];
            string guid = AssetDatabase.AssetPathToGUID(sceneAssetPath);
            return new GetherableScene(sceneName, guid, sceneAssetPath, sceneAsset);
        }

        public static GetherableScene ToGetherableScene(this UnityEngine.SceneManagement.Scene scene)
        {
            if (scene.IsValid())
            {
                var sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(scene.path);
                if (sceneAsset != null)
                {
                    return sceneAsset.ToGetherableScene();
                }
            }
            return GetherableScene.Empty;
        }
    }
#endif
}