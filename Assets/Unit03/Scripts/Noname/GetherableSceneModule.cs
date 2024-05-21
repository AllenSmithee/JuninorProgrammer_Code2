using System;
using System.Diagnostics;
using System.Linq;
using Cysharp.Threading.Tasks;
using Noname.Extentions;
using UnityEngine.SceneManagement;

namespace Noname.SceneManage
{
    public enum LoadMode
    {
        SINGLE_UnloadAlreadyLoaded = 0,
        SINGLE_DontLoadIfAlreadyLoaded = 1,
        MULTI_StackLoad = 2
    }
    public static class GetherableSceneModule
    {

        public static bool IsLoaded(this GetherableScene scene)
        {
            return SceneManager.GetSceneByPath(scene.ScenePath).isLoaded;
        }

        public static async UniTask AsyncLoadTarget(this GetherableScene scene, LoadParameters? loadParameters = null, IProgress<float> progress = null)
        {
            await UniTask.DelayFrame(10);

            try
            {
                var loadpara = loadParameters ?? LoadParameters.Default;
                if (loadpara.IsValidToLoad(scene))
                {
                    if (scene.IsLoaded())
                    {
                        return;
                    }

                    // UnityEngine.SceneManagement.LoadSceneParameters loadSceneParameters = new UnityEngine.SceneManagement.LoadSceneParameters();
                    // loadSceneParameters.loadSceneMode = loadpara.LoadMode switch
                    // {
                        // LoadMode.SINGLE_UnloadAlreadyLoaded => LoadSceneMode.Single,
                        // LoadMode.SINGLE_DontLoadIfAlreadyLoaded => LoadSceneMode.Single,
                        // LoadMode.MULTI_StackLoad => LoadSceneMode.Additive,
                        // _ => LoadSceneMode.Single
                    // };

                    //todo : need to make a single, additive setting


                    await SceneManager.LoadSceneAsync(scene.ScenePath, LoadSceneMode.Additive);
                    


                }
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogWarning(e);
            }
            finally
            {
                await UniTask.CompletedTask;
            }
        }

        public static async UniTask AsyncUnloadTarget(this GetherableScene scene, UnloadParameters? unloadParameters = null, IProgress<float> progress = null)
        {
            
            try
            {
                var unloadpara = unloadParameters ?? UnloadParameters.Default;

                if (unloadpara.RemoveAll)
                {
                    await SceneManager.UnloadSceneAsync(scene.ScenePath);
                }
                else
                {
                    await SceneManager.UnloadSceneAsync(scene.ScenePath, unloadpara.UnloadSceneOptions);
                }
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogWarning(e);
            }
            finally
            {
                await UniTask.CompletedTask;
            }
        }


    }


    public struct LoadParameters
    {
        public static LoadParameters Default => new LoadParameters(LoadMode.SINGLE_UnloadAlreadyLoaded, false);

        private LoadMode m_loadSceneMode;
        bool m_delayLoad;

        public LoadParameters(LoadMode loadSceneMode, bool m_delayLoad)
        {
            this.m_loadSceneMode = loadSceneMode;
            this.m_delayLoad = m_delayLoad;
        }

        public LoadMode LoadMode => m_loadSceneMode;

        public bool MultiMode
        {
            get
            {
                return m_loadSceneMode switch
                {
                    LoadMode.MULTI_StackLoad => true,
                    _ => false
                };
            }
        }

        public bool IsValidToLoad(GetherableScene scene)
        {
            return m_loadSceneMode switch
            {
                LoadMode.SINGLE_UnloadAlreadyLoaded => true,
                LoadMode.SINGLE_DontLoadIfAlreadyLoaded => !SceneManager.GetSceneByPath(scene.ScenePath).isLoaded,
                LoadMode.MULTI_StackLoad => true,
                _ => false
            };
        }
    }

    public struct UnloadParameters
    {
        public static UnloadParameters Default => new UnloadParameters(UnloadSceneOptions.None, false);
        UnloadSceneOptions m_unloadSceneOptions;
        bool m_removeAll;

        public UnloadSceneOptions UnloadSceneOptions => m_unloadSceneOptions;
        public bool RemoveAll => m_removeAll;

        public UnloadParameters(UnloadSceneOptions unloadSceneOptions, bool removeAll)
        {
            m_unloadSceneOptions = unloadSceneOptions;
            m_removeAll = removeAll;
        }

    }



}