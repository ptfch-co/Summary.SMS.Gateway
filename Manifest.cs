using Core.Modules.Manifest;
using Summary.SMS.Gateway;

[assembly: Feature(
    Id = SMSGateway.Features.SMSGateway,
    Name = SMSGateway.Localize.SubjectOfSMSGateway,
    Description =SMSGateway.Localize.DescriptionOfSMSGateway,
    Category = SMSGateway.Public.Category,
    Dependencies = new[] { "Core.Workflows" },
    Version = "1.0.0"
)]