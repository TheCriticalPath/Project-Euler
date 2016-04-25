using System;
using System.Collections;
using System.Collections.Generic;

namespace Problem_54
{
    public class Hand:IList<Card>
    {
        public Hand() { }
        private string Player;
        List<Card> Cards;

        public int Count
        {
            get
            {
                return ((IList<Card>)Cards).Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return ((IList<Card>)Cards).IsReadOnly;
            }
        }

        public Card this[int index]
        {
            get
            {
                return ((IList<Card>)Cards)[index];
            }

            set
            {
                ((IList<Card>)Cards)[index] = value;
            }
        }

        public Hand(string type) {
            Cards = new List<Card>();            
            Player = type;
        }
        public void Add(Card c) {
            Cards.Add(c);
        }
        public void Sort() {
            Cards.Sort();
        }

        public void Remove(Card p)
        {
            Cards.Remove(p);
        }

        public int IndexOf(Card item)
        {
            return ((IList<Card>)Cards).IndexOf(item);
        }

        public void Insert(int index, Card item)
        {
            ((IList<Card>)Cards).Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            ((IList<Card>)Cards).RemoveAt(index);
        }

        public void Clear()
        {
            ((IList<Card>)Cards).Clear();
        }

        public bool Contains(Card item)
        {
            return ((IList<Card>)Cards).Contains(item);
        }

        public void CopyTo(Card[] array, int arrayIndex)
        {
            ((IList<Card>)Cards).CopyTo(array, arrayIndex);
        }

        bool ICollection<Card>.Remove(Card item)
        {
            return ((IList<Card>)Cards).Remove(item);
        }

        public IEnumerator<Card> GetEnumerator()
        {
            return ((IList<Card>)Cards).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IList<Card>)Cards).GetEnumerator();
        }
    }
}