using packt.FoodyGO.Mapping;
using UnityEngine;

namespace packt.FoodyGO.Controllers
{
    public class PlacesController : MonoBehaviour
    {
        public MapLocation location;
        public string placeId;

        public double LastUpdateTimestamp { get; internal set; }
    }
}
