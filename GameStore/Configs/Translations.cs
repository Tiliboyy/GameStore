using Exiled.API.Interfaces;
using System.ComponentModel;
using System.Security.Cryptography;

namespace GameStore
{
    public class Translation : ITranslation
    {


        public string Currencyname { get; set; } = "Daylight Bits.";

        public string Givemoneytext { get; set; } = "Du hast (moneyamount) Daylight Bits erhalten.";
        public string Boughtitem { get; set; } = "Du hast (itemname) für (itemprice) Daylight Bits gekauft";
        public string Maxamountreached { get; set; } = "Du hast schon genug davon gekauft.";

        public string Cantafford { get; set; } = "Du kannst dir dieses Item nicht leisten.";
        public string WrongeRole { get; set; } = "Deine Rolle erlaubt es dir nicht dieses Item zu kaufen.";

        public string Fullinventory { get; set; } = "Dein Inventar ist voll";

        public string Dntmessage { get; set; } = "Du hast Do not Track aktiviert. Deakiviere DNT um den GameStore verwenden zu können";

        public string Disabledstore { get; set; } = "Der GameStore ist momentan deaktiviert";

        public string Roundnotstarted { get; set; } = "Die Runde hat noch nicht gestartet";

        public string Categorydoesnotexist { get; set; } = "Diese Kategorie existiert nicht!";

        public string balmessage { get; set; } = "Du hast (balance) Daylight Bits";








    }
}