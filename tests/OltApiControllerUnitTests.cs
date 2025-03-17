using FluentAssertions;
using GPONMonitor.Controllers.API;
using GPONMonitor.Models;
using GPONMonitor.Models.Configuration;
using GPONMonitor.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace tests
{
    public class OltApiControllerUnitTests
    {
        private static void ArrangeOltConfigurationsWithOnusLists(out Mock<IOptions<DevicesConfiguration>> devicesConfigurationMock, out Mock<IDataService> dataServiceMock)
        {
            // Arrange
            devicesConfigurationMock = new Mock<IOptions<DevicesConfiguration>>();
            devicesConfigurationMock.Setup(x => x.Value).Returns(new DevicesConfiguration()
            {
                Devices = new List<OltConfiguration>()
                {
                    new OltConfiguration{ Id=0, Name="GPON OLT A", IpAddress="10.1.1.21",SnmpPort ="161", SnmpVersion="2", SnmpCommunity="public", SnmpTimeout="3000", IpHostWebManagementPort="80"},
                    new OltConfiguration{ Id=1, Name="GPON OLT B", IpAddress="10.1.1.11",SnmpPort ="161", SnmpVersion="2", SnmpCommunity="public", SnmpTimeout="3000", IpHostWebManagementPort="80"},
                    new OltConfiguration{ Id=2, Name="GPON OLT C", IpAddress="10.2.1.11",SnmpPort ="161", SnmpVersion="2", SnmpCommunity="public", SnmpTimeout="3000", IpHostWebManagementPort="80", SnmpV3Credentials= new SnmpV3Credentials(){ AuthenticationUsername="username", AuthenticationType="SHA1", AuthenticationPassword="userAuthenticationPassword", EncryptionType="DES", EncryptionPassword="encryptionPassword" } }
                }
            });

            dataServiceMock = new Mock<IDataService>();
            dataServiceMock.Setup(x => x.GetOnuDescriptionListAsync(0)).ReturnsAsync(() => new List<OnuShortDescription>()
            {
                new OnuShortDescription(1, 1, "ONU_Description_1", "DSNW12345678", "active", "-200"),
                new OnuShortDescription(1, 2, "ONU_Description_2", "DSNW23456789", "inactive", "-180"),
                new OnuShortDescription(1, 3, "ONU_Description_3", "DSNW34567890", "active", "-270"),
            });
            dataServiceMock.Setup(x => x.GetOnuDescriptionListAsync(1)).ReturnsAsync(() => new List<OnuShortDescription>()
            {
                new OnuShortDescription(1, 1, "ONU_Description_1", "DSNW45678901", "inactive", "-100"),
                new OnuShortDescription(2, 2, "ONU_Description_2", "DSNW56789012", "inactive", "-150"),
                new OnuShortDescription(3, 3, "ONU_Description_3", "DSNW67890123", "active", "-203"),
            });
            dataServiceMock.Setup(x => x.GetOnuDescriptionListAsync(2)).ReturnsAsync(() => new List<OnuShortDescription>()
            {

            });
        }

        [Fact]
        public async Task GetOnuDescriptionListAsync_ShouldReturnOnuDescriptionListIfOltHaveConfiguredOnus_Async()
        {
            // Arrange
            ArrangeOltConfigurationsWithOnusLists(out Mock<IOptions<DevicesConfiguration>> devicesConfigurationMock, out Mock<IDataService> dataServiceMock);

            var controller = new OltApiController(devicesConfigurationMock.Object, dataServiceMock.Object);

            // Act
            var result = await controller.GetOltOnuDescriptionListAsync(0);

            // Assert
            var okResult = result.Should().BeOfType<JsonResult>().Subject;
            var onus = okResult.Value.Should().BeAssignableTo<List<OnuShortDescription>>().Subject;
            onus.Count().Should().Be(3);
        }

        [Fact]
        public async Task GetOnuDescriptionListAsync_ShouldReturnEmptyOnuDescriptionListIfOltDoesntHaveConfiguredOnus_Async()
        {
            // Arrange
            ArrangeOltConfigurationsWithOnusLists(out Mock<IOptions<DevicesConfiguration>> devicesConfigurationMock, out Mock<IDataService> dataServiceMock);

            var controller = new OltApiController(devicesConfigurationMock.Object, dataServiceMock.Object);

            // Act
            var result = await controller.GetOltOnuDescriptionListAsync(2);

            // Assert
            var okResult = result.Should().BeOfType<JsonResult>().Subject;
            var onus = okResult.Value.Should().BeAssignableTo<List<OnuShortDescription>>().Subject;
            onus.Count().Should().Be(0);
        }
    }
}
