using System;
using System.Collections.Generic;
using System.Text;

namespace PlayingCardsAPI
{
    /// <summary>
    /// Класс описывающий коллекцию колод карт.
    /// </summary>
    public class DeckCollection
    {
        private readonly IList<Deck> decks;

        /// <summary>
        /// Инициализирует коллекцию колод карт.
        /// </summary>
        public DeckCollection()
        {
            decks = new List<Deck>();
        }

        /// <summary>
        /// Индексатор, возвращающий колоду по её названию.
        /// </summary>
        /// <param name="name">Название колоды.</param>
        /// <returns>Колода.</returns>
        /// <exception cref="Exception">Некорректное название колоды.</exception>
        public Deck this[string name]
        {
            get
            {
                foreach (var deck in decks)
                {
                    if (deck.Name == name) return deck;
                }
                throw new Exception("Unknown name.");
            }
        }

        /// <summary>
        /// Добавление колоды карт.
        /// </summary>
        /// <param name="name">Название колоды.</param>
        public void AddDeck(string name) => decks.Add(new Deck(name));

        /// <summary>
        /// Удаление колоды по её названию.
        /// </summary>
        /// <param name="name">Название колоды.</param>
        /// <returns>Возвращает 0, если колода была удалена, в другом случае -1.</returns>
        public int RemoveDeck(string name)
        {
            foreach (var deck in decks)
            {
                if (String.Compare(deck.Name, name) == 0)
                {
                    decks.Remove(deck);
                    return 0;
                }
            }

            return -1;
        }

        /// <summary>
        /// Возвращает названия всех колод.
        /// </summary>
        public IEnumerable<string> GetNamesDecks()
        {
            foreach (var deck in decks)
                yield return deck.Name;
        }

        /// <summary>
        /// Перетасовывает колоду по её названию. 
        /// </summary>
        /// <param name="name">Название колоды.</param>
        /// <returns>Возвращает 0, если колода успешно перетасовалась, в другом случае -1.</returns>
        public int DeckShuffling(string name)
        {
            foreach (var deck in decks)
            {
                if (String.Compare(deck.Name, name) == 0)
                {
                    deck.Shuffle();
                    return 0;
                }
            }

            return -1;
        }
    }

    /// <summary>
    /// Класс описывающий колоду карт.
    /// </summary>
    public class Deck
    {
        public readonly string Name;

        private readonly IList<Card> cards;

        /// <summary>
        /// Инициализирует и заполняет новую колоду игральных карт.
        /// </summary>
        public Deck(string name)
        {
            this.Name = name;
            cards = new List<Card>();

            for (int i = 0; i < Enum.GetNames(typeof(Ranks)).Length; i++)
                for (int j = 0; j < Enum.GetNames(typeof(Suits)).Length; j++)
                    cards.Add(new Card((Ranks)i, (Suits)j));
        }

        /// <summary>
        /// Перетасовка колоды.
        /// </summary>
        public void Shuffle()
        {
            int j;
            Card temp;
            Random rnd = new Random((int)DateTime.UtcNow.Ticks);

            for (int i = 0; i < cards.Count; i++)
            {
                j = rnd.Next(cards.Count);
                temp = (Card)cards[i].Clone();
                cards[i] = (Card)cards[j].Clone();
                cards[j] = (Card)temp.Clone();
            }
        }

        public override string ToString()
        {
            var cardsBuilder = new StringBuilder();

            foreach (var card in cards)
                cardsBuilder.AppendLine(card.ToString());

            return cardsBuilder.ToString(); 
        }
    }

    /// <summary>
    /// Класс описывающий игральную карту.
    /// </summary>
    internal class Card : ICloneable
    {
        private readonly Ranks rank;

        private readonly Suits suit;

        /// <summary>
        /// Инициализирует новую карту.
        /// </summary>
        /// <param name="rank">Значение карты.</param>
        /// <param name="suit">Масть карты.</param>
        public Card(Ranks rank, Suits suit)
        {
            this.rank = rank;
            this.suit = suit;
        }

        public object Clone() => MemberwiseClone();

        public override string ToString() => rank + " " + suit;
    }
    
    /// <summary>
    /// Значения карты.
    /// </summary>
    internal enum Ranks
    {
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
        Ace
    }

    /// <summary>
    /// Масти.
    /// </summary>
    internal enum Suits
    {
        Diamonds,   // Бубны.
        Hearts,     // Черви.
        Clubs,      // Крести.
        Spades      // Пики.
    }
}
