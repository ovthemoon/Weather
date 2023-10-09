using LaserAssetPackage.Scripts.Laser.Exceptions;
using UnityEngine;
using Object = UnityEngine.Object;

namespace LaserAssetPackage.Scripts.Laser.ResourceLoading
{
    /// <summary>
    /// Utility class to get a component from an object by either
    /// 1. seeing if the component has been dragged in from the editor
    /// 2. trying to load the prefab from resources and get the component from there.
    ///
    /// It also throws the appropriate exception if the component could not be located in any way.
    /// </summary>
    public class LaserComponentLoader<T> where T : Component
    {
        private readonly T _assetFromEditor;
        private readonly string _prefabPath;
        private readonly Transform _parent;

        private LaserComponentLoader(T assetFromEditor, string prefabPath, Transform parent)
        {
            _assetFromEditor = assetFromEditor;
            _prefabPath = prefabPath;
            _parent = parent;
        }

        /// <summary>
        /// Tries to find a given GameObject with a component of given type on it and if found returns the component, if not,
        /// instantiates the component from resources and returns the given component from it.
        ///
        /// If no component found even after the instantiation from resource loading it throws an exception.
        /// This means that the component was not set up in Editor nor was it present on a prefab that tried to be loaded.
        /// Either set up the asset at editor or check if you set the correct path for the prefab at resources.
        /// </summary>
        /// <returns>the component from the asset - that is either found or instantiated</returns>
        /// <exception cref="MissingLaserAssetException">when the component was not present on the object nor on the instantiated prefab.</exception>
        public T GetComponent()
        {
            VerifyParent();

            if (_assetFromEditor != null)
            {
                return InstantiateFromTemplate();
            }

            if (_prefabPath != null)
            {
                return InstantiateFromResources();
            }

            throw new MissingLaserAssetException($"Component of type {typeof(T)} could not be found on asset at path {_prefabPath}. Make sure you set the correct path "
                                                + $"for the prefab at Resources, or drag a template in the Inspector.");
        }

        private void VerifyParent()
        {
            if (_parent == null)
            {
                throw new MissingLaserAssetException("Parent to instantiate laser component has to be present.");
            }
        }

        private T InstantiateFromTemplate()
        {
            return FindComponentOnObjectOrChildren(Object.Instantiate(_assetFromEditor, _parent).transform);
        }

        private T InstantiateFromResources()
        {
            var obj = Resources.Load(_prefabPath) as GameObject;

            if (obj == null)
            {
                throw new MissingLaserAssetException($"No resource exists at path {_prefabPath}.");
            }

            var loadedAsset = Object.Instantiate(obj, _parent);
            
            if (loadedAsset == null)
            {
                throw new MissingLaserAssetException($"No resource exists at path {_prefabPath}.");
            }

            return FindComponentOnObjectOrChildren(loadedAsset.transform);
        }

        private T FindComponentOnObjectOrChildren(Transform prefab)
        {
            return prefab.GetComponent<T>() ?? prefab.GetComponentInChildren<T>();
        }

        /// <summary>
        /// Builder class for the resource loader.
        /// </summary>
        public class Builder
        {
            private T _assetFromEditor;
            private string _prefabPath;
            private Transform _parent;

            public Builder WithComponent(T asset)
            {
                _assetFromEditor = asset;
                return this;
            }

            public Builder WithPrefabPath(string prefabPath)
            {
                _prefabPath = prefabPath;
                return this;
            }

            public Builder WithParent(Transform parent)
            {
                _parent = parent;
                return this;
            }

            public LaserComponentLoader<T> Build()
            {
                return new LaserComponentLoader<T>(_assetFromEditor, _prefabPath, _parent);
            }
        }
    }
}