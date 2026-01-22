using System;
using System.Collections.Generic;
using System.Text;

namespace SystemBiletowLotniczych
{
    /// <summary>
    /// Interfejs definiujący dodatkowe usługi dla biletów,
    /// takie jak posiłki czy inne opcje płatne.
    /// </summary>
    public interface IUslugowy
    {
        /// <summary>
        /// Cena usług dodatkowych przypisana do biletu.
        /// </summary>
        double CenaUslugDodatkowych { get; set; }

        /// <summary>
        /// Dodaje dodatkowy posiłek do biletu.
        /// </summary>
        /// <param name="nazwaPosilku">Nazwa dodawanego posiłku.</param>
        void DodajPosilek(string nazwaPosilku);

        /// <summary>
        /// Wyświetla listę wszystkich usług dodatkowych przypisanych do biletu.
        /// </summary>
        void WyswietlUslugi();
    }
}
