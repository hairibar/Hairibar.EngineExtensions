using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Hairibar.EngineExtensions
{
    public class TimeScaler
    {
        readonly Dictionary<Object, float> requests = new Dictionary<Object, float>();

        static TimeScaler Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TimeScaler();
                }
                return _instance;
            }
        }
        static TimeScaler _instance;


        public static void Request(float value, Object requester)
        {
            if (value == 1)
            {
                Instance.requests.Remove(requester);
            }
            else
            {
                Instance.requests[requester] = value;
            }

            Instance.RemoveDestroyedRequesters();
            Instance.RefreshTimeScale();
        }

        void RefreshTimeScale()
        {
            float scale = 1;

            foreach (float multiplier in requests.Values)
            {
                scale *= multiplier;
            }

            Time.timeScale = scale;
        }

        void RemoveDestroyedRequesters()
        {
            List<Object> destroyedObjects = requests.Where(pair => !pair.Key)
                         .Select(pair => pair.Key)
                         .ToList();

            foreach (Object destroyedObject in destroyedObjects)
            {
                requests.Remove(destroyedObject);
            }
        }


        //Private constructor so that users can't create instances. 
        //Only the Instance property should instantiate this class.
        TimeScaler() { }
    }
}
