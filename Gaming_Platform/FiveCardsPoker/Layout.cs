using Cards;
using System;
using System.Linq;

namespace FiveCardsPoker
{
    public class Layout : ILayout
    {
        public int HandValue { get; set; } = 0;
        private int heartsSum, diamondsSum, spadesSum, clubsSum;

        private Card[] sortedHand;

        public Hand CardsLayout(Card[] hand)
        {
            GetSortedHand(hand);
            GetSum(sortedHand);

            if (IsRoyalFlush())
                return Hand.RoyalFlush;
            if (IsStraightFlush())
                return Hand.StraightFlush;
            if (IsFourOfKind(sortedHand))
                return Hand.FourOfKind;
            if (IsFullHouse(sortedHand))
                return Hand.FullHouse;
            if (IsFlush(sortedHand))
                return Hand.Flush;
            if (IsStraight(sortedHand))
                return Hand.Straight;
            if (IsThreeOfKind(sortedHand))
                return Hand.ThreeOfKind;
            if (IsTwoPair(sortedHand))
                return Hand.TwoPair;
            if (IsOnePair(sortedHand))
                return Hand.OnePair;
            return Hand.HighCard;
        }

        private void HandOfSortedCards()
        {
            sortedHand = new Card[5];
        }

        private void GetSortedHand(Card[] hand)
        {
            var handOfCards = from cards in hand
                         orderby cards.CardValue
                         select cards;

            HandOfSortedCards();
            int i = 0;
            foreach (var card in handOfCards.ToList())
            {
                sortedHand[i] = card;
                i++;
            }
        }

        private void GetSum(Card[] hand)
        {
            for (int i = 0; i < 5; i++)
            {
                switch (hand[i].CardSuit)
                {
                    case Card.SUIT.HEARTS:
                        heartsSum++;
                        break;
                    case Card.SUIT.DIAMONDS:
                        diamondsSum++;
                        break;
                    case Card.SUIT.CLUBS:
                        clubsSum++;
                        break;
                    default:
                        spadesSum++;
                        break;
                }
            }
        }

        private bool IsRoyalFlush()
        {
            if (IsFlush(sortedHand))
            {
                if (IsStraight(sortedHand) && sortedHand[0].CardValue == Card.VALUE.TEN)
                {
                    HandValue = 1000 + (int)sortedHand[4].CardValue;
                    return true;
                }
                return false;
            }
            return false;
        }

        private bool IsStraightFlush()
        {
            if (IsFlush(sortedHand))
            {
                if (IsStraight(sortedHand))
                {
                    HandValue = 900 + (int)sortedHand[4].CardValue;
                    return true;
                }
                return false;
            }
            return false;
        }

        private bool IsFourOfKind(Card[] hand)
        {
            if (hand[0].CardValue == hand[3].CardValue || hand[1].CardValue == hand[4].CardValue)
            {
                HandValue = 800 + (int)hand[3].CardValue;
                return true;
            }
            return false;
        }

        private bool IsFullHouse(Card[] hand)
        {
            if (hand[0].CardValue == hand[2].CardValue && hand[3].CardValue == hand[4].CardValue)
            {
                HandValue = 700 + (int)hand[2].CardValue;
                return true;
            }
            if (hand[0].CardValue == hand[1].CardValue && hand[2].CardValue == hand[4].CardValue)
            {
                HandValue = 700 + (int)hand[4].CardValue;
                return true;
            }
            return false;
        }

        private bool IsFlush(Card[] hand)
        {
            if (heartsSum == 5 || diamondsSum == 5 || spadesSum == 5 || clubsSum == 5)
            {
                HandValue = 600 + (int)hand[4].CardValue;
                return true;
            }
            return false;
        }

        private bool IsStraight(Card[] hand)
        {
            if (hand[0].CardValue == hand[1].CardValue - 1 &&
                hand[1].CardValue == hand[2].CardValue - 1 &&
                hand[2].CardValue == hand[3].CardValue - 1 &&
                hand[3].CardValue == hand[4].CardValue - 1)
            {
                HandValue = 500 + (int)hand[4].CardValue;
                return true;
            }
            return false;
        }

        private bool IsThreeOfKind(Card[] hand)
        {
            if ((hand[0].CardValue == hand[2].CardValue) ||
                (hand[1].CardValue == hand[3].CardValue) ||
                (hand[2].CardValue == hand[4].CardValue))
            {
                HandValue = 400 + (int)hand[2].CardValue;
                return true;
            }
            return false;
        }

        private bool IsTwoPair(Card[] hand)
        {
            if ((hand[0].CardValue == hand[1].CardValue && hand[2].CardValue == hand[3].CardValue) ||
                (hand[0].CardValue == hand[1].CardValue && hand[3].CardValue == hand[4].CardValue) ||
                (hand[1].CardValue == hand[2].CardValue && hand[3].CardValue == hand[4].CardValue))
            {
                HandValue = 300 + Math.Max((int)hand[1].CardValue, (int)hand[3].CardValue);
                return true;
            }
            return false;
        }

        private bool IsOnePair(Card[] hand)
        {
            if (hand[0].CardValue == hand[1].CardValue)
            {
                HandValue = 200 + (int)hand[1].CardValue;
                return true;
            }
            if (hand[1].CardValue == hand[2].CardValue)
            {
                HandValue = 200 + (int)hand[2].CardValue;
                return true;
            }
            if (hand[2].CardValue == hand[3].CardValue)
            {
                HandValue = 200 + (int)hand[3].CardValue;
                return true;
            }
            if (hand[3].CardValue == hand[4].CardValue)
            {
                HandValue = 200 + (int)hand[4].CardValue;
                return true;
            }
            return false;
        }
    }

    public enum Hand
    {
        HighCard,
        OnePair,
        TwoPair,
        ThreeOfKind,
        Straight,
        Flush,
        FullHouse,
        FourOfKind,
        StraightFlush,
        RoyalFlush
    }
}