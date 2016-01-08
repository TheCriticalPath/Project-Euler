using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginInterface;
using System.Windows.Forms;
using System.ComponentModel.Composition;
using Helpers;
using System.Numerics;

/*
 * http://siralansdailyramble.blogspot.com/2009/04/project-euler-54.html
 */
namespace Problem_54
{
    [Export(typeof(IEulerPlugin))]
    public class EulerPlugin : IEulerPlugin
    {
        public bool IsAsync { get { return true; } }
        public bool ImplementsGetInput { get { return true; } }

        public int ID { get { return 54; } }
        public string Name
        {
            get { return string.Format("Problem {0}: {1}", ID, Title); }
        }
        public string Title { get { return string.Format("Poker Hands", ID); } }
        public string Description
        {
            get
            {
                return @"In the card game poker, a hand consists of five cards and are ranked, from lowest to highest, in the following way:

High Card: Highest value card.
One Pair: Two cards of the same value.
Two Pairs: Two different pairs.
Three of a Kind: Three cards of the same value.
Straight: All cards are consecutive values.
Flush: All cards of the same suit.
Full House: Three of a kind and a pair.
Four of a Kind: Four cards of the same value.
Straight Flush: All cards are consecutive values of same suit.
Royal Flush: Ten, Jack, Queen, King, Ace, in same suit.
The cards are valued in the order:
2, 3, 4, 5, 6, 7, 8, 9, 10, Jack, Queen, King, Ace.

If two players have the same ranked hands then the rank made up of the highest value wins; for example, a pair of eights beats a pair of fives (see example 1 below). But if two ranks tie, for example, both players have a pair of queens, then highest cards in each hand are compared (see example 4 below); if the highest cards tie then the next highest cards are compared, and so on.";
            }
        }
        public EulerPlugin() { }

        private long GetLimit(string strModifier = "", string defaultLimit = "1000")
        {
            long lngLimit = 0;
            string strLimit = defaultLimit;

            while (lngLimit < 1)
            {
                Helpers.InputHelper.Show(Name, string.Format("Enter {0} Limit", strModifier), ref strLimit);
                if (!Int64.TryParse(strLimit, out lngLimit))
                {
                    lngLimit = 0;
                }

            }
            return lngLimit;
        }
        public void PerformGetInput(IEulerPluginContext context)
        {

        }

        public Task<IEulerPluginContext> PerformActionAsync(IEulerPluginContext context)
        {
            return Task.Factory.StartNew(() =>
           {
               // need a more elegant solution.
               DateTime dtStart, dtEnd;
               dtStart = DateTime.Now;
               Task<String> s = BruteForceAsync();
               dtEnd = DateTime.Now;
               context.strResultLongText = s.Result;
               context.spnDuration = dtEnd.Subtract(dtStart);
               return context;
           });
        }

        public IEulerPluginContext PerformAction(IEulerPluginContext context)
        {
            context.strResultLongText = "";
            return context;
        }
        async Task<string> BruteForceAsync()
        {

            return BruteForce();

        }

        private string BruteForce()
        {
            int count = 0;
            List<string> Poker = Helpers.FileHelper.GetFile("Input\\p054_poker.txt");
            int i = 0;
            foreach (string p in Poker)
            {
                i++;
                string[] cards1 = p.Substring(0, 14).Split(' ');
                string[] cards2 = p.Substring(15, 14).Split(' ');
                Hand Player1 = GeneratePokerHand(cards1);
                Hand Player2 = GeneratePokerHand(cards2);
                double p1score = EvaluatePokerHand(Player1);
                double p2score = EvaluatePokerHand(Player2);
                if (p1score > p2score)
                {
                    count++;
                }

            }
            return $"Player 1 won {count} hands.";
        }

