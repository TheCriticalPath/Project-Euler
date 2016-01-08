using System;


namespace Problem_54
{
    public class Card : IComparable<Card>
    {
        public Card()
        {
        }
        public Card(CardTypes.Suits suit, CardTypes.CardType type) {
            Suit = suit;
            FaceValue = type;
            CardRank = ((int) suit * 13)+(int)type;
        }
        public CardTypes.Suits Suit { get; set; }
        public CardTypes.CardType FaceValue { get; set; }
        public int TrueValue {
            get { return ((int)FaceValue) + 2; }
            private set { }
        }
        public int CardRank {
            get { return ((int)Suit * 13) + (int)FaceValue; }
            private set { }
        }
        #region IComparable Members
        public int CompareTo(Card other) {
            return FaceValue - other.FaceValue;
        }
        #endregion
    }
}
