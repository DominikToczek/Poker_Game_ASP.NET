using NUnit.Framework;
using System;
using GamePlatform.Models;

namespace GamingPlatformTests
{
    [TestFixture]
    class CardsTests
    {
        [TestCase(Card.SUIT.CLUBS, Card.VALUE.KING)]
        [TestCase(Card.SUIT.CLUBS, Card.VALUE.FOUR)]
        [TestCase(Card.SUIT.CLUBS, Card.VALUE.NINE)]
        [TestCase(Card.SUIT.DIAMONDS, Card.VALUE.ACE)]
        [TestCase(Card.SUIT.DIAMONDS, Card.VALUE.QUEEN)]
        [TestCase(Card.SUIT.DIAMONDS, Card.VALUE.THREE)]
        [TestCase(Card.SUIT.HEARTS, Card.VALUE.TWO)]
        [TestCase(Card.SUIT.HEARTS, Card.VALUE.SIX)]
        [TestCase(Card.SUIT.HEARTS, Card.VALUE.SEVEN)]
        [TestCase(Card.SUIT.SPADES, Card.VALUE.JACK)]
        [TestCase(Card.SUIT.SPADES, Card.VALUE.EIGHT)]
        [TestCase(Card.SUIT.SPADES, Card.VALUE.FIVE)]
        public void CardConstructorWithParameters_WhenCalled_CardObjectCreated(Card.SUIT suit, Card.VALUE value)
        {
            Card card = new Card(suit, value);
            var expected = new Tuple<Card.SUIT, Card.VALUE>(suit, value);
            var actual = new Tuple<Card.SUIT, Card.VALUE>(card.CardSuit, card.CardValue);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateDeck_WhenCalled_DeckOfCardsEquals52Cards()
        {
            DeckOfCards _deck = new DeckOfCards();
            _deck.CreateDeck();
            var expected = 52;
            var actual = _deck.Deck.Length;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ShuffleDeck_WhenCalled_CardsInDeckWereShuffled()
        {
            DeckOfCards _deck = new DeckOfCards();
            _deck.CreateDeck();
            var expected = _deck.Deck;
            _deck.ShuffleDeck();
            var actual = _deck.Deck;
            for (int i=0; i<expected.Length-1; i++)
            {
                Assert.AreNotSame(
                    new Tuple<Card.SUIT, Card.VALUE>(expected[i].CardSuit, expected[i].CardValue),
                    new Tuple<Card.SUIT, Card.VALUE>(actual[i].CardSuit, actual[i].CardValue)
                    );
            }
        }
    }
}
