using System.Collections.Generic;

namespace GPONMonitor.Models.ComplexStateTypes
{
    public class OpticalConnectionDeactivationReason
    {
        private int? value;
        public int? Value
        {
            get
            {
                return value;
            }
            set
            {
                ResponseDescription responseDescription;

                if (OpticalConnectionDeactivationReasonResponseDictionary.ContainsKey(value))
                {
                    OpticalConnectionDeactivationReasonResponseDictionary.TryGetValue(value, out responseDescription);
                }
                else
                {
                    OpticalConnectionDeactivationReasonResponseDictionary.TryGetValue(null, out responseDescription);
                }

                DescriptionEng = responseDescription.DescriptionEng;
                DescriptionPol = responseDescription.DescriptionPol;
                Severity = responseDescription.Severity;
                this.value = value;
            }
        }

        public string DescriptionEng { get; private set; }
        public string DescriptionPol { get; private set; }
        public SeverityLevel Severity { get; private set; }


        // ONU Deactivation Reason
        // none(1),
        // dgi(2),
        // losi(3),
        // lofi(4),
        // sfi(5),
        // sufi(6),
        // loai(7),
        // loami(8),
        // loki(9),
        // adminReset(10),
        // adminActiveChange(11),
        // adminOltConfiguration(12),
        // adminSlotRestart(13),
        // adminSlotRemove(14),
        // adminRogueOnuCandidate(15),
        // adminRogueOnu(16),
        // adminRogueOnuSelfDetectBlock(17),
        // adminTxOffOptic(18),
        // adminDeactivate(19),
        // adminOltDeactivate(20),
        // adminOmccDown(21),
        // adminSetRedundancy(22),
        // adminRemoveOnu(23),
        // los(100),
        // unknown(255)

        readonly Dictionary<int?, ResponseDescription> OpticalConnectionDeactivationReasonResponseDictionary = new Dictionary<int?, ResponseDescription>()
        {
            { 1, new ResponseDescription("none", "brak powodu", SeverityLevel.Danger) },
            { 2, new ResponseDescription("dying gasp (DGI)", "zanik zasilania (DGi)", SeverityLevel.Danger) },
            { 3, new ResponseDescription("loss of signal (LOSI)", "uszk. kabel magistralny (LOSI)", SeverityLevel.Success) },    // TO BE  FINISHED !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            { 4, new ResponseDescription("loss of frame (LOFI)", "uszk. kabel abonencki (LOFI)", SeverityLevel.Success) },      // TO BE  FINISHED !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            { 5, new ResponseDescription("signal fail (SFI)", "błędy transmisji (SFI)", SeverityLevel.Danger) },
            { 6, new ResponseDescription("start-up failure (SUFI)", "błędy inicjalizacji (SUFI)", SeverityLevel.Danger) },
            { 7, new ResponseDescription("loss of PLOAM (LOAI)", "utrata PLOAM (LOAI)", SeverityLevel.Success) },
            { 8, new ResponseDescription("loss of PLOAM (LOAMI)", "utrata PLOAM (LOAMI)", SeverityLevel.Success) },
            { 9, new ResponseDescription("loss of key sync (LOKI)", "utrata klucza (LOKI)", SeverityLevel.Danger) },
            { 10, new ResponseDescription("admin reset", "reset (administrator)", SeverityLevel.Success) },
            { 11, new ResponseDescription("admin active change", "zamina na aktywny (administrator)", SeverityLevel.Success) },
            { 12, new ResponseDescription("admin olt configuration", "konfiguracja olt (administrator)", SeverityLevel.Danger) },
            { 13, new ResponseDescription("admin slot restart", "restart slotu (administrator)", SeverityLevel.Danger) },
            { 14, new ResponseDescription("admin slot remove", "usunięcie slotu (administrator)", SeverityLevel.Success) },
            { 15, new ResponseDescription("admin rogue onu candidate", "uszkodzony modem (kandydujący)(administrator)", SeverityLevel.Success) },
            { 16, new ResponseDescription("admin rogue onu", "uszkodzony modem (administator)", SeverityLevel.Danger) },
            { 17, new ResponseDescription("admin rogue onu self detect block", "uszkodzony modem (automatyczna blokada) (administrator)", SeverityLevel.Danger) },
            { 18, new ResponseDescription("admin tx off optic", "laser wyłączony (administrator)", SeverityLevel.Success) },
            { 19, new ResponseDescription("admin deactivate", "deaktywacja (administrator)", SeverityLevel.Success) },
            { 20, new ResponseDescription("admin olt deactivate", "deaktyacja olt (administrator)", SeverityLevel.Danger) },
            { 21, new ResponseDescription("admin omcc down", "deaktywacja omcc (administrator)", SeverityLevel.Success) },
            { 22, new ResponseDescription("admin set redundancy", "ustawienie redundancji (administrator)", SeverityLevel.Success) },
            { 23, new ResponseDescription("admin remove onu", "usunięcie onu (administrator)", SeverityLevel.Danger) },
            { 100, new ResponseDescription("loss of signal (LOS)", "zanik sygału portu PON (LOS)", SeverityLevel.Danger) },
            { 255, new ResponseDescription("unknown", "brak odczytu", SeverityLevel.Success) }
        };
    }
}