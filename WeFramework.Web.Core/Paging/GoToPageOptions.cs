namespace WeFramework.Web.Core.Paging
{
	///<summary>
	/// Options for configuring the output of <see cref = "PagerExtensions" />.
	///</summary>
	public class GoToPageOptions
	{
		///<summary>
		/// The default settings, with configurable querystring key (input field name).
		///</summary>
		public GoToPageOptions(string inputFieldName)
		{
			LabelFormat = "Go to page:";
			SubmitButtonFormat = "Go";
			InputFieldName = inputFieldName;
			InputFieldType = "number";
		}
	
		///<summary>
		/// The default settings.
		///</summary>
		public GoToPageOptions() : this("page")
		{
		}

		///<summary>
		/// The text to show in the form's input label.
		///</summary>
		///<example>
		/// "Go to page:"
		///</example>
		public string LabelFormat { get; set; }

		///<summary>
		/// The text to show in the form's submit button.
		///</summary>
		///<example>
		/// "Go"
		///</example>
		public string SubmitButtonFormat { get; set; }

		///<summary>
		/// The querystring key this form should submit the new page number as.
		///</summary>
		///<example>
		/// "page"
		///</example>
		public string InputFieldName { get; set; }

		///<summary>
		/// The HTML input type for this field. Defaults to the HTML5 "number" type, but can be changed to "text" if targetting previous versions of HTML.
		///</summary>
		///<example>
		/// "number"
		///</example>
		public string InputFieldType { get; set; }
	}
}