using FluentAssertions;
using GPONMonitor.Controllers;
using GPONMonitor.Models;
using GPONMonitor.Models.ComplexStateTypes;
using GPONMonitor.Models.Configuration;
using GPONMonitor.Models.Onu;
using GPONMonitor.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace tests
{
    public class OnuApiControllerUnitTests
    {
        private static void ArrangeOltConfigurationsWithOnusState(out Mock<IOptions<DevicesConfiguration>> devicesConfigurationMock, out Mock<IDataService> dataServiceMock)
        {
            // Arrange
            devicesConfigurationMock = new Mock<IOptions<DevicesConfiguration>>();
            devicesConfigurationMock.Setup(x => x.Value).Returns(new DevicesConfiguration()
            {
                Devices = new List<OltConfiguration>()
                {
                    new OltConfiguration{ Id = 0, Name ="GPON OLT A", IpAddress="10.1.1.21",SnmpPort ="161", SnmpVersion="2", SnmpCommunity="public", SnmpTimeout="3000", IpHostWebManagementPort="80"},
                    new OltConfiguration{ Id = 1, Name ="GPON OLT B", IpAddress="10.1.1.11",SnmpPort ="161", SnmpVersion="2", SnmpCommunity="public", SnmpTimeout="3000", IpHostWebManagementPort="80"},
                    new OltConfiguration{ Id = 2, Name ="GPON OLT C", IpAddress="10.2.1.11",SnmpPort ="161", SnmpVersion="2", SnmpCommunity="public", SnmpTimeout="3000", IpHostWebManagementPort="80"}
                }
            });

            dataServiceMock = new Mock<IDataService>();
            dataServiceMock.Setup(x => x.GetOnuDescriptionListAsync(0)).ReturnsAsync(() => new List<OnuShortDescription>()
            {
                new OnuShortDescription(1, 1, "ONU_B", "DSNW23456789", "inactive", "-180"),
                new OnuShortDescription(1, 2, "ONU_A", "DSNWcbd38d4e", "active", "-208"),
                new OnuShortDescription(1, 3, "ONU_C", "DSNW34567890", "active", "-154"),
            });

            dataServiceMock.Setup(x => x.GetOnuStateAsync(0, 1, 2)).ReturnsAsync(() => new H645GOnu()
            {
                OltId = 0,
                OltPortId = 1,
                OltOnuId = 1,
                ModelType = new ComplexStringType("H645G", "H645G", SeverityLevel.Default),
                DescriptionName = new ComplexStringType("ONU_A", "ONU_A", SeverityLevel.Default),
                GponSerialNumber = new ComplexStringType("DSNWcbd38d4e", "DSNWcbd38d4e", SeverityLevel.Default),
                GponProfile = new ComplexStringType("ONU-PROFILE-INT-VLAN100", "ONU-PROFILE-INT-VLAN100", SeverityLevel.Default),
                FirmwareVersion = new ComplexStringType("2.45", "2.45", SeverityLevel.Default),

                OpticalConnectionState = new ComplexIntType(2, "aktywny", SeverityLevel.Success),
                OpticalConnectionDeactivationReason = new ComplexIntType(1, "zanik zasilania (DGi)", SeverityLevel.Info),
                OpticalPowerReceived = new ComplexStringType("-215", "-21,5 dBm", SeverityLevel.Success),
                OpticalCableDistance = new ComplexIntType(1115, "1115 m", SeverityLevel.Default),

                OpticalConnectionUptime = new ComplexIntType(18844321, "218d 02:32:01 (12:50:02 1.01)", SeverityLevel.Default),
                OpticalConnectionInactiveTime = new ComplexIntType(0, "0d 00:00:00", SeverityLevel.Default),
                SystemUptime = new ComplexIntType(18844372, "218d 02:32:52 (12:50:53 1.01)", SeverityLevel.Default),

                BlockStatus = new ComplexIntType(255, "brak blokady", SeverityLevel.Success),
                BlockReason = new ComplexIntType(255, "brak blokady", SeverityLevel.Success),

                EthernetPort1State = new ComplexIntType(1, "up", SeverityLevel.Success),
                EthernetPort1Speed = new ComplexIntType(3, "1000 Mb/s", SeverityLevel.Success)
            });

            dataServiceMock.Setup(x => x.GetOnuStateAsync(0, 1, 1)).Throws(new Exception());
        }


        [Fact]
        public async Task GetH645GOnuStateByOltPortIdAndOnuIdAsync_ShouldReturnOnuStateIfOnuExists_Async()
        {
            // Arrange
            ArrangeOltConfigurationsWithOnusState(out Mock<IOptions<DevicesConfiguration>> devicesConfigurationMock, out Mock<IDataService> dataServiceMock);

            var controller = new OnuApiController(devicesConfigurationMock.Object, dataServiceMock.Object);

            // Act
            var result = await controller.GetOnuStateByOltPortIdAndOnuIdAsync(0, 1, 2);

            // Assert
            var okResult = result.Should().BeOfType<JsonResult>().Subject;
            var onu = okResult.Value.Should().BeAssignableTo<H645GOnu>().Subject;

            onu.OltId.Should().Be(0);
            onu.OltPortId.Should().Be(1);
            onu.OltOnuId.Should().Be(1);

            onu.ModelType.Should().BeEquivalentTo(new ComplexStringType("H645G", "H645G", SeverityLevel.Default));
            onu.DescriptionName.Should().BeEquivalentTo(new ComplexStringType("ONU_A", "ONU_A", SeverityLevel.Default));
            onu.GponSerialNumber.Should().BeEquivalentTo(new ComplexStringType("DSNWcbd38d4e", "DSNWcbd38d4e", SeverityLevel.Default));
            onu.GponProfile.Should().BeEquivalentTo(new ComplexStringType("ONU-PROFILE-INT-VLAN100", "ONU-PROFILE-INT-VLAN100", SeverityLevel.Default));
            onu.FirmwareVersion.Should().BeEquivalentTo(new ComplexStringType("2.45", "2.45", SeverityLevel.Default));

            onu.OpticalConnectionState.Should().BeEquivalentTo(new ComplexIntType(2, "aktywny", SeverityLevel.Success));
            onu.OpticalConnectionDeactivationReason.Should().BeEquivalentTo(new ComplexIntType(1, "zanik zasilania (DGi)", SeverityLevel.Info));
            onu.OpticalPowerReceived.Should().BeEquivalentTo(new ComplexStringType("-215", "-21,5 dBm", SeverityLevel.Success));
            onu.OpticalCableDistance.Should().BeEquivalentTo(new ComplexIntType(1115, "1115 m", SeverityLevel.Default));

            onu.OpticalConnectionUptime.Should().BeEquivalentTo(new ComplexIntType(18844321, "218d 02:32:01 (12:50:02 1.01)", SeverityLevel.Default));
            onu.OpticalConnectionInactiveTime.Should().BeEquivalentTo(new ComplexIntType(0, "0d 00:00:00", SeverityLevel.Default));
            onu.SystemUptime.Should().BeEquivalentTo(new ComplexIntType(18844372, "218d 02:32:52 (12:50:53 1.01)", SeverityLevel.Default));

            onu.BlockStatus.Should().BeEquivalentTo(new ComplexIntType(255, "brak blokady", SeverityLevel.Success));
            onu.BlockReason.Should().BeEquivalentTo(new ComplexIntType(255, "brak blokady", SeverityLevel.Success));

            onu.EthernetPort1State.Should().BeEquivalentTo(new ComplexIntType(1, "up", SeverityLevel.Success));
            onu.EthernetPort1Speed.Should().BeEquivalentTo(new ComplexIntType(3, "1000 Mb/s", SeverityLevel.Success));
        }


        [Fact]
        public async Task GetH645GOnuStateByOltPortIdAndOnuIdAsync_ShouldReturnNotFoundIfOnuDoesntExists_Async()
        {
            // Arrange
            ArrangeOltConfigurationsWithOnusState(out Mock<IOptions<DevicesConfiguration>> devicesConfigurationMock, out Mock<IDataService> dataServiceMock);

            var controller = new OnuApiController(devicesConfigurationMock.Object, dataServiceMock.Object);

            // Act
            var result = await controller.GetOnuStateByOltPortIdAndOnuIdAsync(0, 1, 1);

            // Assert
            var okResult = result.Should().BeOfType<NotFoundObjectResult>();
        }


        [Fact]
        public async Task GetH645GOnuStateByOnuSerialNumberAsync_ShouldReturnOnuStateIfOnuExists_Async()
        {
            // Arrange
            ArrangeOltConfigurationsWithOnusState(out Mock<IOptions<DevicesConfiguration>> devicesConfigurationMock, out Mock<IDataService> dataServiceMock);

            var controller = new OnuApiController(devicesConfigurationMock.Object, dataServiceMock.Object);

            // Act
            var result = await controller.GetOnuStateByOnuSerialNumberAsync(0, "DSNWcbd38d4e");

            // Assert
            var okResult = result.Should().BeOfType<JsonResult>().Subject;
            var onu = okResult.Value.Should().BeAssignableTo<H645GOnu>().Subject;

            onu.OltId.Should().Be(0);
            onu.OltPortId.Should().Be(1);
            onu.OltOnuId.Should().Be(1);

            onu.ModelType.Should().BeEquivalentTo(new ComplexStringType("H645G", "H645G", SeverityLevel.Default));
            onu.DescriptionName.Should().BeEquivalentTo(new ComplexStringType("ONU_A", "ONU_A", SeverityLevel.Default));
            onu.GponSerialNumber.Should().BeEquivalentTo(new ComplexStringType("DSNWcbd38d4e", "DSNWcbd38d4e", SeverityLevel.Default));
            onu.GponProfile.Should().BeEquivalentTo(new ComplexStringType("ONU-PROFILE-INT-VLAN100", "ONU-PROFILE-INT-VLAN100", SeverityLevel.Default));
            onu.FirmwareVersion.Should().BeEquivalentTo(new ComplexStringType("2.45", "2.45", SeverityLevel.Default));

            onu.OpticalConnectionState.Should().BeEquivalentTo(new ComplexIntType(2, "aktywny", SeverityLevel.Success));
            onu.OpticalConnectionDeactivationReason.Should().BeEquivalentTo(new ComplexIntType(1, "zanik zasilania (DGi)", SeverityLevel.Info));
            onu.OpticalPowerReceived.Should().BeEquivalentTo(new ComplexStringType("-215", "-21,5 dBm", SeverityLevel.Success));
            onu.OpticalCableDistance.Should().BeEquivalentTo(new ComplexIntType(1115, "1115 m", SeverityLevel.Default));

            onu.OpticalConnectionUptime.Should().BeEquivalentTo(new ComplexIntType(18844321, "218d 02:32:01 (12:50:02 1.01)", SeverityLevel.Default));
            onu.OpticalConnectionInactiveTime.Should().BeEquivalentTo(new ComplexIntType(0, "0d 00:00:00", SeverityLevel.Default));
            onu.SystemUptime.Should().BeEquivalentTo(new ComplexIntType(18844372, "218d 02:32:52 (12:50:53 1.01)", SeverityLevel.Default));

            onu.BlockStatus.Should().BeEquivalentTo(new ComplexIntType(255, "brak blokady", SeverityLevel.Success));
            onu.BlockReason.Should().BeEquivalentTo(new ComplexIntType(255, "brak blokady", SeverityLevel.Success));

            onu.EthernetPort1State.Should().BeEquivalentTo(new ComplexIntType(1, "up", SeverityLevel.Success));
            onu.EthernetPort1Speed.Should().BeEquivalentTo(new ComplexIntType(3, "1000 Mb/s", SeverityLevel.Success));
        }
    }
}