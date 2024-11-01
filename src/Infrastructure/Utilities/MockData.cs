using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Utilities;

public static class MockData
{
    public static void GetMockData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RssFeed>()
            .HasData(new RssFeed
                {
                    Id = 1,
                    Url = "https://www.spiegel.de/politik/index.rss",
                    ChannelTitle = "Test Feed 1 Technologie Nachrichten und neue Entwicklungen"
                },
                new RssFeed
                {
                    Id = 2,
                    Url = "https://www.welt.de/feeds/section/wissenschaft.rss",
                    ChannelTitle = "Test Feed 2 Wissenschaft - WELT"
                });

        modelBuilder.Entity<RssFeedItem>()
            .HasData(
                new RssFeedItem
                {
                    Id = 1,
                    Title =
                        "Test Feed Item 1 Künstliche Intelligenz; Arbeitsminister Heil will KI \"auf die Straße bringen",
                    Description =
                        "Bundesarbeitsminister Heil will den Einsatz von Künstlicher Intelligenz (KI) in der Arbeitswelt vorantreiben. Arbeit werde sich verändern, aber nicht ausgehen, so Heil. In seinem Ministerium gebe es dazu bereits eine Denkfabrik.",
                    Link = "https://www.tagesschau.de/wirtschaft/digitales/spd-heil-ki-arbeitswelt-100.html",
                    PublishDate = new DateTime(2024, 4, 21, 10, 30, 0),
                    ImageUrl =
                        "https://images.tagesschau.de/image/8616ee35-2c95-46f8-b8b5-0f5da2c4b359/AAABiL5ZIxM/AAABkZLhkrw/16x9-1280/roboter-ki-100.jpg",
                    RssFeedId = 1
                },
                new RssFeedItem
                {
                    Id = 2,
                    Title = "Test Feed Item 2 IAA Mobility: Wann schaffen E-Autos den Durchbruch in Deutschland?",
                    Description =
                        "Test Feed Item 2 Bei der Messe IAA Mobility in München setzen die Autobauer besonders ihre E-Autos in Szene. Die Botschaft: Die Zukunft ist elektrisch. Doch wann kommt der angekündigte Durchbruch der E-Mobilität? Von Sebastian Hanisch.",
                    Link = "https://testlink2.com/",
                    PublishDate = new DateTime(2021, 4, 21, 10, 30, 0),
                    ImageUrl =
                        "https://images.tagesschau.de/image/ddd57310-c336-49f4-9351-067576add465/AAABimEJjZ0/AAABkZLhkrw/16x9-1280/iaa-200.jpg",
                    RssFeedId = 1
                },
                new RssFeedItem
                {
                    Id = 3,
                    Title = "Test Feed Item 3 Elektronikmesse IFA: Der Weg zum intelligenten Zuhause",
                    Description =
                        "Test Feed Item 3 Ob Staubsauger, Herd oder Lampen - Haushaltselektronik wird immer schlauer und macht das Zuhause zum \"Smart Home\". Die intelligente Vernetzung breitet sich aus und ist auch bei auf heute beginnenden IFA ein Thema. Von Ole Hilgert.",
                    Link = "https://www.tagesschau.de/wirtschaft/digitales/ifa-smart-home-102.html",
                    PublishDate = new DateTime(2021, 4, 21, 10, 30, 0),
                    ImageUrl =
                        "https://images.tagesschau.de/image/132ad0ce-c2a4-4b80-893f-d5236a39891f/AAABikx6AhE/AAABkZLhkrw/16x9-1280/ifa-2023-100.jpg",
                    RssFeedId = 1
                },
                new RssFeedItem
                {
                    Id = 4,
                    Title = "Test Feed Item 4 Zuchtmuscheln sollen wieder Riffe bilden",
                    Description =
                        "Test Feed Item 4 Ausgedehnte Riffe vor den europäischen Küsten – das war einmal. Aber nicht Korallen, sondern einheimische Austern waren die Basis. Diese existieren nur noch vereinzelt in Nordsee und Atlantik, aber das soll sich durch die Wiederansiedlung ändern",
                    Link =
                        "https://www.welt.de/wissenschaft/article253832928/Europaeische-Austern-Muscheln-aus-der-Zucht-sollen-neue-Riffe-bilden.html",
                    PublishDate = new DateTime(2021, 4, 21, 10, 30, 0),
                    ImageUrl =
                        "https://img.welt.de/img/wissenschaft/mobile253832924/1181622987-ci23x11-w780/France-Normandy-Manche-department-Cotentin-Saint-Vaast-la-Hougue-lle-de-Tat.jpg",
                    RssFeedId = 1
                },
                new RssFeedItem
                {
                    Id = 5,
                    Title = "Test Feed Item 5 Zwei von 50 Menschen – sind nur vier Prozent",
                    Description =
                        "Test Feed Item 5 Die Form ist entscheidend: Menschen nehmen Informationen unterschiedlich wahr – und zwar abhängig davon, auf welche Weise ihnen Zahlen präsentiert werden. Italienische Forscher haben untersucht, wie sich das zum Beispiel auf das Thema Migration auswirkt.",
                    Link =
                        "https://www.welt.de/wissenschaft/article253829764/Gefuehltes-Verhaeltnis-Warum-zwei-von-50-mehr-klingt-als-vier-Prozent.html",
                    PublishDate = new DateTime(2021, 4, 21, 10, 30, 0),
                    ImageUrl =
                        "https://img.welt.de/img/wissenschaft/mobile253829762/5701629567-ci23x11-w780/Beer-Fest-from-within-tent.jpg",
                    RssFeedId = 1
                },
                new RssFeedItem
                {
                    Id = 6,
                    Title =
                        "Test Feed Item 6 Warum sich Deutschland für Mäuse, Igel, Krähen und andere Tiere in Ost und West teilt",
                    Description =
                        "Test Feed Item 6 Eine Grenze, mitten durch Deutschland: Nicht nur die Nation war über Jahrzehnte geteilt, sondern auch die Tierwelt. Der fast 1400 Kilometer lange Todesstreifen ist heute ein „Grünes Band“. Trotzdem bleiben die Tiere im Osten und die im Westen unter sich – aus bemerkenswerten Gründen.",
                    Link =
                        "https://www.welt.de/wissenschaft/plus253801310/West-und-ostdeutsche-Natur-Warum-durch-die-Tierwelt-noch-immer-eine-Grenze-verlaeuft.html",
                    PublishDate = new DateTime(2021, 4, 21, 10, 30, 0),
                    ImageUrl =
                        "https://img.welt.de/img/wissenschaft/mobile253834946/0941626977-ci23x11-w780/DWO-WS-Teaser-Ost-West-Tiere-mku-cw-jpg.jpg",
                    RssFeedId = 1
                },
                new RssFeedItem
                {
                    Id = 7,
                    Title =
                        "Test Feed Item 7 Künstliche Intelligenz; Arbeitsminister Heil will KI \"auf die Straße bringen",
                    Description =
                        "Bundesarbeitsminister Heil will den Einsatz von Künstlicher Intelligenz (KI) in der Arbeitswelt vorantreiben. Arbeit werde sich verändern, aber nicht ausgehen, so Heil. In seinem Ministerium gebe es dazu bereits eine Denkfabrik.",
                    Link = "https://www.tagesschau.de/wirtschaft/digitales/spd-heil-ki-arbeitswelt-100.html",
                    PublishDate = new DateTime(2021, 4, 21, 10, 30, 0),
                    ImageUrl =
                        "https://images.tagesschau.de/image/8616ee35-2c95-46f8-b8b5-0f5da2c4b359/AAABiL5ZIxM/AAABkZLhkrw/16x9-1280/roboter-ki-100.jpg",
                    RssFeedId = 2
                },
                new RssFeedItem
                {
                    Id = 8,
                    Title = "Test Feed Item 8 IAA Mobility: Wann schaffen E-Autos den Durchbruch in Deutschland?",
                    Description =
                        "Test Feed Item 2 Bei der Messe IAA Mobility in München setzen die Autobauer besonders ihre E-Autos in Szene. Die Botschaft: Die Zukunft ist elektrisch. Doch wann kommt der angekündigte Durchbruch der E-Mobilität? Von Sebastian Hanisch.",
                    Link = "https://testlink2.com/",
                    PublishDate = new DateTime(2021, 4, 21, 10, 30, 0),
                    ImageUrl =
                        "https://images.tagesschau.de/image/ddd57310-c336-49f4-9351-067576add465/AAABimEJjZ0/AAABkZLhkrw/16x9-1280/iaa-200.jpg",
                    RssFeedId = 2
                },
                new RssFeedItem
                {
                    Id = 9,
                    Title = "Test Feed Item 9 Elektronikmesse IFA: Der Weg zum intelligenten Zuhause",
                    Description =
                        "Test Feed Item 3 Ob Staubsauger, Herd oder Lampen - Haushaltselektronik wird immer schlauer und macht das Zuhause zum \"Smart Home\". Die intelligente Vernetzung breitet sich aus und ist auch bei auf heute beginnenden IFA ein Thema. Von Ole Hilgert.",
                    Link = "https://www.tagesschau.de/wirtschaft/digitales/ifa-smart-home-102.html",
                    PublishDate = new DateTime(2021, 4, 21, 10, 30, 0),
                    ImageUrl =
                        "https://images.tagesschau.de/image/132ad0ce-c2a4-4b80-893f-d5236a39891f/AAABikx6AhE/AAABkZLhkrw/16x9-1280/ifa-2023-100.jpg",
                    RssFeedId = 2
                },
                new RssFeedItem
                {
                    Id = 10,
                    Title = "Test Feed Item 10 Zuchtmuscheln sollen wieder Riffe bilden",
                    Description =
                        "Test Feed Item 4 Ausgedehnte Riffe vor den europäischen Küsten – das war einmal. Aber nicht Korallen, sondern einheimische Austern waren die Basis. Diese existieren nur noch vereinzelt in Nordsee und Atlantik, aber das soll sich durch die Wiederansiedlung ändern",
                    Link =
                        "https://www.welt.de/wissenschaft/article253832928/Europaeische-Austern-Muscheln-aus-der-Zucht-sollen-neue-Riffe-bilden.html",
                    PublishDate = new DateTime(2021, 4, 21, 10, 30, 0),
                    ImageUrl =
                        "https://img.welt.de/img/wissenschaft/mobile253832924/1181622987-ci23x11-w780/France-Normandy-Manche-department-Cotentin-Saint-Vaast-la-Hougue-lle-de-Tat.jpg",
                    RssFeedId = 2
                },
                new RssFeedItem
                {
                    Id = 11,
                    Title = "Test Feed Item 11 Zwei von 50 Menschen – sind nur vier Prozent",
                    Description =
                        "Test Feed Item 5 Die Form ist entscheidend: Menschen nehmen Informationen unterschiedlich wahr – und zwar abhängig davon, auf welche Weise ihnen Zahlen präsentiert werden. Italienische Forscher haben untersucht, wie sich das zum Beispiel auf das Thema Migration auswirkt.",
                    Link =
                        "https://www.welt.de/wissenschaft/article253829764/Gefuehltes-Verhaeltnis-Warum-zwei-von-50-mehr-klingt-als-vier-Prozent.html",
                    PublishDate = new DateTime(2021, 4, 21, 10, 30, 0),
                    ImageUrl =
                        "https://img.welt.de/img/wissenschaft/mobile253829762/5701629567-ci23x11-w780/Beer-Fest-from-within-tent.jpg",
                    RssFeedId = 2
                },
                new RssFeedItem
                {
                    Id = 12,
                    Title =
                        "Test Feed Item 12 Warum sich Deutschland für Mäuse, Igel, Krähen und andere Tiere in Ost und West teilt",
                    Description =
                        "Test Feed Item 6 Eine Grenze, mitten durch Deutschland: Nicht nur die Nation war über Jahrzehnte geteilt, sondern auch die Tierwelt. Der fast 1400 Kilometer lange Todesstreifen ist heute ein „Grünes Band“. Trotzdem bleiben die Tiere im Osten und die im Westen unter sich – aus bemerkenswerten Gründen.",
                    Link =
                        "https://www.welt.de/wissenschaft/plus253801310/West-und-ostdeutsche-Natur-Warum-durch-die-Tierwelt-noch-immer-eine-Grenze-verlaeuft.html",
                    PublishDate = new DateTime(2021, 4, 21, 10, 30, 0),
                    ImageUrl =
                        "https://img.welt.de/img/wissenschaft/mobile253834946/0941626977-ci23x11-w780/DWO-WS-Teaser-Ost-West-Tiere-mku-cw-jpg.jpg",
                    RssFeedId = 2
                }
            );
    }
}