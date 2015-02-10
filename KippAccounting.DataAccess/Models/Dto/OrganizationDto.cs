namespace KippAcctCodeSearch.DataAccess
{
    public class OrganizationDto
    {
        public int OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        public string Division { get; set; }
        public int OrganizationCode { get; set; }
        public int? FunctionId { get; set; }
        public bool CombineOrganizationAndDivisionName { get; set; }
    }
}
