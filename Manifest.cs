using Core.Modules.Manifest;
using Summary.SMS.Gateway;

[assembly: Feature(
    Id = SMSGateway.Features.SMSGateway,
    Name = SMSGateway.Localize.SOfSMSGateway,
    Description =SMSGateway.Localize.DOfSMSGateway,
    Category = SMSGateway.Public.Category,
    Dependencies = new[] { "Core.Workflows" }
)]