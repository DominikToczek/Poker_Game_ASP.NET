using NUnit.Framework;
using System;
using GamePlatform.Models;

namespace GamingPlatformTests
{
    [TestFixture]
    class FiveCardsPokerTests
    {
        [Test]
        public void DealCards_WhenCalled_DeckOfCardsCreated()
        {
            Deal _deal = new Deal();
            _deal.DealCards();
            var actual = _deal.Deck.Length;
            var expected = 52;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void DealCards_WhenCalled_PlayerHandCreated()
        {
            Deal _deal = new Deal();
            _deal.DealCards();
            var actual = _deal.GetPlayerHand().Length;
            var expected = 5;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void DealCards_WhenCalled_ComputerHandCreated()
        {
            Deal _deal = new Deal();
            _deal.DealCards();
            var actual = _deal.GetComputerHand().Length;
            var expected = 5;
            Assert.AreEqual(expected, actual);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        public void ChangeCard_SelectCardNumber_CardChanged(int cardNumber)
        {
            Deal _deal = new Deal();
            _deal.DealCards();
            var hand = _deal.GetPlayerHand();
            var expected = new Tuple<Card.SUIT, Card.VALUE>(hand[cardNumber - 1].CardSuit, hand[cardNumber - 1].CardValue);
            _deal.ChangeCard(cardNumber);
            hand = _deal.GetPlayerHand();
            var actual = new Tuple<Card.SUIT, Card.VALUE>(hand[cardNumber - 1].CardSuit, hand[cardNumber - 1].CardValue);
            Assert.AreNotEqual(expected, actual);
        }

        [Test]
        public void SortCards_SortPlayerCards_CardsSortedByValue()
        {
            Deal _deal = new Deal();
            _deal.DealCards();
            _deal.SortCards();
            var sortedHand = _deal.GetSortedPlayerHand();

            for (int i = 0; i < sortedHand.Length - 2; i++)
            {
                Assert.LessOrEqual(sortedHand[i].CardValue, sortedHand[i + 1].CardValue);
            }
        }

        [Test]
        public void CardsLayout_WhenHighCardHand_ReturnsHighCardType()
        {
            Layout _layout = new Layout();
            Card[] hand =
            {
                new Card(Card.SUIT.CLUBS, Card.VALUE.ACE),
                new Card(Card.SUIT.DIAMONDS, Card.VALUE.EIGHT),
                new Card(Card.SUIT.HEARTS, Card.VALUE.FOUR),
                new Card(Card.SUIT.DIAMONDS, Card.VALUE.QUEEN),
                new Card(Card.SUIT.SPADES, Card.VALUE.TEN)
            };

            var expected = Hand.HighCard;
            var actual = _layout.CardsLayout(hand);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CardsLayout_WhenOnePairHand_ReturnsOnePairType()
        {
            Layout _layout = new Layout();
            Card[] hand =
            {
                new Card(Card.SUIT.CLUBS, Card.VALUE.ACE),
                new Card(Card.SUIT.DIAMONDS, Card.VALUE.TEN),
                new Card(Card.SUIT.HEARTS, Card.VALUE.FOUR),
                new Card(Card.SUIT.DIAMONDS, Card.VALUE.QUEEN),
                new Card(Card.SUIT.SPADES, Card.VALUE.ACE)
            };

            var expected = Hand.OnePair;
            var actual = _layout.CardsLayout(hand);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CardsLayout_WhenTwoPairHand_ReturnsTwoPairType()
        {
            Layout _layout = new Layout();
            Card[] hand =
            {
                new Card(Card.SUIT.CLUBS, Card.VALUE.ACE),
                new Card(Card.SUIT.DIAMONDS, Card.VALUE.FOUR),
                new Card(Card.SUIT.HEARTS, Card.VALUE.TEN),
                new Card(Card.SUIT.DIAMONDS, Card.VALUE.ACE),
                new Card(Card.SUIT.SPADES, Card.VALUE.FOUR)
            };

            var expected = Hand.TwoPair;
            var actual = _layout.CardsLayout(hand);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CardsLayout_WhenThreeOfKindHand_ReturnsThreeOfKindType()
        {
            Layout _layout = new Layout();
            Card[] hand =
            {
                new Card(Card.SUIT.CLUBS, Card.VALUE.ACE),
                new Card(Card.SUIT.DIAMONDS, Card.VALUE.TEN),
                new Card(Card.SUIT.HEARTS, Card.VALUE.ACE),
                new Card(Card.SUIT.DIAMONDS, Card.VALUE.QUEEN),
                new Card(Card.SUIT.SPADES, Card.VALUE.ACE)
            };

            var expected = Hand.ThreeOfKind;
            var actual = _layout.CardsLayout(hand);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CardsLayout_WhenStraightHand_ReturnsStraightType()
        {
            Layout _layout = new Layout();
            Card[] hand =
            {
                new Card(Card.SUIT.CLUBS, Card.VALUE.FOUR),
                new Card(Card.SUIT.DIAMONDS, Card.VALUE.SEVEN),
                new Card(Card.SUIT.HEARTS, Card.VALUE.SIX),
                new Card(Card.SUIT.DIAMONDS, Card.VALUE.FIVE),
                new Card(Card.SUIT.SPADES, Card.VALUE.EIGHT)
            };

            var expected = Hand.Straight;
            var actual = _layout.CardsLayout(hand);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CardsLayout_WhenFlushHand_ReturnsFlushType()
        {
            Layout _layout = new Layout();
            Card[] hand =
            {
                new Card(Card.SUIT.HEARTS, Card.VALUE.ACE),
                new Card(Card.SUIT.HEARTS, Card.VALUE.EIGHT),
                new Card(Card.SUIT.HEARTS, Card.VALUE.FOUR),
                new Card(Card.SUIT.HEARTS, Card.VALUE.QUEEN),
                new Card(Card.SUIT.HEARTS, Card.VALUE.TEN)
            };

            var expected = Hand.Flush;
            var actual = _layout.CardsLayout(hand);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CardsLayout_WhenFullHouseHand_ReturnsFullHouseType()
        {
            Layout _layout = new Layout();
            Card[] hand =
            {
                new Card(Card.SUIT.CLUBS, Card.VALUE.ACE),
                new Card(Card.SUIT.DIAMONDS, Card.VALUE.QUEEN),
                new Card(Card.SUIT.HEARTS, Card.VALUE.ACE),
                new Card(Card.SUIT.DIAMONDS, Card.VALUE.QUEEN),
                new Card(Card.SUIT.SPADES, Card.VALUE.ACE)
            };

            var expected = Hand.FullHouse;
            var actual = _layout.CardsLayout(hand);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CardsLayout_WhenFourOfKindHand_ReturnsFourOfKindType()
        {
            Layout _layout = new Layout();
            Card[] hand =
            {
                new Card(Card.SUIT.CLUBS, Card.VALUE.ACE),
                new Card(Card.SUIT.DIAMONDS, Card.VALUE.ACE),
                new Card(Card.SUIT.HEARTS, Card.VALUE.TEN),
                new Card(Card.SUIT.DIAMONDS, Card.VALUE.ACE),
                new Card(Card.SUIT.SPADES, Card.VALUE.ACE)
            };

            var expected = Hand.FourOfKind;
            var actual = _layout.CardsLayout(hand);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CardsLayout_WhenStraightFlushHand_ReturnsStraightFlushType()
        {
            Layout _layout = new Layout();
            Card[] hand =
            {
                new Card(Card.SUIT.CLUBS, Card.VALUE.NINE),
                new Card(Card.SUIT.CLUBS, Card.VALUE.SEVEN),
                new Card(Card.SUIT.CLUBS, Card.VALUE.EIGHT),
                new Card(Card.SUIT.CLUBS, Card.VALUE.SIX),
                new Card(Card.SUIT.CLUBS, Card.VALUE.TEN)
            };

            var expected = Hand.StraightFlush;
            var actual = _layout.CardsLayout(hand);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CardsLayout_WhenRoyalFlushHand_ReturnsRoyalFlushType()
        {
            Layout _layout = new Layout();
            Card[] hand =
            {
                new Card(Card.SUIT.HEARTS, Card.VALUE.QUEEN),
                new Card(Card.SUIT.HEARTS, Card.VALUE.KING),
                new Card(Card.SUIT.HEARTS, Card.VALUE.ACE),
                new Card(Card.SUIT.HEARTS, Card.VALUE.JACK),
                new Card(Card.SUIT.HEARTS, Card.VALUE.TEN)
            };

            var expected = Hand.RoyalFlush;
            var actual = _layout.CardsLayout(hand);
            Assert.AreEqual(expected, actual);
        }
    }
}