        private Hand GeneratePokerHand(string[] cards)
        {
            Hand hand = new Hand("player");
            foreach (string s in cards)
            {
                char[] c = s.ToCharArray();
                Card card = new Card();
                hand.Add(card);
                switch (c[0])
                {
                    case '2':
                        card.FaceValue = 0;
                        break;
                    case '3':
                        card.FaceValue = (CardTypes.CardType)1;
                        break;
                    case '4':
                        card.FaceValue = (CardTypes.CardType)2;
                        break;
                    case '5':
                        card.FaceValue = (CardTypes.CardType)3;
                        break;
                    case '6':
                        card.FaceValue = (CardTypes.CardType)4;
                        break;
                    case '7':
                        card.FaceValue = (CardTypes.CardType)5;
                        break;
                    case '8':
                        card.FaceValue = (CardTypes.CardType)6;
                        break;
                    case '9':
                        card.FaceValue = (CardTypes.CardType)7;
                        break;
                    case 'T':
                        card.FaceValue = (CardTypes.CardType)8;
                        break;
                    case 'J':
                        card.FaceValue = (CardTypes.CardType)9;
                        break;
                    case 'Q':
                        card.FaceValue = (CardTypes.CardType)10;
                        break;
                    case 'K':
                        card.FaceValue = (CardTypes.CardType)11;
                        break;
                    case 'A':
                        card.FaceValue = (CardTypes.CardType)12;
                        break;
                    default:
                        break;
                }
                switch (c[1])
                {
                    case 'C':
                        card.Suit = 0;
                        break;
                    case 'D':
                        card.Suit = (CardTypes.Suits)1;
                        break;
                    case 'H':
                        card.Suit = (CardTypes.Suits)2;
                        break;
                    case 'S':
                        card.Suit = (CardTypes.Suits)3;
                        break;
                }
            }
            hand.Sort();
            return hand;
        }
        public static double EvaluatePokerHand(Hand hand) {
            double points = 0;
            int flush = 1, straight = 1;
            //flushes and straights
            for (int i = 1; i < hand.Count; i++)
            {
                if (hand[0].Suit == hand[i].Suit)
                    flush++;
                if (hand[i].FaceValue == hand[0].FaceValue + i)
                    straight++;
            }
            if (straight == 5)
            {
                points = flush == 5
                             ? 450 + hand[4].TrueValue
                             : 250 + hand[4].TrueValue;
                return points;
            }
            if (flush == 5)
                return 300 + EvaluateCards(hand);

            int numberOfPairs = 0, numberOfTres = 0, cardValue = 0;
            //check for pairs/threes/fours
            for (int i = 0; i < hand.Count - 1; i++)
            {
                int sameValue = 1;
                for (int j = i + 1; j < hand.Count; j++)
                {
                    if (hand[i].FaceValue == hand[j].FaceValue)
                        sameValue++;
                    if (sameValue == 4)
                        return 400 + hand[j].TrueValue;
                }
                if (sameValue == 3)
                {
                    numberOfTres++;
                    numberOfPairs--;
                    cardValue = hand[i].TrueValue;
                }
                if (sameValue == 2)
                    numberOfPairs++;
            }
            //two pairs
            if (numberOfPairs == 2)
            {
                int pairNumber = 2;
                int i = hand.Count - 1;
                while (hand.Count > 1)
                {
                    if (hand[i - 1].FaceValue == hand[i].FaceValue)
                    {
                        if (pairNumber == 2)
                        {
                            points = 13 * (int)hand[i].FaceValue;
                            pairNumber--;
                        }
                        else
                            points += hand[i].TrueValue;
                        hand.Remove(hand[i]);
                        hand.Remove(hand[i - 1]);
                        i -= 2;
                    }
                    else
                        i--;
                }
                return points + EvaluateCards(hand);
            }
            //pair
            if (numberOfPairs == 1 && numberOfTres == 0)
            {
                int i = hand.Count - 1;
                while (hand.Count > 3)
                {
                    if (hand[i - 1].FaceValue == hand[i].FaceValue)
                    {
                        points += hand[i].TrueValue;
                        hand.Remove(hand[i]);
                        hand.Remove(hand[i - 1]);
                        return points + EvaluateCards(hand);
                    }
                    i--;
                }
            }
            //3 of a kind or house
            if (numberOfTres == 1)
            {
                if (numberOfPairs == 1)
                    return 350 + cardValue;
                return 200 + cardValue;
            }
            //high card
            return EvaluateCards(hand);
        }
        public static double EvaluateCards(IList<Card> cards)
        {
            double points = 0;
            for (int i = cards.Count - 1; i >= 0; i--)
                points += (int)cards[i].FaceValue * ((Math.Pow(14, i) / 1000000));
            return points;
        }

    }
}
