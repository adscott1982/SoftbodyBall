using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    class Side
    {
        public Side(GameObject sideObject, SideController sideController)
        {
            this.Object = sideObject;
            this.Controller = sideController;
        }

        public GameObject Object { get; }
        public SideController Controller { get; }
    }
}
