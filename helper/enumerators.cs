namespace Xyngr.helper
{

    public enum PlanType
    {
        Simple = 1,
        Business = 2
    }

    /// <summary>
    /// User Roles
    /// </summary>
    public enum userrole
    {
        Admin = 1,
        PropertyManager = 2,
        Owner = 3
    }

    /// <summary>
    /// File Type declaration for File Upload User Control
    /// </summary>
    public enum FType
    {
        image,
        video,
        pdf,
        css,
        normal
    }

    /// <summary>
    /// Reservation Status
    /// </summary>
    public enum ReservationStatus
    {
        completed,
        pending,
        cancelled
    }

    /// <summary>
    /// emailtoken
    /// </summary>
    public struct emailtoken
    {
        public const string FirstName = "#FirstName#";
        public const string LastName = "#LastName#";
        public const string Email = "#Email#";
        public const string Password = "#Password#";
        public const string Image = "#Image#";
        public const string Comment = "#Comment#";
        public const string Name = "#Name#";
        public const string Phone = "#Phone#";
        public const string OwnerName = "#OwnerName#";
        public const string PropertyName = "#PropertyName#";
        public const string BiddingPrice = "#BiddingPrice#";
        public const string Credit = "#Credit#";
        public const string PropertyURL = "#PropertyURL#";
        public const string Date = "#Date#";
        public const string PostedDate = "#PostedDate#";
        public const string RequestedDate = "#RequestedDate#";
        public const string Price = "#Price#";
        public const string DomainURL = "#DomainURL#";

        // Added for Send Mail to Guest
        public const string GuestName = "#GuestName#";
        public const string CheckIN = "#CheckIN#";
        public const string CheckOUT = "#CheckOUT#";
        public const string TotalDay = "#TotalDay#";
        public const string SubTotal = "#SubTotal#";
        public const string OptionDate = "#OptionDate#";
        public const string Advance = "#Advance#";
        public const string AdvanceDeadLine = "#AdvanceDeadLine#";
        public const string Balance = "#Balance#";
        public const string BalanceDeadLine = "#BalanceDeadLine#";
        public const string PaymentMethod = "#PaymentMethod#";
        public const string ServicesToBePaidLocally = "#ServicesToBePaidLocally#";
        public const string TelNumber = "#TelNumber#";
    }

    /// <summary>
    /// emailtemplatecode 
    /// </summary>
    public struct emailtemplatecode
    {
        public const string BIDDINGACTION_TOMANAGER = "BIDDINGACTION_TOMANAGER";
        public const string BIDDINGACTION_TOOWNER = "BIDDINGACTION_TOOWNER";
        public const string BIDDINGACTION_TOUSER = "BIDDINGACTION_TOUSER";

        public const string NEWBIDDING_TOMANAGER = "NEWBIDDING_TOMANAGER";
        public const string NEWBIDDING_TOOWNER = "NEWBIDDING_TOOWNER";
        public const string NEWBIDDING_TOUSER = "NEWBIDDING_TOUSER";

        public const string QUESTION_TOMANAGER = "QUESTION_TOMANAGER";
        public const string QUESTION_TOOWNER = "QUESTION_TOOWNER";
        ////public const string QUESTION_TOUSER = "QUESTION_TOUSER";
        public const string ANSWER_TOUSER = "ANSWER_TOUSER";

        public const string WINNER_TOMANAGER = "WINNER_TOMANAGER";
        public const string WINNER_TOOWNER = "WINNER_TOOWNER";
        public const string WINNER_TOUSER = "WINNER_TOUSER";

        public const string NEWAUCTION_TOMANAGER = "NEWAUCTION_TOMANAGER";
        public const string NEWAUCTION_TOOWNER = "NEWAUCTION_TOMANAGER";

        public const string NEWREGISTRATION_TOADMIN = "NEWREGISTRATION_TOADMIN";
        public const string NEWREGISTRATION_TOUSER = "NEWREGISTRATION_TOUSER";

        public const string PROPERTYREQUEST_TOMANAGER = "PROPERTYREQUEST_TOMANAGER";
        public const string PROPERTYREQUEST_TOOWNER = "PROPERTYREQUEST_TOOWNER";
        public const string REQUEST_ACK_TOUSER = "REQUEST_ACK_TOUSER";

        public const string NEWCREDIT_TOUSER = "NEWCREDIT_TOUSER";
        public const string NEWINCOME_TOUSERFROMAFFILIATE = "NEWINCOME_TOUSERFROMAFFILIATE";

        public const string FORGOTPASSWORD_TOUSER = "FORGOTPASSWORD_TOUSER";

        public const string NEWSLETTER_TONEWLETTERUSER = "NEWSLETTER_TONEWLETTERUSER";

        public const string REDUCECREDIT_TOUSER = "REDUCECREDIT_TOUSER";
        public const string BANNER_REDUCECREDIT_TOUSER = "BANNER_REDUCECREDIT_TOUSER";

        public const string CONTACTUS_TOADMIN = "CONTACTUS_TOADMIN";

        public const string CONTACTUS_TOPM = "CONTACTUS_TOPM";

        public const string NEWSIMILARPROPERTY_TOUSERS = "NEWSIMILARPROPERTY_TOUSERS";

        public const string CHANGE_PASSWORD = "CHANGE_PASSWORD";

    }

}

