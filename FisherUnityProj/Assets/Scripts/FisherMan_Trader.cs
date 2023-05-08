using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace FisherGuy
{
    public class FisherMan_Trader : SerializedMonoBehaviour
    {
        [OdinSerialize]
        private Dictionary<string, GameObject> fisherSalesObjects = new Dictionary<string, GameObject>();
    }
}
