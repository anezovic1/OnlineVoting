﻿
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void TestPrikazaInformacijaOPrethodnomClanstvu1()
        {
            Kandidat k = new Kandidat("Bakir", "Izetbegović", "1");
            k.Opis = "Kandidat je bio član stranke SDA od 1.2.2000. do 5.6.2001., član stranke HDZ od 11.4.2002. do 15.12.2005., " +
                "član stranke NiP od 3.10.2010. do 7.7.2012., član stranke SDP od 2.2.2015. do 8.12.2021.";

            Assert.AreEqual(k.PrikazPrethodnogClanstva(), "Stranka: SDA, Članstvo od: 1.2.2000., Članstvo do: 5.6.2001.\n" +
                "Stranka: HDZ, Članstvo od: 11.4.2002., Članstvo do: 15.12.2005.\n" +
                "Stranka: NiP, Članstvo od: 3.10.2010., Članstvo do: 7.7.2012.\n" +
                "Stranka: SDP, Članstvo od: 2.2.2015., Članstvo do: 8.12.2021.\n");
        }

        [TestMethod]
        public void TestPrikazaInformacijaOPrethodnomClanstvu2()
        {
            Kandidat k = new Kandidat("Denis", "Zvizdić", "1");
            k.Opis = "Kandidat je bio član stranke SDA od 1.2.2000. do 5.6.2001., član stranke NiP od 3.10.2010. do 7.7.2012.";

            StringAssert.StartsWith(k.PrikazPrethodnogClanstva(), "Stranka: SDA, Članstvo od: 1.2.2000., Članstvo do: 5.6.2001.\n");
            StringAssert.Contains(k.PrikazPrethodnogClanstva(), "Stranka: NiP, Članstvo od: 3.10.2010., Članstvo do: 7.7.2012.\n");
        }

        [TestMethod]
        public void TestPraznogOpisa()
        {
            Kandidat k = new Kandidat("Bakir", "Izetbegović", "1");
            k.Opis = "";
            Assert.AreEqual(k.PrikazPrethodnogClanstva(), "Greška u unosu detaljnih informacija o kandidatu!");
        }

        [TestMethod]
        public void TestPocetkaOpisa()
        {
            Kandidat k = new Kandidat("Bakir", "Izetbegović", "1");
            k.Opis = "Kandidat je član stranke SDA od 1.2.2000. do 5.6.2001.";
            StringAssert.Contains(k.PrikazPrethodnogClanstva(), "Greška u unosu detaljnih informacija o kandidatu!");
        }


        [TestMethod]
        public void TestNemaNazivaStranke()
        {
            Kandidat k = new Kandidat("Bakir", "Izetbegović", "1");
             k.Opis = "Kandidat je bio član stranke od 1.2.2000. do 5.6.2001.";
             Assert.AreEqual(k.PrikazPrethodnogClanstva(), "Greška u unosu detaljnih informacija o kandidatu!");
        }

        [TestMethod]
        public void TestNazivaStranke()
        {
            Kandidat k = new Kandidat("Bakir", "Izetbegović", "1");
            k.Opis = "Kandidat je bio član stranke SDA123 od 1.2.2000. do 5.6.2001.";
            Assert.AreEqual(k.PrikazPrethodnogClanstva(), "Greška u unosu detaljnih informacija o kandidatu!");

            k.Opis = "Kandidat je bio član stranke SDA*** od 1.2.2000. do 5.6.2001.";
            Assert.AreEqual(k.PrikazPrethodnogClanstva(), "Greška u unosu detaljnih informacija o kandidatu!");
        }

        [TestMethod]
        public void TestUnosaDatuma()
        {
            Kandidat k = new Kandidat("Bakir", "Izetbegović", "1");
            k.Opis = "Kandidat je bio član stranke SDA od 1a.2.2000. do 5.6.2001.";
            Assert.AreEqual(k.PrikazPrethodnogClanstva(), "Greška u unosu detaljnih informacija o kandidatu!");

            k.Opis = "Kandidat je bio član stranke SDA od 1.2.2000. do 5.E6.2001.";
            Assert.AreEqual(k.PrikazPrethodnogClanstva(), "Greška u unosu detaljnih informacija o kandidatu!");
        }

        [TestMethod]
        public void TestUnosaDana()
        {
            Kandidat k = new Kandidat("Bakir", "Izetbegović", "1");
            k.Opis = "Kandidat je bio član stranke SDA od 45.2.2000. do 5.6.2001.";
            StringAssert.Contains(k.PrikazPrethodnogClanstva(), "Greška u unosu detaljnih informacija o kandidatu!");

            k.Opis = "Kandidat je bio član stranke SDA od 1.2.2000. do 55.6.2001.";
            Assert.AreEqual(k.PrikazPrethodnogClanstva(), "Greška u unosu detaljnih informacija o kandidatu!");
        }

        [TestMethod]
        public void TestUnosaMjeseca()
        {
            Kandidat k = new Kandidat("Bakir", "Izetbegović", "1");
            k.Opis = "Kandidat je bio član stranke SDA od 5.22.2000. do 5.6.2001.";
            Assert.AreEqual(k.PrikazPrethodnogClanstva(), "Greška u unosu detaljnih informacija o kandidatu!");

            k.Opis = "Kandidat je bio član stranke SDA od 1.2.2000. do 5.-2.2001.";
            StringAssert.Contains(k.PrikazPrethodnogClanstva(), "Greška u unosu detaljnih informacija o kandidatu!");
        }

        [TestMethod]
        public void TestUnosaGodine()
        {
            Kandidat k = new Kandidat("Bakir", "Izetbegović", "1");

            k.Opis = "Kandidat je bio član stranke SDA od 5.2.-2000. do 5.6.2001.";
            Assert.AreEqual(k.PrikazPrethodnogClanstva(), "Greška u unosu detaljnih informacija o kandidatu!");

            k.Opis = "Kandidat je bio član stranke SDA od 5.2.2000. do 5.6.2030.";
            StringAssert.Contains(k.PrikazPrethodnogClanstva(), "Greška u unosu detaljnih informacija o kandidatu!");

            k.Opis = "Kandidat je bio član stranke SDA od 1.2.2000. do 5.2.2025.";
            Assert.AreEqual(k.PrikazPrethodnogClanstva(), "Greška u unosu detaljnih informacija o kandidatu!");

            k.Opis = "Kandidat je bio član stranke SDA od 1.2.2040. do 5.2.2020.";
            StringAssert.Contains(k.PrikazPrethodnogClanstva(), "Greška u unosu detaljnih informacija o kandidatu!");
        }

        [TestMethod]
        public void TestDatumaPocetkaIKraja()
        {
            Kandidat k = new Kandidat("Denis", "Zvizdić", "1");

            k.Opis = "Kandidat je bio član stranke SDA od 5.2.2005. do 5.6.2001.";
            Assert.AreEqual(k.PrikazPrethodnogClanstva(), "Greška u unosu detaljnih informacija o kandidatu!");

            k.Opis = "Kandidat je bio član stranke SDA od 1.11.2000. do 5.2.2000.";
            Assert.AreEqual(k.PrikazPrethodnogClanstva(), "Greška u unosu detaljnih informacija o kandidatu!");

            k.Opis = "Kandidat je bio član stranke SDA od 9.11.2000. do 5.11.2000.";
            StringAssert.Contains(k.PrikazPrethodnogClanstva(), "Greška u unosu detaljnih informacija o kandidatu!");
        }

    }
}