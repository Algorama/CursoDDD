namespace Kernel.Domain.Model.Dtos
{
    public class EnumItem
    {
        public int Value { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }

        public override string ToString() => $"{Value}: {Text} - {Description}";
    }
}
