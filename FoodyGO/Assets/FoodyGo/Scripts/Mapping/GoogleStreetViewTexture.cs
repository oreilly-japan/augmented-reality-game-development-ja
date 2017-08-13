using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System;
using UnityEngine.UI;

namespace packt.FoodyGO.Mapping
{
    public class GoogleStreetViewTexture : MonoBehaviour
    {
        //Google Maps API StreetView URL
        private const string GOOGLE_STREET_VIEW_URL = "https://maps.googleapis.com/maps/api/streetview";

        public MapLocation location;
        public Vector2 size;
        public float fov;
        public float heading;
        public float pitch;
        public bool useCompass;
        public string MapsAPIKey;



        // Use this for initialization
        void Start()
        {
            if (useCompass)
            {
                Input.compass.enabled = true;
            }
        }

        void OnEnable()
        {
            if (useCompass)
            {
                heading = Input.compass.trueHeading;                
            }
            StartCoroutine(LoadTexture());
        }
        
        IEnumerator LoadTexture()
        {
            var queryString = string.Format("location={0}&fov={1}&heading={2}&key={3}&size={4}x{5}&pitch={6}"
                , location.LatLong, fov, heading, MapsAPIKey, size.x, size.y, pitch);

            var req = UnityWebRequest.GetTexture(GOOGLE_STREET_VIEW_URL + "?" + queryString);
            //yield until the service responds
            yield return req.Send();
            //first destroy the old texture first
            Destroy(GetComponent<RawImage>().material.mainTexture);
            //when the image returns set it as the tile texture
            GetComponent<RawImage>().texture = ((DownloadHandlerTexture)req.downloadHandler).texture;
            GetComponent<RawImage>().material.mainTexture = ((DownloadHandlerTexture)req.downloadHandler).texture;
        }

        // Update is called once per frame
        void Update()
        {
            if (useCompass)
            {
                var dA = Mathf.DeltaAngle(heading, Input.compass.trueHeading);
                if (dA > fov / 2)
                {
                    heading = Input.compass.trueHeading;
                    //device changed, reset texture
                    StartCoroutine(LoadTexture());
                }
            }
        }
    }
}
