using System;
using UnityEngine;

namespace HilamPrototypes
{
    public abstract class Service<T> : MonoBehaviour, IService where T : MonoBehaviour
    {
        [SerializeField] protected int ServicePriority = 0;
        public abstract void Initialize();
        

        #region Service

        public int GetServiceOrder()
        {
            return ServicePriority;
        }

        public Type GetServiceType()
        {
            return typeof(T);
        }

        #endregion
    }
}
