namespace AeLa.EasyFeedback.Editor
{
	internal static class Constants
	{
		public const string ASSET_ROOT_DIRECTORY = "Easy Feedback";
		public const string CONFIG_ASSET_NAME = "EasyFeedbackConfig.asset";

		public const string MENU_PATH_ROOT = "/Tools/Easy Feedback";
		public const string CONFIG_MENU_PATH = MENU_PATH_ROOT + "/Configure";

        /// <summary>
        /// Forward slash (/) as unicode sequence. 
        /// Used as a workaround for forward slashes in Trello board names
        /// causing popups to erronously display submenus.
        /// </summary>
        public const string UNICODE_FORWARD_SLASH = "\u2215";
	}
}