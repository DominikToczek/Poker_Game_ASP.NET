using System;

namespace Cards
{
    public class DeckOfCards : IDeckOfCards
    {
        public Card[] Deck { get; private set; }

        public void CreateDeck()
        {
            Deck = new Card[52];
            int i = 0;

            foreach (Card.SUIT suit in Enum.GetValues(typeof(Card.SUIT)))
            {
                foreach (Card.VALUE value in Enum.GetValues(typeof(Card.VALUE)))
                {
                    Deck[i] = new Card { CardSuit = suit, CardValue = value };
                    i++;
                }
            }
        }

        public void ShuffleDeck()
        {
            Random rnd = new Random();
            Card tmp;

            for (int counter = 0; counter < 1000; counter++)
            {
                for (int i = 0; i < 52; i++)
                {
                    int j = rnd.Next(52);
                    tmp = Deck[j];
                    Deck[j] = Deck[i];
                    Deck[i] = tmp;
                }
            }
        }
    }
}