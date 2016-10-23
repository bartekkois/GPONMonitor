using System;

namespace GPONMonitor.Models.Onu
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

        //        echo "<tr>";
        //        echo "<td><p class=\"onuinfotabletext\">Czas pracy:</p></td>";
        //        echo "<td><p class=\"btn btn-block\"><strong>".secondsToTime($SnmpOnuSystemUptime)." "."</strong></p></td>";
        //        echo "</tr>";

        //        echo "<tr>";
        //        echo "<td><p class=\"onuinfotabletext\">Czas połączenia:</p></td>";
        //        echo "<td><p class=\"btn btn-block\"><strong>".secondsToTime($SnmpOnuUpTime)." "."</strong></p></td>";
        //        echo "</tr>";
        //    }
        //    else
        //    {
        //        //$SnmpOnuInactiveTime = CleanSnmpOutput(snmp2_get($OltIpAddress,"$OltSnmpCommunity", $SnmpOidOnuInactiveTime));

        //        //echo "<tr>";
        //        //echo "<td><p class=\"onuinfotabletext\">Czas od rozlaczenia modemu:</p></td>";
        //        //echo "<td><p class=\"CssTableONUCenterBold\">".secondsToTime($SnmpOnuInactiveTime)." "."</p></td>";
        //        //echo "</tr>";
        //    }
        //}
    }
}