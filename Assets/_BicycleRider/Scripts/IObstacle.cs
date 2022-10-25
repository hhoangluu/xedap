using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BicycleRider
{
    public interface IObstacle
    {
        public void OnHitPlayer();

        public void OnNearPlayer();

    }
}
