using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HilamPrototypes
{
    public class ServiceLocator : Singleton<ServiceLocator>
    {
        private Dictionary<Type, object> _services = new();
        protected override void Awake()
        {
            base.Awake();
            InitializeAllServices();
        }
       
        private bool Register(IService service)
        {
            Type type = service.GetServiceType();

            if (_services.TryAdd(type, service) == false)
            {
                Debug.LogError($"Service Locator Located Registered Service:{type.FullName}");
                return false;
            }

            return true;
        }

        private void InitializeAllServices()
        {
            var servicesInSceneEnum = FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include,FindObjectsSortMode.None).OfType<IService>();
            {
                servicesInSceneEnum = servicesInSceneEnum.OrderBy(service => service.GetServiceOrder());

                var servicesInScene = servicesInSceneEnum.ToArray();

                foreach (var service in servicesInScene)
                {
                    bool isSuccessful = Register(service);
                    if (isSuccessful)
                    {
                        service.Initialize();
                    }
                }
            }
        }

        public static T GetService<T>() where T : class, IService
        {
            var service = Instance._services[typeof(T)];
            return service as T;
        }
    }
}
