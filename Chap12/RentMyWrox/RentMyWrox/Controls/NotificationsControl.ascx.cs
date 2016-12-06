using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RentMyWrox.Models;

namespace RentMyWrox.Controls
{
    public partial class NotificationsControl : System.Web.UI.UserControl
    {
        public enum DisplayType
        {
            AdminOnly,
            NonAdminOnly,
            Both
        }

        public DisplayType Display { get; set; }

        public DateTime? DateForDisplay { get; set; }

        private int numberToSkip;

        protected void Page_Load(object sender, EventArgs e)
        {
            using (RentMyWroxContext context = new RentMyWroxContext())
            {
                Notification note = context.Notifications
                    .Where(x => x.IsAdminOnly
                        && x.DisplayStartDate <= DateTime.Now
                        && x.DisplayEndDate >= DateTime.Now)
                    .OrderByDescending(y => y.CreateDate)
                    .FirstOrDefault();

                if (note != null)
                {
                    NotificationTitle.Text = note.Title;
                    NotificationDetail.Text = note.Details;
                }
            }
        }

    }
}