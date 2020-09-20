using Cards;
using System;
using System.Linq;

namespace FiveCardsPoker
{
    public class Deal : DeckOfCards, IDeal
    {
        private Card[] playerHand;
        private Card[] computerHand;
        private Card[] sortedPlayerHand;
        private Card[] sortedComputerHand;
        private int counter;
        private readonly Layout layout = new Layout();

        public Card[] GetPlayerHand()
        {
            if (playerHand is null)
                throw new NullReferenceException();

            return playerHand;
        }

        public Card[] GetComputerHand()
        {
            if (computerHand is null)
                throw new NullReferenceException();
            return computerHand;
        }

        public Card[] GetSortedPlayerHand()
        {
            if (computerHand is null)
                throw new NullReferenceException();
            return sortedPlayerHand;
        }

        public Card[] GetSortedComputerHand()
        {
            if (computerHand is null)
                throw new NullReferenceException();
            return sortedComputerHand;
        }

        public void DealCards()
        {
            CreateDeck();
            ShuffleDeck();
            GetHand();
        }

        public void ChangeCard(int cardNumber)
        {
            if (cardNumber < 1 || cardNumber > 5)
                throw new ArgumentOutOfRangeException();
            playerHand[cardNumber - 1] = Deck[counter];
            counter++;
        }

        public void SortCards()
        {
            var player = from hand in playerHand
                         orderby hand.CardValue
                         select hand;

            var computer = from hand in computerHand
                           orderby hand.CardValue
                           select hand;

            int i = 0;
            foreach (var card in player.ToList())
            {
                sortedPlayerHand[i] = card;
                i++;
            }
            int j = 0;
            foreach (var card in computer.ToList())
            {
                sortedComputerHand[j] = card;
                j++;
            }
        }

        private void HandOfCards()
        {
            playerHand = new Card[5];
            computerHand = new Card[5];
            sortedPlayerHand = new Card[5];
            sortedComputerHand = new Card[5];
        }

        private void GetHand()
        {
            HandOfCards();
            for (int i = 0; i < 5; i++)
            {
                playerHand[i] = Deck[i];
                computerHand[i] = Deck[i + 5];
                counter += 2;
            }
        }
    }
}