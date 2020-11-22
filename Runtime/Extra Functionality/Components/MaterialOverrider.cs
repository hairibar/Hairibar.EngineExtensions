using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Hairibar.EngineExtensions
{
    [AddComponentMenu(""), RequireComponent(typeof(Renderer))]
    public class MaterialOverrider : MonoBehaviour
    {
        new Renderer renderer;
        Material[] originalMaterialSetup;

        readonly List<Override> overrides = new List<Override>();
        readonly OverridePriorityComparer comparer = new OverridePriorityComparer();
        Material[] overridenMaterialArray;


        public static MaterialOverrider FindOrCreate(Renderer renderer)
        {
            if (renderer.TryGetComponent(out MaterialOverrider overrider))
            {
                return overrider;
            }
            else
            {
                return renderer.gameObject.AddComponent<MaterialOverrider>();
            }
        }


        public void AddOverride(Override newOverride)
        {
            overrides.Add(newOverride);
            overrides.Sort(comparer);

            RefreshRendererMaterials();
        }

        public void RemoveOverride(Override overrideToRemove)
        {
            overrides.Remove(overrideToRemove);
            overrides.Sort(comparer);

            RefreshRendererMaterials();
        }


        void RefreshRendererMaterials()
        {
            if (overrides.Count > 0)
            {
                Override highestPriorityOverride = overrides[overrides.Count - 1];

                for (int i = 0; i < overridenMaterialArray.Length; i++)
                {
                    overridenMaterialArray[i] = highestPriorityOverride.material;
                }

                renderer.sharedMaterials = overridenMaterialArray;
            }
            else
            {
                renderer.sharedMaterials = originalMaterialSetup;
            }
        }


        void Start()
        {
            renderer = GetComponent<Renderer>();

            originalMaterialSetup = renderer.sharedMaterials;
            overridenMaterialArray = new Material[originalMaterialSetup.Length];
        }


        public class Override
        {
            readonly public Material material;
            readonly public int priority;


            public Override(Material material, int priority)
            {
                this.material = material;
                this.priority = priority;
            }
        }

        class OverridePriorityComparer : IComparer<Override>
        {
            public int Compare(Override x, Override y)
            {
                return x.priority.CompareTo(y.priority);
            }
        }
    }
}
