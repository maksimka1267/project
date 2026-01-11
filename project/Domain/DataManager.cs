using project.Domain.Repositories.Abstract;

namespace project.Domain
{
    public class DataManager
    {

        public IPageItemsRepository TextFields { get; set; }
        public IArticleItemsRepository ServiceItems { get; set; }
        public INewsItemRepository NewsItems { get; set; }
        public IPhotoItemRepository PhotoItems { get; set; }
        public ITextModelRepository TextModels { get; set; }
        public DataManager(IPageItemsRepository textFields, IArticleItemsRepository serviceItems, INewsItemRepository newsItem, IPhotoItemRepository photoItem, ITextModelRepository textModel)
		{
			TextFields = textFields;
			ServiceItems = serviceItems;
            NewsItems = newsItem;
            PhotoItems = photoItem;
            TextModels = textModel;
		}
	}
}
