using Exiled.API.Interfaces;
using System.ComponentModel;

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







    }
}