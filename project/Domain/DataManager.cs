using project.Domain.Repositories.Abstract;

namespace project.Domain
{
    public class DataManager
    {

        public ITextFieldsRepository TextFields { get; set; }
        public IServiceItemsRepository ServiceItems { get; set; }
		public IPhotoFieldsReporitory PhotoFields { get; set; }
		public DataManager(ITextFieldsRepository textFields, IServiceItemsRepository serviceItems, IPhotoFieldsReporitory photoFields)
		{
			TextFields = textFields;
			ServiceItems = serviceItems;
			PhotoFields = photoFields;
		}
	}
}
