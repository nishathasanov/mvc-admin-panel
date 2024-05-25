namespace zba.intranet.modules.Models
{
    public class QuestionDto
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public bool IsActive { get; set; }
        public string InsertedBy { get; set; }
        public long InsertedDate { get; set; }
        public int Point { get; set; }
        public int ResponseTime { get; set; }
        public long UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}