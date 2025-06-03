namespace IncidentManagement.DataModel.UIModels.UICategory
{
    public class UICategoryModel
    {
        public Guid CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string CategoryDescription { get; set; }

        public string CreatedBy { get; set; }

        public string CreatedOn { get; set; }

        public string ModifiedBy { get; set; }

        public string ModifiedOn { get; set; }
    }
}
