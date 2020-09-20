using Cards;

namespace FiveCardsPoker
{
    public interface IDeal
    {
        Card[] GetPlayerHand();
        Card[] GetComputerHand();
        Card[] GetSortedPlayerHand();
        Card[] GetSortedComputerHand();
        void DealCards();
        void ChangeCard(int cardNumber);
        void SortCards();
    }
}
