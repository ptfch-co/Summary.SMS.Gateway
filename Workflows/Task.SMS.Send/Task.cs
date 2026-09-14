namespace Summary.SMS.Gateway.Workflows.Task.SMS.Send
{
    using Core.Workflows.Abstractions.Models;
    using Core.Workflows.Activities;
    using Core.Workflows.Models;
    using Microsoft.Extensions.Localization;
    using Summary.SMS.Gateway.Services;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class SendSmsBySMSGatewayTask : TaskActivity
    {
        private readonly IStringLocalizer<SendSmsBySMSGatewayTask> T;
        private readonly ISMSGatewayService _gateway;

        public SendSmsBySMSGatewayTask(IStringLocalizer<SendSmsBySMSGatewayTask> t,
            ISMSGatewayService gateway)
        {
            T = t;
            _gateway = gateway;
        }

        public override string Name => nameof(SendSmsBySMSGatewayTask);

        public override LocalizedString DisplayText => T[SMSGateway.Localize.SOfSMSGateway];

        public override LocalizedString Category => T[SMSGateway.Public.Category];

        public override IEnumerable<Outcome> GetPossibleOutcomes(WorkflowExecutionContext workflowContext,
            ActivityContext activityContext)
        {
            return Outcomes(T[SMSGateway.Workflows.Done]);
        }

        public string Message
        {
            get => GetProperty<string>();
            set => SetProperty(value);
        }

        public string Phone_Number
        {
            get => GetProperty<string>();
            set => SetProperty(value);
        }

        public int? Sim_Slot
        {
            get => GetProperty(() => default(int?));
            set => SetProperty(value);
        }

        public override async Task<ActivityExecutionResult> ExecuteAsync(WorkflowExecutionContext workflowContext,
            ActivityContext activityContext)
        {
            var message = workflowContext.GetInputOrDefault(Message);
            var phone_number = workflowContext.GetInputOrDefault(Phone_Number);
            var sim_slot = Sim_Slot;

            await _gateway.SendSmsAsync(
                message,
                phone_number,
                sim_slot.Value
            );

            return Outcomes(SMSGateway.Workflows.Done);
        }
    }
}