using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using packt.FoodyGo.Mapping;
using packt.FoodyGo.Services;

namespace packt.FoodyGO.Mapping
{
    [AddComponentMenu("Mapping/GoogleMapTile")]
    public class GoogleMapTile : MonoBehaviour
    {
        public enum MapType
        {
            RoadMap,
            Satellite,
            Terrain,
            Hybrid
        }

        //Google Maps API Staticmap URL
        private const string GOOGLE_MAPS_URL = "https://maps.googleapis.com/maps/api/staticmap";

        [Header("Map Settings")]
        [Range(1,20)]
        [Tooltip("Zoom Level, 1=global - 20=house")]
        public int zoomLevel = 1;
        [Tooltip("Type of map, Road, Satellite, Terrain or Hybrid")]
        public MapType mapType = MapType.RoadMap;
        [Range(64,1024)]
        [Tooltip("Size in pixels of the map image")]
        public int size = 640;
        [Tooltip("Double the pixel resolution of the image returned")]
        public bool doubleResolution = true;

        public MapLocation worldCenterLocation;

        [Header("Tile Settings")]
        [Tooltip("Sets the tiles position in tile units")]
        public Vector2 TileOffset;
        [Tooltip("Calculated tile center")]
        public MapLocation tileCenterLocation;
        [Tooltip("Calculated tile corners")]
        public Vector2 TopLeftCorner;
        public Vector2 BottomRightCorner;

        [Header("GPS Settings")]
        [Tooltip("GPS service used to locate world center")]
        public GPSLocationService gpsLocationService;
        private double lastGPSUpdate;

        // Use this for initialization
        void Start ()
        {
            RefreshMapTile ();
        }
    
        // Update is called once per frame
        void Update ()
        {
            // 新しい位置が取得されたかどうかを確認する
            if (gpsLocationService != null &&
                gpsLocationService.IsServiceStarted && 
                lastGPSUpdate < gpsLocationService.Timestamp)
            {
                lastGPSUpdate = gpsLocationService.Timestamp;
                worldCenterLocation.Latitude = gpsLocationService.Latitude;
                worldCenterLocation.Longitude = gpsLocationService.Longitude;
                print("GoogleMapTile refreshing map texture");
                RefreshMapTile();
            }
        }

        public void RefreshMapTile() {
            
            StartCoroutine(_RefreshMapTile());
        }

        IEnumerator _RefreshMapTile ()
        {            
            // タイル中心の緯度/経度を取得
            tileCenterLocation.Latitude = GoogleMapUtils.adjustLatByPixels(worldCenterLocation.Latitude, (int)(size * 1 * TileOffset.y), zoomLevel);
            tileCenterLocation.Longitude = GoogleMapUtils.adjustLonByPixels(worldCenterLocation.Longitude, (int)(size * 1 * TileOffset.x), zoomLevel);

            var url = GOOGLE_MAPS_URL;
            var queryString = "";

            // 地図タイルをリクエストするクエリ文字列パラメーターを作成する
            queryString += "center=" + WWW.UnEscapeURL (string.Format ("{0},{1}", tileCenterLocation.Latitude, tileCenterLocation.Longitude));
            queryString += "&zoom=" + zoomLevel.ToString ();
            queryString += "&size=" + WWW.UnEscapeURL (string.Format ("{0}x{0}", size));
            queryString += "&scale=" + (doubleResolution ? "2" : "1");
            queryString += "&maptype=" + mapType.ToString ().ToLower ();
            queryString += "&format=" + "png";

            // 地図のスタイルを追加する
            queryString += "&style=element:geometry|invert_lightness:true|weight:3.1|hue:0x00ffd5";
            queryString += "&style=element:labels|visibility:off";

            var usingSensor = false;
#if MOBILE_INPUT
            usingSensor = Input.location.isEnabledByUser && Input.location.status == LocationServiceStatus.Running;
#endif

            queryString += "&sensor=" + (usingSensor ? "true" : "false");

            //set map bounds rect
            TopLeftCorner.x = GoogleMapUtils.adjustLonByPixels(tileCenterLocation.Longitude, -size, zoomLevel);
            TopLeftCorner.y = GoogleMapUtils.adjustLatByPixels(tileCenterLocation.Latitude, size, zoomLevel);

            BottomRightCorner.x = GoogleMapUtils.adjustLonByPixels(tileCenterLocation.Longitude, size, zoomLevel);
            BottomRightCorner.y = GoogleMapUtils.adjustLatByPixels(tileCenterLocation.Latitude, -size, zoomLevel);

            print(string.Format("Tile {0}x{1} requested with {2}", TileOffset.x, TileOffset.y, queryString));

            // 最後に、画像をリクエストする
            var req = UnityWebRequest.GetTexture(GOOGLE_MAPS_URL + "?" + queryString);
            // サービスが応答するまで待つ
            yield return req.Send();
            // 最初に古いテクスチャーを破棄する
            Destroy(GetComponent<Renderer>().material.mainTexture);
            // エラーをチェックする
            if (req.error != null) {
                print (string.Format ("Error loading tile {0}x{1}:  exception={2}",
                    TileOffset.x, TileOffset.y, req.error));
            } else {
                // レンダリング画像がエラーがなければ戻ってきた画像をタイルテクスチャーとして設定する
                GetComponent<Renderer> ().material.mainTexture = ((DownloadHandlerTexture)req.downloadHandler).texture;
                print (string.Format ("Tile {0}x{1} textured", TileOffset.x, TileOffset.y));
            }
        }
    }
}
