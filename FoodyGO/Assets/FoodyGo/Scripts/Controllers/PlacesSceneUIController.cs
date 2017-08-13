using System;
using UnityEngine;
using packt.FoodyGO.Services;
using packt.FoodyGO.UI;

namespace packt.FoodyGO.Controllers
{
    public class PlacesSceneUIController : MonoBehaviour
    {
        public GameObject SellButton;
        public GameObject OfferDialog;
        public GameObject RefuseDialog;
                
        public MonsterOffer CurrentOffer { get; set; }

        void OnEnable()
        {
            if (OfferDialog == null || RefuseDialog == null) return;
            OfferDialog.SetActive(false);
            RefuseDialog.SetActive(false);
        }
        public void ShowOffer(MonsterOffer offer)
        {
            if (OfferDialog == null || RefuseDialog == null) return;

            if (offer != null)
            {
                CurrentOffer = offer;
                OfferDialog.SetActive(true);
                SellButton.SetActive(false);
                OfferDialog.GetComponent<MonsterOfferPresenter>().PresentOffer(offer);
            }
            else
            {
                RefuseDialog.SetActive(true);
                SellButton.SetActive(false);
            }            
        }

        public void AcceptOffer()
        {
            OfferDialog.SetActive(false);
            SellButton.SetActive(true);

            var offer = CurrentOffer;
            InventoryService.Instance.DeleteMonster(offer.Monster);
            var player = InventoryService.Instance.ReadPlayer(1);
            player.Experience += offer.Experience;
            InventoryService.Instance.UpdatePlayer(player);
            foreach(var i in offer.Items)
            {
                InventoryService.Instance.CreateInventoryItem(i);
            }
        }

        public void RefuseOffer()
        {
            OfferDialog.SetActive(false);
            SellButton.SetActive(true);
        }

        public void OK()
        {
            RefuseDialog.SetActive(false);
            SellButton.SetActive(true);
        }
    }
}