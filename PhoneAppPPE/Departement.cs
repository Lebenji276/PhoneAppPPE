namespace PhoneAppPPE
{
    internal class Departement
    {
        public string nom { get; set; }
        public string code { get; set; }
        public string codeRegion { get; set; }

        public override string ToString() => $"Département : {nom} ({code})";

    }
}