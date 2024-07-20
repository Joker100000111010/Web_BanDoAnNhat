namespace Do_An_CNTT_K3.Models.Interfaces
{
    public interface ILienLacRepository
    {
        public void AddBinhLuan(Contact Comment);
        IEnumerable<Contact> GetAllComment();
        //Task SendEmailAsync(string toEmail, string subject, string message);
    }
}
