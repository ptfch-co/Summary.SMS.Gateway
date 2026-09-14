namespace Summary.SMS.Gateway.Workflows.Task.SMS.Send
{
    using Core.Workflows.Display;

    public class SendSmsBySMSGatewayDisplay : ActivityDisplayDriver<SendSmsBySMSGatewayTask,
        SendSmsBySMSGatewayViewModel>
    {
        protected override void EditActivity(SendSmsBySMSGatewayTask activity,
            SendSmsBySMSGatewayViewModel model)
        {
            model.Message = activity.Message;
            model.Phone_Number = activity.Phone_Number;
            model.Sim_Slot = activity.Sim_Slot;
        }

        protected override void UpdateActivity(SendSmsBySMSGatewayViewModel model,
            SendSmsBySMSGatewayTask activity)
        {
            activity.Message = model.Message;
            activity.Phone_Number = model.Phone_Number;
            activity.Sim_Slot = model.Sim_Slot;
        }
    }
}