using Newtonsoft.Json;
using System;

namespace GPONMonitor.Models.ComplexStateTypes
{
    public class SystemUptime
    {
        public int? Value
        {
            get
            {
                return Value;
            }
            set
            {
                if (value != null)
                {
                    TimeSpan timeSpan = TimeSpan.FromSeconds(Convert.ToDouble(value));
                    string displayTimeSpan = timeSpan.ToString("dd, hh:mm:tt");

                    DescriptionEng = displayTimeSpan;
                    DescriptionPol = displayTimeSpan;
                    Severity = SeverityLevel.Default;

                    Value = value;
                }
                else
                {
                    DescriptionEng = null;
                    DescriptionPol = null;
                    Severity = SeverityLevel.Unknown;

                    Value = null;
                }
            }
        }

        public string DescriptionEng { get; private set; }
        public string DescriptionPol { get; private set; }
        public SeverityLevel Severity { get; private set; }

        [JsonIgnore]
        public string SnmpOID { get; private set; } = "1.3.6.1.4.1.6296.101.23.3.1.1.61";              // ONU system uptime (followed by OnuPortId and OnuId)

        const string snmpOIDOnuTimeUpdate1 = "1.3.6.1.4.1.6296.101.23.3.2.1";                         // ONU time update (set 21)
        const string snmpOIDOnuTimeUpdate2 = "1.3.6.1.4.1.6296.101.23.3.2.6";
        const string snmpOIDOnuTimeUpdate3 = "1.3.6.1.4.1.6296.101.23.3.2.7";
        const string snmpOIDOnuTimeUpdate4 = "1.3.6.1.4.1.6296.101.23.3.2.3";


        // ONT Uptime
        //if ((strpos($SnmpSystemDescription,'NOS 5.08') == true) || (strpos($SnmpSystemDescription,'NOS 6.02') == true) || (strpos($SnmpSystemDescription,'NOS 6.07p1') == true))
        //{
        //    //snmp2_set($OltIpAddress,"$OltSnmpCommunity",$SnmpOidOnuTimeUpdate1, "i", 21);
        //    //snmp2_set($OltIpAddress,"$OltSnmpCommunity",$SnmpOidOnuTimeUpdate2, "i", $OltId);
        //    //snmp2_set($OltIpAddress,"$OltSnmpCommunity",$SnmpOidOnuTimeUpdate3, "i", $OnuId);
        //    //snmp2_set($OltIpAddress,"$OltSnmpCommunity",$SnmpOidOnuTimeUpdate4, "i", 0);

        //    if($SnmpOnuLinkStatus == 2)
        //    {
        //        $SnmpOnuSystemUptime = CleanSnmpOutput(snmp2_get($OltIpAddress,"$OltSnmpCommunity",$SnmpOidOnuSystemUptime));
        //        $SnmpOnuUpTime = CleanSnmpOutput(snmp2_get($OltIpAddress,"$OltSnmpCommunity",$SnmpOidOnuUpTime));

        //        echo "<td><p class=\"onuinfotabletext\">Czas pracy:</p></td>";
        //        echo "<td><p class=\"btn btn-block\"><strong>".secondsToTime($SnmpOnuSystemUptime)." "."</strong></p></td>";

        //        echo "<td><p class=\"onuinfotabletext\">Czas połączenia:</p></td>";
        //        echo "<td><p class=\"btn btn-block\"><strong>".secondsToTime($SnmpOnuUpTime)." "."</strong></p></td>";
        //    }
        //    else
        //    {
        //        //$SnmpOnuInactiveTime = CleanSnmpOutput(snmp2_get($OltIpAddress,"$OltSnmpCommunity", $SnmpOidOnuInactiveTime));

        //        //echo "<td><p class=\"onuinfotabletext\">Czas od rozlaczenia modemu:</p></td>";
        //        //echo "<td><p class=\"CssTableONUCenterBold\">".secondsToTime($SnmpOnuInactiveTime)." "."</p></td>";
        //    }
        //}
    }
}