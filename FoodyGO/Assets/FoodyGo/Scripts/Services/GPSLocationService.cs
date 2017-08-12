using System.Collections;
using UnityEngine;

namespace packt.FoodyGo.Services
{
    [AddComponentMenu("Services/GPSLocationService")]
    public class GPSLocationService : MonoBehaviour
    {
        public bool IsServiceStarted;
        public float Latitude;
        public float Longitude;
        public float Altitude;
        public float Accuracy;
        public double Timestamp;

        void Start()
        {
            print("Starting GPSLocationService");
            StartCoroutine(StartService());
        }
        IEnumerator StartService()
        {
            // First, check if user has location service enabled
            if (!Input.location.isEnabledByUser)
            {
                print("location not enabled by user, existing");
                yield break;
            }

            // Start service before querying location
            Input.location.Start();

            // Wait until service initializes
            int maxWait = 20;
            while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
            {
                yield return new WaitForSeconds(1);
                maxWait--;
            }

            // Service didn't initialize in 20 seconds
            if (maxWait < 1)
            {
                print("Timed out");
                yield break;
            }

            // Connection has failed
            if (Input.location.status == LocationServiceStatus.Failed)
            {
                print("Unable to determine device location.");
                yield break;
            }
            else
            {
                print("GSPLocationService started");
                IsServiceStarted = true;
                // Access granted and location value could be retrieved
                print("Location initialized at: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
            }

           
        }

        void Update()
        {
            if(Input.location.status == LocationServiceStatus.Running)
            {                
                Latitude = Input.location.lastData.latitude;
                Longitude = Input.location.lastData.longitude;
                Altitude = Input.location.lastData.altitude;
                Accuracy = Input.location.lastData.horizontalAccuracy;
                Timestamp = Input.location.lastData.timestamp;
            }
        }

        void Dispose()
        {
            if (IsServiceStarted)
                Input.location.Stop();
        }
    }
}
