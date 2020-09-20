using GamePlatform.Models;
using Microsoft.AspNetCore.Mvc;

namespace GamePlatform.Controllers
{
    public class PokerController : Controller
    {
        private readonly IDeal deal;

        public PokerController(Deal deal)
        {
            this.deal = deal;
        }

        public ViewResult Poker()
        {
            return View();
        }

        [HttpPost]
        public void DealCards()
        {
            deal.DealCards();
        }

        [HttpPost]
        public ActionResult GetPlayerHand()
        {
            var playerHand = deal.GetPlayerHand();
            var jsonCards = new
            {
                card1Suit = playerHand[0].CardSuit.ToString(),
                card1Value = playerHand[0].CardValue.ToString(),
                card2Suit = playerHand[1].CardSuit.ToString(),
                card2Value = playerHand[1].CardValue.ToString(),
                card3Suit = playerHand[2].CardSuit.ToString(),
                card3Value = playerHand[2].CardValue.ToString(),
                card4Suit = playerHand[3].CardSuit.ToString(),
                card4Value = playerHand[3].CardValue.ToString(),
                card5Suit = playerHand[4].CardSuit.ToString(),
                card5Value = playerHand[4].CardValue.ToString(),
            };
            return Json(jsonCards);
        }

        [HttpPost]
        public ActionResult GetComputerHand()
        {
            var computerHand = deal.GetComputerHand();
            var jsonCards = new
            {
                card1Suit = computerHand[0].CardSuit.ToString(),
                card1Value = computerHand[0].CardValue.ToString(),
                card2Suit = computerHand[1].CardSuit.ToString(),
                card2Value = computerHand[1].CardValue.ToString(),
                card3Suit = computerHand[2].CardSuit.ToString(),
                card3Value = computerHand[2].CardValue.ToString(),
                card4Suit = computerHand[3].CardSuit.ToString(),
                card4Value = computerHand[3].CardValue.ToString(),
                card5Suit = computerHand[4].CardSuit.ToString(),
                card5Value = computerHand[4].CardValue.ToString(),
            };
            return Json(jsonCards);
        }

        [HttpPost]
        public ActionResult ChangeCard(int id)
        {
            deal.ChangeCard(id);
            var playerHand = deal.GetPlayerHand();
            var jsonCards = new
            {
                card1Suit = playerHand[0].CardSuit.ToString(),
                card1Value = playerHand[0].CardValue.ToString(),
                card2Suit = playerHand[1].CardSuit.ToString(),
                card2Value = playerHand[1].CardValue.ToString(),
                card3Suit = playerHand[2].CardSuit.ToString(),
                card3Value = playerHand[2].CardValue.ToString(),
                card4Suit = playerHand[3].CardSuit.ToString(),
                card4Value = playerHand[3].CardValue.ToString(),
                card5Suit = playerHand[4].CardSuit.ToString(),
                card5Value = playerHand[4].CardValue.ToString(),
            };
            return Json(jsonCards);
        }

        [HttpPost]
        public ActionResult CheckGame()
        {
            ILayout playerLayout = new Layout();
            ILayout computerLayout = new Layout();
            Hand playerHand = playerLayout.CardsLayout(deal.GetPlayerHand());
            Hand computerHand = computerLayout.CardsLayout(deal.GetComputerHand());

            string winner = string.Empty;
            Hand winnerLayout;

            if (playerHand > computerHand)
                winner = "player";
            else if (playerHand < computerHand)
                winner = "computer";
            else
            {
                if (playerLayout.GetHandValue() > computerLayout.GetHandValue())
                    winner = "player";
                else if (playerLayout.GetHandValue() < computerLayout.GetHandValue())
                    winner = "computer";
                else winner = "draw";
            }
            if (winner == "player")
                winnerLayout = playerHand;
            else
                winnerLayout = computerHand;

            return Json(winner + ";" + winnerLayout.ToString());
        }
    }
}