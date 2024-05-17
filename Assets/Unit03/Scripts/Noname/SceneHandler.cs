using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Noname.SceneManage
{
    using Cysharp.Threading.Tasks;
    using Noname.Extentions;
    using UnityEditor;

    [CreateAssetMenu(fileName = "SceneHandler", menuName = "Noname/Scene Handler")]
    public class SceneHandler : ScriptableObject
    {
        [Space, Header("Scene Setup")]
        [SerializeField] GetherableScene m_targetScene;

        public GetherableScene TargetScene => m_targetScene;


        public void UpdateTargetScene(SceneAsset sceneAsset)
        {
            m_targetScene = sceneAsset.ToGetherableScene();
            
        }        
        
                                                      
    }
}
