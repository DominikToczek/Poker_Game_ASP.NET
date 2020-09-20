using Cards;

namespace FiveCardsPoker
{
    public interface ILayout
    {
        Hand CardsLayout(Card[] hand);
    }
}
