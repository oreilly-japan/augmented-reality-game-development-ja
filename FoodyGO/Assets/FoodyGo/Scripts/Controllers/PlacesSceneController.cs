using System;
using packt.FoodyGO.Managers;
using UnityEngine;
using packt.FoodyGO.Mapping;
using packt.FoodyGO.UI;
using packt.FoodyGO.Services;

namespace packt.FoodyGO.Controllers
{
    public class PlacesSceneController : MonoBehaviour
    {
        public GoogleStreetViewTexture googleStreetViewTexture;
        public GooglePlacesDetailInfo googlePlacesDetailInfo;
        public PlacesSceneUIController placesSceneUIController;

        public void OnClickSell()
        {
            if(googlePlacesDetailInfo.PlaceResult != null)
            {
                var offer = MonsterExchangeService.Instance.PriceMonsters(googlePlacesDetailInfo.PlaceResult);
                placesSceneUIController.ShowOffer(offer);
            }
        }

        public void OnCloseScene()
        {
            GameManager.Instance.CloseMe(this);
        }        
        public void ResetScene(string placeId, MapLocation location)
        {
            if(googleStreetViewTexture != null)
            {
                googleStreetViewTexture.location = location;
            }
            if(googlePlacesDetailInfo != null)
            {
                googlePlacesDetailInfo.placeId = placeId;
            }
        }
    }
}
