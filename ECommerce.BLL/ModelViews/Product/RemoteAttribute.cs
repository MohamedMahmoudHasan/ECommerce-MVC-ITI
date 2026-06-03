
namespace ECommerce.BLL
{
    internal class RemoteAttribute : Attribute
    {
        private string action;
        private string controller;

        public RemoteAttribute(string action, string controller)
        {
            this.action = action;
            this.controller = controller;
        }
    }
}