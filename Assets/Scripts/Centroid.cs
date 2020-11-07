using UnityEngine;

namespace Assets.Scripts
{
    class Centroid
    {
        public Centroid(GameObject centroidObject, CentroidController centroidController)
        {
            this.Object = centroidObject;
            this.Controller = centroidController;
        }

        public GameObject Object { get; }
        public CentroidController Controller { get; }
    }
}
