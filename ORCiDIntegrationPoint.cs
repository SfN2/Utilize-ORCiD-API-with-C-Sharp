public partial class ORCiDIntegrationPoint : System.Web.UI.UserControl, IPostBackEventHandler
{
	private void Page_Load(object sender, EventArgs e)
    {
		protected ORCiDInterface _orcid = new ORCiDInterface();
        protected List<string> idToken = null;
		protected string code = string.Empty;
		protected string redirectUrl = "your return URL";
		protected string string clientSecret = "your client secret";
		protected string clientId = "your client ID";
		
		 if (Request.QueryString["code"] != null)
        {
            code = Request.QueryString["code"];
            if (!(string.IsNullOrEmpty(code)))
            {
                idToken = _orcid.GetAccessToken(code, redirectUrl, clientSecret, clientId); //gives user ORCiD ID and ORCiD token
				
                Session["Token"] = idToken[0];
                Session["Id"] = idToken[1];
			}
			
			//now you have the user's ORCiD ID and a token for accessing different information about the user
		}
		
	}
}