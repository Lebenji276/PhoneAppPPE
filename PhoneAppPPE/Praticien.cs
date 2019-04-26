namespace PhoneAppPPE
{
    internal class Praticien
    {
        private string _MinCP;
        public string pra_num { get; set; }
        public string pra_nom { get; set; }
        public string pra_prenom { get; set; }
        public string pra_adresse { get; set; }
        public string pra_cp { get; set; }
        public string pra_ville { get; set; }
        public string pra_coefnotoriete { get; set; }
        public string typ_lieu { get; set; }
        public string typ_libelle { get; set; }
        public string MinCP
        {
            get
            {
                string PremierCp = pra_cp[0] + "";
                string DeuxCP = pra_cp[1] + "";
                return PremierCp + DeuxCP;
            }
            set => _MinCP = value;
        }
        public override string ToString() => $"{pra_nom.ToUpper()} {pra_prenom}\n{pra_adresse}\n{pra_cp}\t{pra_ville}\nCoefficient notoriété : {pra_coefnotoriete}\nLieu : {typ_lieu}\nLibelle : {typ_libelle}";
    }
}