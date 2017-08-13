using System;
using System.Collections;
using System.Collections.Generic;
using TinyJson;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

namespace packt.FoodyGO.UI
{
    public class GooglePlacesDetailInfo : MonoBehaviour
    {

        //Google Places API Detail URL
        private const string GOOGLE_PLACES_DETAIL_URL = "https://maps.googleapis.com/maps/api/place/details/json";
        private const string GOOGLE_DETAIL_PHOTO_URL = "https://maps.googleapis.com/maps/api/place/photo";
        public bool doSlideShow = true;
        public float showSlideTimeSeconds = 10;
        public string placeId;
        public string PlacesAPIKey;
        public GameObject photoPanel;
        private Renderer photoRenderer;
        public Text header;
        public Text rating;
        public Text price;
        private int idx;
        private int loadAttempts;

        private string star = "";
        private string halfStar = "";
        private string currency = "$";

        public Result PlaceResult { get; set; }

        void OnEnable()
        {
            StartCoroutine(LoadPlacesDetail());
            if (photoPanel != null)
            {
                photoPanel.SetActive(true);
                photoRenderer = photoPanel.GetComponent<Renderer>();
            }
            loadAttempts = 0;
            PlaceResult = null;  
        }   

        IEnumerator LoadPlacesDetail()
        {
            var queryString = string.Format("placeid={0}&key={1}"
                , placeId, PlacesAPIKey);

            var req = UnityWebRequest.Get(GOOGLE_PLACES_DETAIL_URL + "?" + queryString);
            //yield until the service responds
            yield return req.Send();
            var json = req.downloadHandler.text;
            ParseSearchResult(json);
        }

        private void ParseSearchResult(string json)
        {
            var dsr = JSONParser.FromJson<DetailSearchResult>(json);
            print(dsr);
            PlaceResult = dsr.result;

            //check status is good, otherwise just flush
            if (dsr.status == "OK")
            {
                if (header != null)
                {
                    header.text = PlaceResult.name;
                }
                if (rating != null)
                {
                    rating.text = string.Empty;
                    int r = (int)PlaceResult.rating;
                    var rate = string.Empty;
                    for (int i = 1; i <= r; i++)
                    {
                        rate += star;
                    }
                    if (PlaceResult.rating - r >= .5f)
                    {
                        rate += halfStar;
                    }
                    rating.text = rate;
                }
                if(price != null)
                {
                    price.text = string.Empty;
                    var cur = string.Empty;
                    for (int i = 1; i <= PlaceResult.price_level; i++)
                    {
                        cur += currency;
                    }
                    price.text = cur;
                }
                if (PlaceResult.photos != null && PlaceResult.photos.Count > 0)
                {
                    idx = 0;
                    StartCoroutine(LoadPhotoTexture(PlaceResult.photos[idx]));
                    StartCoroutine(SlideShow(PlaceResult));
                }
                else
                {
                    photoPanel.SetActive(false);
                }
            }
            else
            {
                //if failed try again to load
                if (loadAttempts++ < 3) LoadPlacesDetail();
            }
        }

        private IEnumerator SlideShow(Result result)
        {
            while(doSlideShow && idx < result.photos.Count - 1)
            {
                yield return new WaitForSeconds(showSlideTimeSeconds);
                idx++;
                StartCoroutine(LoadPhotoTexture(result.photos[idx]));
            }
        }

        private IEnumerator LoadPhotoTexture(Photo photo)
        {            
            var queryString = string.Format("photoreference={0}&key={1}&maxwidth=800"
                , photo.photo_reference, PlacesAPIKey);

            var url = GOOGLE_DETAIL_PHOTO_URL + "?" + queryString;
            var req = UnityWebRequest.GetTexture(url);
            //yield until the service responds
            yield return req.Send();
            //first destroy the old texture first
            Destroy(photoRenderer.material.mainTexture);
            //when the image returns set it as the tile texture
            photoRenderer.material.mainTexture = ((DownloadHandlerTexture)req.downloadHandler).texture;            
        }
    }


    public class AddressComponent
    {
        public string long_name { get; set; }
        public string short_name { get; set; }
        public List<string> types { get; set; }
    }

    public class Location
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Northeast
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Southwest
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Viewport
    {
        public Northeast northeast { get; set; }
        public Southwest southwest { get; set; }
    }

    public class Geometry
    {
        public Location location { get; set; }
        public Viewport viewport { get; set; }
    }

    public class Close
    {
        public int day { get; set; }
        public string time { get; set; }
    }

    public class Open
    {
        public int day { get; set; }
        public string time { get; set; }
    }

    public class Period
    {
        public Close close { get; set; }
        public Open open { get; set; }
    }

    public class OpeningHours
    {
        public bool open_now { get; set; }
        public List<Period> periods { get; set; }
        public List<string> weekday_text { get; set; }
    }

    public class Photo
    {
        public int height { get; set; }
        public List<string> html_attributions { get; set; }
        public string photo_reference { get; set; }
        public int width { get; set; }
    }

    public class Aspect
    {
        public int rating { get; set; }
        public string type { get; set; }
    }

    public class Review
    {
        public List<Aspect> aspects { get; set; }
        public string author_name { get; set; }
        public string author_url { get; set; }
        public string language { get; set; }
        public int rating { get; set; }
        public string text { get; set; }
        public int time { get; set; }
        public string profile_photo_url { get; set; }
    }

    public class Result
    {
        public List<AddressComponent> address_components { get; set; }
        public string adr_address { get; set; }
        public string formatted_address { get; set; }
        public string formatted_phone_number { get; set; }
        public Geometry geometry { get; set; }
        public string icon { get; set; }
        public string id { get; set; }
        public string international_phone_number { get; set; }
        public string name { get; set; }
        public OpeningHours opening_hours { get; set; }
        public List<Photo> photos { get; set; }
        public string place_id { get; set; }
        public double rating { get; set; }
        public int price_level { get; set; }
        public string reference { get; set; }
        public List<Review> reviews { get; set; }
        public string scope { get; set; }
        public List<string> types { get; set; }
        public string url { get; set; }
        public int utc_offset { get; set; }
        public string vicinity { get; set; }
        public string website { get; set; }

        public override string ToString()
        {
            return string.Format("Place: {0}, Rating: {1}, Reviews: {2}",
                name, rating, reviews.Count);
        }
    }

    public class DetailSearchResult
    {
        public List<object> html_attributions { get; set; }
        public Result result { get; set; }
        public string status { get; set; }

        public override string ToString()
        {
            return "Status: " + status + "Result: " + result.ToString();
        }
    }

}
