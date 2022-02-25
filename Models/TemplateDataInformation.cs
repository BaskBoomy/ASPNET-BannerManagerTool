namespace WebToolManager.Models
{
    public class TemplateDataInformation
    {
        public int id { get; set; }
        public int templateId { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public string label { get; set; }
        public string placeholder { get; set; }
        public string data { get; set; }
    }
}
