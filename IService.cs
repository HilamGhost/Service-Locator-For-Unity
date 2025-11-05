using System;
using UnityEngine;

namespace HilamPrototypes
{
    public interface IService
    {
        public int GetServiceOrder();
        public Type GetServiceType();

        public void Initialize();

    }
}
