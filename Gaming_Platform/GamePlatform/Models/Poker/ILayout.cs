namespace GamePlatform.Models
{
    public interface ILayout
    {
        Hand CardsLayout(Card[] hand);
        int GetHandValue();
    }
}
